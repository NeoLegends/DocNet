using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.XmlRpc.Posts
{
    [Serializable]
    public class PostInsert : Post
    {
        [XmlRpcMember("comment_status")]
        public String CommentState;

        [XmlRpcMember("ping_status")]
        public String PingState;

        [XmlRpcMember("post_parent")]
        public int Parent;

        [XmlRpcMember("post_thumbnail")]
        public int Thumbnail;

        [XmlRpcMember("custom_fields")]
        public CustomField[] CustomFields;
    }
}
