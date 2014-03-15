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
    public class Documentation
    {
        /// <summary>
        /// Gets the assembly's name.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Gets the path to the documentation file.
        /// </summary>
        public String DocumentationPath { get; private set; }

        /// <summary>
        /// Gets all documented namespaces in the assembly.
        /// </summary>
        public IEnumerable<Namespace> Namespaces { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="Documentation"/>.
        /// </summary>
        /// <param name="assembly">The documented assembly.</param>
        /// <param name="documentationPath">The path to the documentation file.</param>
        /// <param name="namespaces">All namespaces in the assembly.</param>
        public Documentation(Assembly assembly, String documentationPath, IEnumerable<Namespace> namespaces)
        {
            Contract.Requires<ArgumentNullException>(assembly != null && documentationPath != null && namespaces != null);

            this.Assembly = assembly;
            this.DocumentationPath = documentationPath;
            this.Namespaces = namespaces;
        }

        /// <summary>
        /// Contains Contract.Invariant definitions.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Assembly != null);
            Contract.Invariant(this.Namespaces != null);
            Contract.Invariant(this.DocumentationPath != null);
        }

        /// <summary>
        /// Creates a new <see cref="Documentation"/> from a list of <see cref="DocumentedMember"/>s.
        /// </summary>
        /// <param name="assembly">The documented assembly.</param>
        /// <param name="documentationPath">The path to the documentation file.</param>
        /// <param name="members">A list of <see cref="DocumentedMember"/>s to transform into a namespace-hierarchy.</param>
        public static async Task<Documentation> FromMembers(Assembly assembly, String documentationPath, IEnumerable<DocumentedMember> members)
        {
            Contract.Requires<ArgumentNullException>(assembly != null && documentationPath != null && members != null);

            throw new NotImplementedException();
        }
    }
}
