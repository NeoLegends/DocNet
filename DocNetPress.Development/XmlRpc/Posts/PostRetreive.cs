using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;
using DocNetPress.XmlRpc.Taxonomies;
using DocNetPress.XmlRpc.Media;

namespace DocNetPress.XmlRpc.Posts
{
    /// <summary>
    /// Represents a post as it is being retreived from the WordPress-Installation
    /// </summary>
    [Serializable]
    public class PostRetreive : Post
    {
        /// <summary>
        /// The unique Post-ID
        /// </summary>
        [XmlRpcMember("post_id")]
        public String ID;
        
        /// <summary>
        /// The parent Post
        /// </summary>
        [XmlRpcMember("post_parent")]
        public String Parent;

        /// <summary>
        /// The last time the post was modified
        /// </summary>
        [XmlRpcMember("post_modified")]
        public DateTime Modified;

        /// <summary>
        /// The last time the post was modified in GMT
        /// </summary>
        [XmlRpcMember("post_modified_gmt")]
        public DateTime ModifiedGmt;
        
        [XmlRpcMember("post_mime_type")]
        public String MimeType;
        
        /// <summary>
        /// The link to the post
        /// </summary>
        [XmlRpcMember("link")]
        public String Link;
        
        [XmlRpcMember("guid")]
        public String Guid;
        
        [XmlRpcMember("menu_order")]
        public int MenuOrder;
        
        [XmlRpcMember("ping_status")]
        public String PingStatus;

        /// <summary>
        /// All post terms
        /// </summary>
        [XmlRpcMember("terms")]
        public Taxonomy[] Terms;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public PostRetreive() { }
    }
}
