using System;
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
    /// Represents the documentation for a <see cref="Type"/>.
    /// </summary>
    public class DocumentedType : DocumentedMember<Type>
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
        /// Contains Contract.Invariant definitions.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Events != null);
            Contract.Invariant(this.Fields != null);
            Contract.Invariant(this.Methods != null);
            Contract.Invariant(this.NestedTypes != null);
            Contract.Invariant(this.Properties != null);
        }
    }
}
