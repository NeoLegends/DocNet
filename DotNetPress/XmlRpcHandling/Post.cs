using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DotNetPress.XmlRpcHandling
{
    [Serializable]
    public class Post
    {
        [XmlRpcMember("post_id")]
        public String ID;
        
        [XmlRpcMember("post_parent")]
        public String Parent;

        [XmlRpcMember("post_modified")]
        public DateTime Modified;

        [XmlRpcMember("post_modified_gmt")]
        public DateTime ModifiedGmt;
        
        [XmlRpcMember("post_mime_type")]
        public String MimeType;
        
        [XmlRpcMember("link")]
        public String Link;
        
        [XmlRpcMember("guid")]
        public String Guid;
        
        [XmlRpcMember("menu_order")]
        public int MenuOrder;
        
        [XmlRpcMember("comment_status")]
        public String CommentStatus;
        
        [XmlRpcMember("ping_status")]
        public String PingStatus;
        
        [XmlRpcMember("sticky")]
        public bool IsSticky;

        [XmlRpcMember("custom_fields")]
        public CustomField[] CustomFields;

        [XmlRpcMember("enclosure")]
        public Enclosure Enclosure;

        [XmlRpcMember("terms")]
        public Term[] Terms;

        public Post() { }
    }
}
