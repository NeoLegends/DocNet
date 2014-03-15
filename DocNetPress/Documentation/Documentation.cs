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
    /// Contains all the documentation for a specified assembly.
    /// </summary>
    public class Documentation : IEnumerable<DocumentedMember>
    {
        /// <summary>
        /// Gets the assembly's name.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Gets all documented members in the assembly.
        /// </summary>
        public IEnumerable<DocumentedMember> Members { get; private set; }

        /// <summary>
        /// Gets the path to the documentation file.
        /// </summary>
        public String DocumentationPath { get; private set; }

        /// <summary>
        /// Gets the amount of stored <see cref="DocumentedMember"/>s.
        /// </summary>
        public int Count
        {
            get
            {
                return this.Members.Count();
            }
        }

        /// <summary>
        /// Initializes a new <see cref="Documentation"/>.
        /// </summary>
        /// <param name="assembly">The documented assembly.</param>
        /// <param name="documentationPath">The path to the documentation file.</param>
        /// <param name="members">All documented members in the assembly.</param>
        public Documentation(Assembly assembly, String documentationPath, IEnumerable<DocumentedMember> members)
        {
            Contract.Requires<ArgumentNullException>(assembly != null && documentationPath != null && members != null);

            this.Assembly = assembly;
            this.DocumentationPath = documentationPath;
            this.Members = members;
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<DocumentedMember> GetEnumerator()
        {
            return this.Members.GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="System.Collections.IEnumerator"/>.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerator"/>.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Members.GetEnumerator();
        }

        /// <summary>
        /// Contains Contract.Invariant definitions.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Assembly != null);
            Contract.Invariant(this.Members != null);
            Contract.Invariant(this.DocumentationPath != null);
        }
    }
}
