using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Posts
{
    /// <summary>
    /// A filter able to filter posts by given criteria
    /// </summary>
    [Serializable]
    public struct PostFilter
    {
        /// <summary>
        /// The post type to filter results by
        /// </summary>
        [XmlRpcMember("post_type")]
        public String PostType;

        /// <summary>
        /// The post type to satus results by
        /// </summary>
        [XmlRpcMember("post_status")]
        public String PostStatus;

        /// <summary>
        /// The maximum amount of posts to retreive
        /// </summary>
        [XmlRpcMember("number")]
        public int Number;

        [XmlRpcMember("offset")]
        public int Offset;

        [XmlRpcMember("orderby")]
        public String OrderBy;

        [XmlRpcMember("order")]
        public String Order;
    }
}
