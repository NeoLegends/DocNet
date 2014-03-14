using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocNetPress.Documentation
{
    /// <summary>
    /// Parses .NET Documentation into <see cref="DocumentedMember"/>s.
    /// </summary>
    /// <remarks>This class is thread-safe.</remarks>
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
        public Task<Documentation> ParseAsync(String assemblyPath, String xmlDocumentationPath, bool includePrivate)
        {
            Assembly documentedAssembly = Assembly.LoadFrom(assemblyPath);
            DocumentationWrapper xmlDocumentation = new DocumentationWrapper(XDocument.Load(xmlDocumentationPath));
            
            if (documentedAssembly.FullName != xmlDocumentation.AssemblyName)
            {
                throw new InvalidOperationException(
                    String.Format(
                        "The XML documentation ({0}) is not generated from the specified assembly ({1}).",
                        xmlDocumentationPath,
                        assemblyPath
                    )
                );
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Wraps an <see cref="XDocument"/> simplifying documentation access.
        /// </summary>
        private class DocumentationWrapper
        {
            /// <summary>
            /// The underlying <see cref="XDocument"/>.
            /// </summary>
            public XDocument XmlDocumentation { get; private set; }

            /// <summary>
            /// Gets the name of the assembly the documentation was generated from.
            /// </summary>
            public String AssemblyName
            {
                get
                {
                    return this.XmlDocumentation.Root.Element("assembly").Element("name").Value;
                }
            }

            /// <summary>
            /// Gets the documentation 
            /// </summary>
            public IEnumerable<MemberDocumentation> Documentation
            {
                get
                {
                    return this.XmlDocumentation.Root.Element("members").Elements("member")
                        .Select(memberElement => new MemberDocumentation(memberElement.Attribute("name").Value, memberElement.Elements()));
                }
            }

            /// <summary>
            /// Initializes a new <see cref="DocumentationWrapper"/>.
            /// </summary>
            /// <param name="xmlDocumentation">The underlying <see cref="XDocument"/> containing the documentation.</param>
            public DocumentationWrapper(XDocument xmlDocumentation)
            {
                this.XmlDocumentation = xmlDocumentation;
            }

            /// <summary>
            /// Contains the documentation for a single member with it's name string as it is contained in the XML file.
            /// </summary>
            public struct MemberDocumentation
            {
                /// <summary>
                /// The member name string.
                /// </summary>
                public String MemberName { get; private set; }

                /// <summary>
                /// The member documentation.
                /// </summary>
                public IEnumerable<XElement> Documentation { get; private set; }

                /// <summary>
                /// Initializes a new <see cref="MemberDocumentation"/>.
                /// </summary>
                /// <param name="memberName">The member's name string.</param>
                /// <param name="documentation">All documentation subnodes.</param>
                public MemberDocumentation(String memberName, IEnumerable<XElement> documentation)
                    : this()
                {
                    Contract.Requires<ArgumentNullException>(memberName != null && documentation != null);

                    this.MemberName = memberName;
                    this.Documentation = documentation;
                }
            }
        }
    }
}
