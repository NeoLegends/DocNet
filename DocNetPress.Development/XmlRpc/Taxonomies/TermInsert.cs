using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.XmlRpc.Taxonomies
{
    /// <summary>
    /// Contains data being sent to WordPress to create a new Term
    /// </summary>
    public class TermInsert : Term
    {
        /// <summary>
        /// The term's parent
        /// </summary>
        [XmlRpcMember("parent")]
        public int Parent;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public TermInsert() { }
    }
}
