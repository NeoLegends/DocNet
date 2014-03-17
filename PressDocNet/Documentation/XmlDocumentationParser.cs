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
        /// <param name="includePrivate"><c>true</c> if the method shall parse documentation for private members as well, otherwise <c>false</c>.</param>
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

                IEnumerable<XElement> memberElements = xmlDocumentation.Descendants("member").ToArray();
                IEnumerable<XElement> constructorDocumentation = memberElements.Where(element => element.Attribute("name").Value.Contains("#ctor"));
                IEnumerable<XElement> eventDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("E:"));
                IEnumerable<XElement> fieldDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("F:"));
                IEnumerable<XElement> methodDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("M:"));
                IEnumerable<XElement> typeDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("T:"));
                IEnumerable<XElement> propertyDocumentation = memberElements.Where(element => element.Attribute("name").Value.StartsWith("P:"));

                this.ConstructorDocumentation = constructorDocumentation.AsParallel().ToDictionary(element =>
                {
                    String name = element.Attribute("name").Value.Substring(2);
                    String[] splittedNameAttribute = name.Split(new[] { ".#ctor" }, StringSplitOptions.RemoveEmptyEntries);

                    Type declaringType = assembly.GetType(splittedNameAttribute[0], true, false);
                    Type[] ctorParams = splittedNameAttribute[1].Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ctorParam => Type.GetType(ctorParam)).ToArray(); // Bullshit, takes no generics into account
                    ConstructorInfo constructor = declaringType.GetConstructors().FirstOrDefault(cInfo =>
                    {
                        return false;
                    });

                    return ((ConstructorInfo)null);
                }, element => element);

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
}
