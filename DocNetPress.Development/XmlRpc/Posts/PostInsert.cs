using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.XmlRpc.Posts
{
    /// <summary>
    /// Represents a post as it is being entered into a WordPress-Database
    /// </summary>
    [Serializable]
    public class PostInsert : Post
    {
        [XmlRpcMember("comment_status")]
        public String CommentState;

        [XmlRpcMember("ping_status")]
        public String PingState;

        /// <summary>
        /// The parent's post ID
        /// </summary>
        [XmlRpcMember("post_parent")]
        public int Parent;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public PostInsert() { }
    }
}
