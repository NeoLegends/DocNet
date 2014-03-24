using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocNet.Documentation
{
    /// <summary>
    /// Represents the documentation for a <see cref="Type"/>.
    /// </summary>
    public class DocumentedType : DocumentedMember<Type>,
                                  IEnumerable<DocumentedMember<ConstructorInfo>>,
                                  IEnumerable<DocumentedMember<EventInfo>>,
                                  IEnumerable<DocumentedMember<FieldInfo>>,
                                  IEnumerable<DocumentedMember<MethodInfo>>,
                                  IEnumerable<DocumentedType>,
                                  IEnumerable<DocumentedMember<PropertyInfo>>
    {
        /// <summary>
        /// Contains the documentation for all constructors on the <see cref="Type"/>.
        /// </summary>
        public IEnumerable<DocumentedMember<ConstructorInfo>> Constructors { get; set; }

        /// <summary>
        /// Contains the documentation for all events on the <see cref="Type"/>.
        /// </summary>
        public IEnumerable<DocumentedMember<EventInfo>> Events { get; set; }

        /// <summary>
        /// Contains the documentation for all fields on the <see cref="Type"/>.
        /// </summary>
        public IEnumerable<DocumentedMember<FieldInfo>> Fields { get; set; }

        /// <summary>
        /// Contains the documentation for all methods on the <see cref="Type"/>.
        /// </summary>
        public IEnumerable<DocumentedMember<MethodInfo>> Methods { get; set; }

        /// <summary>
        /// Contains the documentation for all nested types.
        /// </summary>
        public IEnumerable<DocumentedType> NestedTypes { get; set; }

        /// <summary>
        /// Contains the documentation for all properties on the <see cref="Type"/>.
        /// </summary>
        public IEnumerable<DocumentedMember<PropertyInfo>> Properties { get; set; }

        /// <summary>
        /// Initializes a new <see cref="DocumentedType"/>.
        /// </summary>
        public DocumentedType() { }

        /// <summary>
        /// Initializes a new <see cref="DocumentedType"/>.
        /// </summary>
        /// <param name="constructors">The documentation for all constructors on the <see cref="Type"/>.</param>
        /// <param name="events">The documentation for all events on the <see cref="Type"/>.</param>
        /// <param name="fields">The documentation for all fields on the <see cref="Type"/>.</param>
        /// <param name="methods">The documentation for all methods on the <see cref="Type"/>.</param>
        /// <param name="nestedTypes">The documentation for all nested types.</param>
        /// <param name="properties">Tontains the documentation for all properties on the <see cref="Type"/>.</param>
        /// <param name="member">The member that is being documented.</param>
        /// <param name="xml">The documentation Xml.</param>
        public DocumentedType(
                    Type member,
                    XElement xml,
                    IEnumerable<DocumentedMember<ConstructorInfo>> constructors,
                    IEnumerable<DocumentedMember<EventInfo>> events,
                    IEnumerable<DocumentedMember<FieldInfo>> fields,
                    IEnumerable<DocumentedMember<MethodInfo>> methods,
                    IEnumerable<DocumentedType> nestedTypes,
                    IEnumerable<DocumentedMember<PropertyInfo>> properties
                )
            : base(member, xml)
        {
            Contract.Requires<ArgumentNullException>(member != null && xml != null);
            Contract.Requires<ArgumentNullException>(
                constructors != null &&
                events != null &&
                fields != null &&
                methods != null &&
                nestedTypes != null &&
                properties != null
            );

            this.Constructors = constructors;
            this.Events = events;
            this.Fields = fields;
            this.Methods = methods;
            this.NestedTypes = nestedTypes;
            this.Properties = properties;
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        IEnumerator<DocumentedMember<ConstructorInfo>> IEnumerable<DocumentedMember<ConstructorInfo>>.GetEnumerator()
        {
            return (this.Constructors ?? Enumerable.Empty<DocumentedMember<ConstructorInfo>>()).GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        IEnumerator<DocumentedMember<EventInfo>> IEnumerable<DocumentedMember<EventInfo>>.GetEnumerator()
        {
            return (this.Events ?? Enumerable.Empty<DocumentedMember<EventInfo>>()).GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        IEnumerator<DocumentedMember<FieldInfo>> IEnumerable<DocumentedMember<FieldInfo>>.GetEnumerator()
        {
            return (this.Fields ?? Enumerable.Empty<DocumentedMember<FieldInfo>>()).GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        IEnumerator<DocumentedMember<MethodInfo>> IEnumerable<DocumentedMember<MethodInfo>>.GetEnumerator()
        {
            return (this.Methods ?? Enumerable.Empty<DocumentedMember<MethodInfo>>()).GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<DocumentedType> GetEnumerator()
        {
            return (this.NestedTypes ?? Enumerable.Empty<DocumentedType>()).GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (this.NestedTypes ?? Enumerable.Empty<DocumentedType>()).GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        IEnumerator<DocumentedMember<PropertyInfo>> IEnumerable<DocumentedMember<PropertyInfo>>.GetEnumerator()
        {
            return (this.Properties ?? Enumerable.Empty<DocumentedMember<PropertyInfo>>()).GetEnumerator();
        }
    }
}
