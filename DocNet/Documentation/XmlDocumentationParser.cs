using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocNet.Documentation
{
    /// <summary>
    /// Parses .NET Documentation into <see cref="DocumentedMember"/>s.
    /// </summary>
    /// <remarks>This class is stateless and thus thread-safe.</remarks>
    public class XmlDocumentationParser : IDocumentationParser
    {
        /// <summary>
        /// Initializes a new <see cref="XmlDocumentationParser"/>.
        /// </summary>
        public XmlDocumentationParser() { }

        /// <summary>
        /// Asynchronously parses documentation for the specified assembly.
        /// </summary>
        /// <param name="assemblyPath">The full path to the assembly being documented.</param>
        /// <param name="xmlDocumentationPath">The path to the XML file containing the documentation.</param>
        /// <returns>A <see cref="Task{T}"/> representing the asynchronous parsing process.</returns>
        public Task<Documentation> ParseAsync(String assemblyPath, String xmlDocumentationPath)
        {
            return Task.Run(() =>
            {
                Assembly documentedAssembly = Assembly.LoadFrom(assemblyPath);
                XDocument document = XDocument.Load(xmlDocumentationPath);
                DocumentationWrapper xmlDocumentation = new DocumentationWrapper(document, documentedAssembly);

                IEnumerable<DocumentedType> nestedDocumentation = xmlDocumentation.TypeDocumentation.Select(documentedType =>
                {
                    DocumentedType docEdType = new DocumentedType();
                    docEdType.Member = documentedType.Key;
                    docEdType.Xml = documentedType.Value;
                    return docEdType;
                });

                return new Documentation(documentedAssembly, xmlDocumentationPath, nestedDocumentation.Select(documentedType =>
                {
                    documentedType.Constructors = xmlDocumentation.ConstructorDocumentation
                        .Where(cDoc => cDoc.Key.DeclaringType == documentedType.Member)
                        .Select(cDoc => new DocumentedMember<ConstructorInfo>(cDoc.Key, cDoc.Value));
                    documentedType.Events = xmlDocumentation.EventDocumentation
                        .Where(eDoc => eDoc.Key.DeclaringType == documentedType.Member)
                        .Select(eDoc => new DocumentedMember<EventInfo>(eDoc.Key, eDoc.Value));
                    documentedType.Methods = xmlDocumentation.MethodDocumentation
                        .Where(mDoc => mDoc.Key.DeclaringType == documentedType.Member)
                        .Select(mDoc => new DocumentedMember<MethodInfo>(mDoc.Key, mDoc.Value));
                    documentedType.NestedTypes = nestedDocumentation
                        .Where(docType => documentedType.Member == docType.Member.DeclaringType);
                    documentedType.Properties = xmlDocumentation.PropertyDocumentation
                        .Where(pDoc => pDoc.Key.DeclaringType == documentedType.Member)
                        .Select(pDoc => new DocumentedMember<PropertyInfo>(pDoc.Key, pDoc.Value));
                    return documentedType;
                }).ToArray()); // We already are on a separate thread, so we can execute everything here instead of deferring it
            });
        }
    }

    /// <summary>
    /// Wraps an <see cref="XDocument"/> simplifying documentation access.
    /// </summary>
    internal class DocumentationWrapper
    {
        /// <summary>
        /// Contains the documentation for constructors.
        /// </summary>
        public IDictionary<ConstructorInfo, XElement> ConstructorDocumentation { get; private set; }

        /// <summary>
        /// Contains the documentation for events.
        /// </summary>
        public IDictionary<EventInfo, XElement> EventDocumentation { get; private set; }

        /// <summary>
        /// Contains the documentation for fields.
        /// </summary>
        public IDictionary<FieldInfo, XElement> FieldDocumentation { get; private set; }

        /// <summary>
        /// Contains the documentation for methods.
        /// </summary>
        public IDictionary<MethodInfo, XElement> MethodDocumentation { get; private set; }

        /// <summary>
        /// Contains the documentation for types.
        /// </summary>
        public IDictionary<Type, XElement> TypeDocumentation { get; private set; }

        /// <summary>
        /// Contains the documentation for properties.
        /// </summary>
        public IDictionary<PropertyInfo, XElement> PropertyDocumentation { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="DocumentationWrapper"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> containing the <see cref="Type"/>s to be documented.</param>
        /// <param name="xmlDocumentation">The underlying <see cref="XDocument"/> containing the documentation.</param>
        public DocumentationWrapper(XDocument xmlDocumentation, Assembly assembly)
        {
            Contract.Requires<ArgumentNullException>(xmlDocumentation != null && assembly != null);

            XElement nameElement = xmlDocumentation.Descendants("name").FirstOrDefault();
            String assemblyName = (nameElement ?? new XElement("name")).Value ?? String.Empty;
            if (assembly.GetName().Name != assemblyName)
            {
                throw new InvalidOperationException(
                    String.Format(
                        "The XML documentation is not generated from the specified assembly ({0}), the names (assembly: '{1}'; XML docs: '{2}') do not match.",
                        assembly.Location,
                        assembly.FullName,
                        assemblyName
                    )
                );
            }

            IEnumerable<XElement> memberElements = xmlDocumentation.Descendants("member").ToArray(); // ToArray for parallelization
            IEnumerable<XElement> constructorDocumentation = memberElements.Where(element => element.Attribute("name").Value.Contains("#ctor") || element.Attribute("name").Value.Contains("#cctor"));
            IEnumerable<XElement> eventDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("E:"));
            IEnumerable<XElement> fieldDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("F:"));
            IEnumerable<XElement> methodDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("M:"));
            IEnumerable<XElement> typeDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("T:"));
            IEnumerable<XElement> propertyDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("P:"));
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

            this.ConstructorDocumentation = new Dictionary<ConstructorInfo, XElement>();
            //this.ConstructorDocumentation = constructorDocumentation.AsParallel().ToDictionary(element =>
            //{
            //    String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
            //    Type declaringType = this.GetDeclaringType(nameWithoutPrefix, assembly);

            //    return declaringType.GetConstructors().First(cInfo => this.ParametersMatching(cInfo, this.GetMemberName(nameWithoutPrefix)));
            //}, element => element);

            this.EventDocumentation = eventDocumentation.AsParallel().ToDictionary(element =>
            {
                String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
                Type declaringType = this.GetDeclaringType(nameWithoutPrefix, assembly);
                String eventName = this.GetMemberName(nameWithoutPrefix);

                return declaringType.GetEvent(eventName, flags);
            }, element => element);

            this.FieldDocumentation = fieldDocumentation.AsParallel().ToDictionary(element =>
            {
                String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
                Type declaringType = this.GetDeclaringType(nameWithoutPrefix, assembly);
                String fieldName = this.GetMemberName(nameWithoutPrefix);

                return declaringType.GetField(fieldName, flags);
            }, element => element);

            this.MethodDocumentation = new Dictionary<MethodInfo, XElement>();
            //this.MethodDocumentation = methodDocumentation.AsParallel().ToDictionary(element =>
            //{
            //    String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
            //    Type declaringType = this.GetDeclaringType(nameWithoutPrefix, assembly);

            //    return declaringType.GetMethods().First(mInfo => this.ParametersMatching(mInfo, this.GetMemberName(nameWithoutPrefix)));
            //}, element => element);

            this.TypeDocumentation = typeDocumentation.AsParallel().ToDictionary(element =>
            {
                return this.GetTypeFromAllLoadedAssemblies(this.GetDeclaringTypeName(this.GetNameAttributeValueWithoutPrefix(element)));
            }, element => element);

            this.PropertyDocumentation = propertyDocumentation.AsParallel().ToDictionary(element =>
            {
                String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
                Type declaringType = this.GetDeclaringType(nameWithoutPrefix, assembly);
                String propertyName = this.GetMemberName(nameWithoutPrefix);

                return declaringType.GetProperty(propertyName, flags);
            }, element => element);
        }

        /// <summary>
        /// Gets the <see cref="Type"/> from the specified input inside the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="assembly">The <see cref="Assembly"/> to search for the <see cref="Type"/> in.</param>
        /// <returns>The <see cref="Type"/> with the specified name.</returns>
        private Type GetDeclaringType(String input, Assembly assembly)
        {
            Contract.Requires<ArgumentNullException>(input != null && assembly != null);

            return assembly.GetType(this.GetDeclaringTypeName(input), true, true);
        }

        /// <summary>
        /// Gets the name of the <see cref="Type"/> from the specified input inside the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The <see cref="Type"/> with the specified name.</returns>
        private String GetDeclaringTypeName(String input)
        {
            Contract.Requires<ArgumentNullException>(input != null);

            int indexOfBracket = input.IndexOf('(');
            int indexOfDot = input.LastIndexOf('.', (indexOfBracket >= 0) ? indexOfBracket : input.Length - 1);
            return input.Substring(0, indexOfDot);
        }

        /// <summary>
        /// Gets the count of generic parameters.
        /// </summary>
        /// <param name="method">The method to parse the amount of generic parameters from.</param>
        /// <returns>The method.</returns>
        private int GetGenericParameterCount(String method)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(method));

            Regex regex = new Regex(@"`\d\){1,}");
            return int.Parse(regex.Match(method).Value.Substring(1));
        }

        /// <summary>
        /// Gets the value of the "name"-attribute on the <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> to get the value of the "name"-attribute of.</param>
        /// <returns>The value of the name attribute.</returns>
        private String GetNameAttributeValue(XElement element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Ensures(Contract.Result<String>() != null);

            XAttribute attribute = element.Attribute("name");
            return (attribute != null) ? attribute.Value : String.Empty;
        }

        /// <summary>
        /// Gets the value of the "name"-attribute on the <see cref="XElement"/> without the prefix.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> to get the value of the "name"-attribute of.</param>
        /// <returns>The value of the name attribute.</returns>
        private String GetNameAttributeValueWithoutPrefix(XElement element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Ensures(Contract.Result<String>() != null);

            String name = this.GetNameAttributeValue(element);
            Contract.Assume(name.Length >= 2);
            return name.Substring(2);
        }

        /// <summary>
        /// Gets the contents after the last dot.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The <see cref="String"/> after the last dot / the member's name.</returns>
        private String GetMemberName(String input)
        {
            Contract.Requires<ArgumentNullException>(input != null);
            Contract.Ensures(Contract.Result<String>() != null);

            int indexOfBracket = input.IndexOf('(');
            int indexOfDot = input.LastIndexOf('.', (indexOfBracket >= 0) ? indexOfBracket : input.Length - 1);
            return input.Substring(indexOfDot + 1);
        }

        /// <summary>
        /// Gets the method parameters.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The parameters.</returns>
        private String[] GetParameters(String input)
        {
            Contract.Requires<ArgumentNullException>(input != null);
            Contract.Ensures(Contract.Result<String[]>() != null);

            int indexOfBracket = input.IndexOf('(');
            Contract.Assume(input.Length >= indexOfBracket - 2);
            return (indexOfBracket >= 0) ?
                input.Substring(indexOfBracket + 1, input.Length - indexOfBracket - 2).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) :
                new String[] { };
        }

        /// <summary>
        /// Gets a <see cref="Type"/> from it's <see cref="P:Type.FullName"/>.
        /// </summary>
        /// <param name="fullTypeName">The <see cref="Type"/> to obtain's full type name.</param>
        /// <returns>The <see cref="Type"/> with the specified name, or null if the <see cref="Type"/> could not be found.</returns>
        private Type GetTypeFromAllLoadedAssemblies(String fullTypeName)
        {
            Contract.Requires<ArgumentNullException>(fullTypeName != null);

            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.FullName == fullTypeName);
        }

        /// <summary>
        /// Checks whether the specified parameter is generic.
        /// </summary>
        /// <param name="parameterString">The parameter string as it is contained in the documentation.</param>
        /// <returns><c>true</c> if the parameter is generic, otherwise <c>false</c>.</returns>
        private bool IsGeneric(String parameterString)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(parameterString));

            return parameterString.StartsWith("`");
        }

        /// <summary>
        /// Checks whether the parameters are matching.
        /// </summary>
        /// <param name="mInfo">The method.</param>
        /// <param name="methodWithParameters">The method with the parameters as they are given in the documentation.</param>
        /// <returns><c>true</c> if the parameters are the same, otherwise <c>false</c>.</returns>
        private bool ParametersMatching(MethodBase mInfo, String methodWithParameters)
        {
            Contract.Requires<ArgumentNullException>(mInfo != null && methodWithParameters != null);

            return false;

            Type[] genericParameters = mInfo.GetGenericArguments();
            int docGenericArgumentCount = this.GetGenericParameterCount(methodWithParameters);
            if (genericParameters.Length != docGenericArgumentCount)
            {
                return false;
            }

            ParameterInfo[] parameters = mInfo.GetParameters();
            Contract.Assume(parameters != null && parameters.All(param => param != null));
            String[] docParameters = this.GetParameters(methodWithParameters);
            if (parameters.Length != docParameters.Length)
            {
                return false;
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                // Check if we're dealing with generic parameters
                Type parameterType = parameters[i].ParameterType;
                bool isGenericDocParameter = this.IsGeneric(docParameters[i]);
                if (parameterType.IsGenericParameter && isGenericDocParameter)
                {
                    // Check if we are dealing with the same type here
                    if (parameterType != genericParameters[int.Parse(docParameters[i].Substring(1))])
                    {
                        return false;
                    }
                }
                else
                {
                    // If parameter types are unequal, return false.
                    if (parameterType != this.ResolveTypeFromDocTypeName(docParameters[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Resolves a <see cref="Type"/> from the syntax as they are given in the docs.
        /// </summary>
        /// <param name="typeName">The type name as they are given in the documentation XML.</param>
        /// <returns>The resolved <see cref="Type"/> or null, if none was found.</returns>
        private Type ResolveTypeFromDocTypeName(String typeName)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(typeName));


            throw new NotImplementedException();
        }

        /// <summary>
        /// Contains Contract.Invariant definitions.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ConstructorDocumentation != null);
            Contract.Invariant(this.EventDocumentation != null);
            Contract.Invariant(this.FieldDocumentation != null);
            Contract.Invariant(this.MethodDocumentation != null);
            Contract.Invariant(this.TypeDocumentation != null);
            Contract.Invariant(this.PropertyDocumentation != null);
        }
    }
}
