using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.Development.XmlRpc.Posts
{
    /// <summary>
    /// Represents a custom post field
    /// </summary>
    [Serializable]
    public struct CustomField
    {
        /// <summary>
        /// The fields name
        /// </summary>
        [XmlRpcMember("key")]
        public String Key;

        /// <summary>
        /// The fields value
        /// </summary>
        [XmlRpcMember("value")]
        public String Value;
    }

    /// <summary>
    /// Represents a custom post field with a unique ID
    /// </summary>
    [Serializable]
    public struct CustomFieldWithID
    {
        /// <summary>
        /// The uniqie field ID
        /// </summary>
        [XmlRpcMember("id")]
        public String ID;

        /// <summary>
        /// The fields name
        /// </summary>
        [XmlRpcMember("key")]
        public String Key;

        /// <summary>
        /// The fields value
        /// </summary>
        [XmlRpcMember("value")]
        public String Value;
    }
}
