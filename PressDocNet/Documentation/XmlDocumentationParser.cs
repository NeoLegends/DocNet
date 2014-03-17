using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PressDocNet.Documentation
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

                throw new NotImplementedException();
                return ((Documentation)null);
            });
        }

        /// <summary>
        /// Wraps an <see cref="XDocument"/> simplifying documentation access.
        /// </summary>
        private class DocumentationWrapper
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
                if (assembly.FullName != assemblyName)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "The XML documentation is not generated from the specified assembly ({0}), the names ({1} from the assembly and {2} from the XML file) do not match.",
                            assembly.Location,
                            assembly.FullName,
                            assemblyName
                        )
                    );
                }

                IEnumerable<XElement> memberElements = xmlDocumentation.Descendants("member").ToArray(); // ToArray for parallelization
                IEnumerable<XElement> constructorDocumentation = memberElements.Where(element => element.Attribute("name").Value.Contains("#ctor"));
                IEnumerable<XElement> eventDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("E:"));
                IEnumerable<XElement> fieldDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("F:"));
                IEnumerable<XElement> methodDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("M:"));
                IEnumerable<XElement> typeDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("T:"));
                IEnumerable<XElement> propertyDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("P:"));
                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

                this.ConstructorDocumentation = constructorDocumentation.AsParallel().ToDictionary(element =>
                {
                    String name = this.GetNameAttributeValueWithoutPrefix(element);
                    String[] splittedNameAttribute = name.Split(new[] { ".#ctor" }, StringSplitOptions.RemoveEmptyEntries);

                    Type declaringType = assembly.GetType(splittedNameAttribute[0], true, false);
                    Type[] ctorParams = splittedNameAttribute[1].Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ctorParam => Type.GetType(ctorParam)).ToArray(); // Bullshit, takes no generics into account
                    ConstructorInfo constructor = declaringType.GetConstructors().FirstOrDefault(cInfo =>
                    {
                        return false;
                    });

                    throw new NotImplementedException();
                    return ((ConstructorInfo)null);
                }, element => element);

                this.EventDocumentation = eventDocumentation.AsParallel().ToDictionary(element =>
                {
                    String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
                    Type declaringType = this.GetTypeFromNameAttribute(nameWithoutPrefix, assembly);
                    String eventName = this.GetMemberName(nameWithoutPrefix);

                    return declaringType.GetEvent(eventName, flags);
                }, element => element);

                this.FieldDocumentation = fieldDocumentation.AsParallel().ToDictionary(element =>
                {
                    String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
                    Type declaringType = this.GetTypeFromNameAttribute(nameWithoutPrefix, assembly);
                    String fieldName = this.GetMemberName(nameWithoutPrefix);

                    return declaringType.GetField(fieldName, flags);
                }, element => element);

                this.MethodDocumentation = methodDocumentation.AsParallel().ToDictionary(element =>
                {
                    throw new NotImplementedException();
                    return ((MethodInfo)null);
                }, element => element);

                this.TypeDocumentation = typeDocumentation.AsParallel().ToDictionary(element =>
                {
                    return assembly.GetType(this.GetNameAttributeValueWithoutPrefix(element), true, true);
                }, element => element);

                this.PropertyDocumentation = propertyDocumentation.AsParallel().ToDictionary(element =>
                {
                    String nameWithoutPrefix = this.GetNameAttributeValueWithoutPrefix(element);
                    Type declaringType = this.GetTypeFromNameAttribute(nameWithoutPrefix, assembly);
                    String propertyName = this.GetMemberName(nameWithoutPrefix);

                    return declaringType.GetProperty(propertyName, flags);
                }, element => element);
            }

            /// <summary>
            /// Gets the value of the "name"-attribute on the <see cref="XElement"/>.
            /// </summary>
            /// <param name="element">The <see cref="XElement"/> to get the value of the "name"-attribute of.</param>
            /// <returns>The value of the name attribute.</returns>
            private String GetNameAttributeValue(XElement element)
            {
                Contract.Requires<ArgumentNullException>(element != null);

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

                int lastIndexOfDot = input.LastIndexOf('.');
                return input.Substring((lastIndexOfDot >= 0) ? lastIndexOfDot : 0);
            }

            /// <summary>
            /// Gets the <see cref="Type"/> from the specified input inside the specified <see cref="Assembly"/>.
            /// </summary>
            /// <param name="input">The input.</param>
            /// <param name="assembly">The <see cref="Assembly"/> to search for the <see cref="Type"/> in.</param>
            /// <returns>The <see cref="Type"/> with the specified name.</returns>
            private Type GetTypeFromNameAttribute(String input, Assembly assembly)
            {
                Contract.Requires<ArgumentNullException>(input != null && assembly != null);

                int lastIndexOfDot = input.LastIndexOf('.');
                return assembly.GetType(input.Substring(0, (lastIndexOfDot >= 0) ? lastIndexOfDot : 0), true, true);
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
}
