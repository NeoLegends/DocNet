using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.XmlRpc.Taxonomies
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
        public String Name;

        /// <summary>
        /// The term's slug
        /// </summary>
        [XmlRpcMember("slug")]
        public String Slug;

        /// <summary>
        /// The term's taxonomy
        /// </summary>
        [XmlRpcMember("taxonomy")]
        public String Taxonomy;

        /// <summary>
        /// The term's description
        /// </summary>
        [XmlRpcMember("description")]
        public String Description;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Term() { }
    }
}
