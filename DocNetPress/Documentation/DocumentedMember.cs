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
    public struct DocumentedMember
    {
        /// <summary>
        /// The documentation Xml.
        /// </summary>
        public XElement Xml { get; private set; }

        /// <summary>
        /// The member to be documented.
        /// </summary>
        public MemberInfo Member { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="DocumentedMember"/>.
        /// </summary>
        /// <param name="member">The <see cref="MemberInfo"/> that is being documented.</param>
        /// <param name="xml">The documentation Xml.</param>
        public DocumentedMember(MemberInfo member, XElement xml)
            : this()
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
}
