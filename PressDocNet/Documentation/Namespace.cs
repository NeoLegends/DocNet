using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressDocNet.Documentation
{
    /// <summary>
    /// Represents a namespace.
    /// </summary>
    public class Namespace
    {
        /// <summary>
        /// The namespace's name.
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets all sub namespaces.
        /// </summary>
        public List<Namespace> Namespaces { get; protected set; }

        /// <summary>
        /// The <see cref="Type"/>s defined in the namespace.
        /// </summary>
        public List<DocumentedType> Types { get; protected set; }

        /// <summary>
        /// Initializes a new <see cref="Namespace"/>.
        /// </summary>
        /// <param name="name">The namespace's name.</param>
        /// <param name="namespaces">Gets all sub namespaces.</param>
        /// <param name="types">The <see cref="Type"/>s directly defined in the namespace.</param>
        public Namespace(String name, IEnumerable<Namespace> namespaces, IEnumerable<DocumentedType> types)
        {
            Contract.Requires<ArgumentNullException>(name != null && namespaces != null && types != null);

            this.Name = name;
            this.Namespaces = new List<Namespace>(namespaces);
            this.Types = new List<DocumentedType>(types);
        }
    }
}
