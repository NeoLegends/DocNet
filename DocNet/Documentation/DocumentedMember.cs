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
    /// Contains the documentation for a member.
    /// </summary>
    public class DocumentedMember
    {
        /// <summary>
        /// The generated documentation.
        /// </summary>
        public String DocumentationText { get; set; }

        /// <summary>
        /// The member to be documented.
        /// </summary>
        public MemberInfo Member { get; set; }

        /// <summary>
        /// The documentation Xml.
        /// </summary>
        public XElement Xml { get; set; }

        /// <summary>
        /// Initializes a new <see cref="DocumentedMember"/>.
        /// </summary>
        public DocumentedMember() { }

        /// <summary>
        /// Initializes a new <see cref="DocumentedMember"/>.
        /// </summary>
        /// <param name="member">The <see cref="MemberInfo"/> that is being documented.</param>
        /// <param name="xml">The documentation Xml.</param>
        public DocumentedMember(MemberInfo member, XElement xml)
        {
            this.Member = member;
            this.Xml = xml;
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
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);

                base.Member = value;
            }
        }

        /// <summary>
        /// Initializes a new <see cref="DocumentedMember{T}"/>.
        /// </summary>
        public DocumentedMember() { }

        /// <summary>
        /// Initializes a new <see cref="DocumentedMember{T}"/>.
        /// </summary>
        /// <param name="member">The member that is being documented.</param>
        /// <param name="xml">The documentation Xml.</param>
        public DocumentedMember(T member, XElement xml) : base(member, xml) { }
    }
}
