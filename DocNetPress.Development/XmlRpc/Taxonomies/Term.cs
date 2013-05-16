using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.XmlRpc.Taxonomies
{
    /// <summary>
    /// Contains data about a WordPress-Term
    /// </summary>
    public class Term
    {
        /// <summary>
        /// The term's name
        /// </summary>
        [XmlRpcMember("name")]
        String Name;

        /// <summary>
        /// The term's slug
        /// </summary>
        [XmlRpcMember("slug")]
        String Slug;

        /// <summary>
        /// The term's taxonomy
        /// </summary>
        [XmlRpcMember("taxonomy")]
        String Taxonomy;

        /// <summary>
        /// The term's description
        /// </summary>
        [XmlRpcMember("description")]
        String Description;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Term() { }
    }
}
