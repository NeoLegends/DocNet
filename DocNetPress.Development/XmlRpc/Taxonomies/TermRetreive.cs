using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.Development.XmlRpc.Taxonomies
{
    /// <summary>
    /// Contains the retreived data when calling WordPress
    /// </summary>
    [Serializable]
    public class TermRetreive : Term
    {
        /// <summary>
        /// The term's unique ID
        /// </summary>
        [XmlRpcMember("term_id")]
        String ID;

        /// <summary>
        /// The term's group
        /// </summary>
        [XmlRpcMember("term_group")]
        String Group;

        /// <summary>
        /// The ID of the term's taxonomy
        /// </summary>
        [XmlRpcMember("term_taxonomy_id")]
        String TaxonomyID;

        /// <summary>
        /// The term's parent
        /// </summary>
        [XmlRpcMember("parent")]
        String Parent;

        [XmlRpcMember("count")]
        int Count;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public TermRetreive() { }
    }
}
