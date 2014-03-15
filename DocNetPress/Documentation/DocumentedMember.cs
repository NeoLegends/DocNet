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
    /// Contains the documentation for a member.
    /// </summary>
    public class DocumentedMember
    {
        /// <summary>
        /// The documentation Xml.
        /// </summary>
        public XElement Xml { get; protected set; }

        /// <summary>
        /// The member to be documented.
        /// </summary>
        public MemberInfo Member { get; protected set; }

        /// <summary>
        /// Initializes a new <see cref="DocumentedMember"/>.
        /// </summary>
        /// <param name="member">The <see cref="MemberInfo"/> that is being documented.</param>
        /// <param name="xml">The documentation Xml.</param>
        public DocumentedMember(MemberInfo member, XElement xml)
        {
            Contract.Requires<ArgumentNullException>(member != null && xml != null);

            this.Member = member;
            this.Xml = xml;
        }

        /// <summary>
        /// Contains Contract.Invariant definitions.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Member != null);
            Contract.Invariant(this.Xml != null);
        }
    }

    /// <summary>
    /// Contains the documentation for a member.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of documented member.</typeparam>
    public class DocumentedMember<T> : DocumentedMember
        where T : MemberInfo
    {
        /// <summary>
        /// Gets the documented member.
        /// </summary>
        public new T Member
        {
            get
            {
                return (T)base.Member;
            }
            protected set
            {
                base.Member = value;
            }
        }

        /// <summary>
        /// Initializes a new <see cref="DocumentedMember{T}"/>.
        /// </summary>
        /// <param name="member">The member that is being documented.</param>
        /// <param name="xml">The documentation Xml.</param>
        public DocumentedMember(T member, XElement xml)
            : base(member, xml)
        {
            Contract.Requires<ArgumentNullException>(member != null && xml != null);
        }

        /// <summary>
        /// Contains Contract.Invariant definitions.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Member != null);
        }
    }
}
