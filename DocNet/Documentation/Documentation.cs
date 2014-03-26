using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocNet.Documentation
{
    /// <summary>
    /// Contains all the documentation for a specified assembly.
    /// </summary>
    public class Documentation : IEnumerable<DocumentedType>
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
        /// Gets all documented <see cref="Type"/>s in the assembly.
        /// </summary>
        public IEnumerable<DocumentedType> Types { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="Documentation"/>.
        /// </summary>
        /// <param name="assembly">The documented assembly.</param>
        /// <param name="documentationPath">The path to the documentation file.</param>
        /// <param name="types">All namespaces in the assembly.</param>
        public Documentation(Assembly assembly, String documentationPath, IEnumerable<DocumentedType> types)
        {
            Contract.Requires<ArgumentNullException>(assembly != null && documentationPath != null && types != null);

            this.Assembly = assembly;
            this.DocumentationPath = documentationPath;
            this.Types = types;
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<DocumentedType> GetEnumerator()
        {
            return this.Types.GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator"/>.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Types.GetEnumerator();
        }

        /// <summary>
        /// Contains Contract.Invariant definitions.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Assembly != null);
            Contract.Invariant(this.DocumentationPath != null);
            Contract.Invariant(this.Types != null);
        }
    }
}
