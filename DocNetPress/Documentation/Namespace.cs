using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Documentation
{
    /// <summary>
    /// Represents a namespace.
    /// </summary>
    public class Namespace
    {
        /// <summary>
        /// The namespace's name.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Gets all sub namespaces.
        /// </summary>
        public List<Namespace> SubNamespaces { get; private set; }

        /// <summary>
        /// The <see cref="Type"/>s directly defined in the namespace.
        /// </summary>
        public List<Type> Types { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="Namespace"/>.
        /// </summary>
        /// <param name="name">The namespace's name.</param>
        /// <param name="subNamespaces">Gets all sub namespaces.</param>
        /// <param name="types">The <see cref="Type"/>s directly defined in the namespace.</param>
        public Namespace(String name, IEnumerable<Namespace> subNamespaces, IEnumerable<Type> types)
        {
            Contract.Requires<ArgumentNullException>(name != null && subNamespaces != null && types != null);

            this.Name = name;
            this.SubNamespaces = new List<Namespace>(subNamespaces);
            this.Types = new List<Type>(types);
        }
    }
}
