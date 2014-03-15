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
        public Task<Documentation> ParseAsync(String assemblyPath, String xmlDocumentationPath, bool includePrivate)
        {
            return Task.Run(() =>
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

                IEnumerable<Type> allTypes = includePrivate ? documentedAssembly.GetTypes() : documentedAssembly.GetExportedTypes();
                IEnumerable<Type> typesWithoutSubtypes = allTypes.Where(type => !type.IsNested);

                return ((Documentation)null);
            });
        }

        /// <summary>
        /// Gets the documentation for a <see cref="ConstructorInfo"/>.
        /// </summary>
        /// <param name="constructorInfo">The constructor to be documented.</param>
        /// <param name="wrapper">The <see cref="DocumentationWrapper"/> containing the documentation data.</param>
        /// <returns>The <see cref="XElement"/> containing the documentation for the specified member.</returns>
        private XElement GetDocumentation(ConstructorInfo constructorInfo, DocumentationWrapper wrapper)
        {
            Contract.Requires<ArgumentNullException>(constructorInfo != null && wrapper != null);

            IEnumerable<XElement> constructorDocumentation = wrapper.ConstructorDocumentation;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the documentation for an <see cref="EventInfo"/>.
        /// </summary>
        /// <param name="eventInfo">The event to be documented.</param>
        /// <param name="wrapper">The <see cref="DocumentationWrapper"/> containing the documentation data.</param>
        /// <returns>The <see cref="XElement"/> containing the documentation for the specified member.</returns>
        private XElement GetDocumentation(EventInfo eventInfo, DocumentationWrapper wrapper)
        {
            Contract.Requires<ArgumentNullException>(eventInfo != null && wrapper != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the documentation for a <see cref="FieldInfo"/>.
        /// </summary>
        /// <param name="fieldInfo">The field to be documented.</param>
        /// <param name="wrapper">The <see cref="DocumentationWrapper"/> containing the documentation data.</param>
        /// <returns>The <see cref="XElement"/> containing the documentation for the specified member.</returns>
        private XElement GetDocumentation(FieldInfo fieldInfo, DocumentationWrapper wrapper)
        {
            Contract.Requires<ArgumentNullException>(fieldInfo != null && wrapper != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the documentation for a <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="methodInfo">The method to be documented.</param>
        /// <param name="wrapper">The <see cref="DocumentationWrapper"/> containing the documentation data.</param>
        /// <returns>The <see cref="XElement"/> containing the documentation for the specified member.</returns>
        private XElement GetDocumentation(MethodInfo methodInfo, DocumentationWrapper wrapper)
        {
            Contract.Requires<ArgumentNullException>(methodInfo != null && wrapper != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the documentation for a <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="propertyInfo">The member to be documented.</param>
        /// <param name="wrapper">The <see cref="DocumentationWrapper"/> containing the documentation data.</param>
        /// <returns>The <see cref="XElement"/> containing the documentation for the specified member.</returns>
        private XElement GetDocumentation(PropertyInfo propertyInfo, DocumentationWrapper wrapper)
        {
            Contract.Requires<ArgumentNullException>(propertyInfo != null && wrapper != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the documentation for a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The member to be documented.</param>
        /// <param name="wrapper">The <see cref="DocumentationWrapper"/> containing the documentation data.</param>
        /// <returns>The <see cref="XElement"/> containing the documentation for the specified member.</returns>
        private XElement GetDocumentation(Type type, DocumentationWrapper wrapper)
        {
            Contract.Requires<ArgumentNullException>(type != null && wrapper != null);

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
                    Contract.Ensures(Contract.Result<IEnumerable<MemberDocumentation>>() != null);

                    return this.FullDocs
                               .Descendants("member")
                               .Select(memberElement => new MemberDocumentation(memberElement.Attribute("name").Value, memberElement));
                }
            }

            /// <summary>
            /// Gets all <see cref="XElement"/>s documenting constructors.
            /// </summary>
            public IEnumerable<XElement> ConstructorDocumentation
            {
                get
                {
                    Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

                    return this.FilterMembersByName(attribute => attribute.Value.Contains("#ctor"));
                }
            }

            /// <summary>
            /// Gets all <see cref="XElement"/>s documenting events.
            /// </summary>
            public IEnumerable<XElement> EventDocumentation
            {
                get
                {
                    Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

                    return this.FilterMembersByName(attribute => attribute.Value.StartsWith("E:"));
                }
            }

            /// <summary>
            /// Gets all <see cref="XElement"/>s documenting fields.
            /// </summary>
            public IEnumerable<XElement> FieldDocumentation
            {
                get
                {
                    Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

                    return this.FilterMembersByName(attribute => attribute.Value.StartsWith("F:"));
                }
            }

            /// <summary>
            /// Gets all <see cref="XElement"/>s documenting methods.
            /// </summary>
            public IEnumerable<XElement> MethodDocumentation
            {
                get
                {
                    Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

                    return this.FilterMembersByName(attribute => attribute.Value.StartsWith("M:"));
                }
            }

            /// <summary>
            /// Gets all <see cref="XElement"/>s documenting types.
            /// </summary>
            public IEnumerable<XElement> TypeDocumentation
            {
                get
                {
                    Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

                    return this.FilterMembersByName(attribute => attribute.Value.StartsWith("T:"));
                }
            }

            /// <summary>
            /// Gets all <see cref="XElement"/>s documenting properties.
            /// </summary>
            public IEnumerable<XElement> PropertyDocumentation
            {
                get
                {
                    Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

                    return this.FilterMembersByName(attribute => attribute.Value.StartsWith("P:"));
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
            /// Filters the "member"-elements by their "name"-attribute.
            /// </summary>
            /// <param name="predicate">The condition an attribute has to fullfill.</param>
            /// <returns>A collection of matches.</returns>
            private IEnumerable<XElement> FilterMembersByName(Predicate<XAttribute> predicate)
            {
                Contract.Requires<ArgumentNullException>(predicate != null);
                Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

                return this.FullDocs.Descendants("member").Where(element => predicate(element.Attribute("name")));
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
