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
        public async Task<Documentation> ParseAsync(String assemblyPath, String xmlDocumentationPath, bool includePrivate)
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

            BindingFlags publicOnly = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
            BindingFlags publicAndPrivate = publicOnly | BindingFlags.NonPublic;
            IEnumerable<MemberInfo> membersToProcess = documentedAssembly.GetMembers(includePrivate ? publicAndPrivate : publicOnly);

            IEnumerable<Task<DocumentedMember>> processingTasks = membersToProcess
                .Select(member => Task.Run(() => new DocumentedMember(member, this.GetDocumentationForMember(member, xmlDocumentation))));
            return new Documentation(documentedAssembly, xmlDocumentationPath, await Task<DocumentedMember>.WhenAll(processingTasks));
        }

        /// <summary>
        /// Gets the documentation for a single member.
        /// </summary>
        /// <param name="member">The member to be documented.</param>
        /// <param name="wrapper">The documentation for all members.</param>
        /// <returns>The documentation for a single member.</returns>
        private XElement GetDocumentationForMember(MemberInfo member, DocumentationWrapper wrapper)
        {
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
            public XDocument FullDocs { get; private set; }

            /// <summary>
            /// Gets the name of the assembly the documentation was generated from.
            /// </summary>
            public String AssemblyName
            {
                get
                {
                    XElement nameElement = this.FullDocs.Descendants("name").FirstOrDefault();
                    return (nameElement != null) ? nameElement.Value : String.Empty;
                }
            }

            /// <summary>
            /// Gets the documentation.
            /// </summary>
            public IEnumerable<MemberDocumentation> Documentation
            {
                get
                {
                    return this.FullDocs
                               .Descendants("member")
                               .Select(memberElement => new MemberDocumentation(memberElement.Attribute("name").Value, memberElement));
                }
            }

            /// <summary>
            /// Initializes a new <see cref="DocumentationWrapper"/>.
            /// </summary>
            /// <param name="xmlDocumentation">The underlying <see cref="XDocument"/> containing the documentation.</param>
            public DocumentationWrapper(XDocument xmlDocumentation)
            {
                Contract.Requires<ArgumentNullException>(xmlDocumentation != null);

                this.FullDocs = xmlDocumentation;
            }

            /// <summary>
            /// Contains Contract.Invariant definitions.
            /// </summary>
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.FullDocs != null);
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
                public XElement Documentation { get; private set; }

                /// <summary>
                /// Initializes a new <see cref="MemberDocumentation"/>.
                /// </summary>
                /// <param name="memberName">The member's name string.</param>
                /// <param name="documentation">All documentation node.</param>
                public MemberDocumentation(String memberName, XElement documentation)
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
