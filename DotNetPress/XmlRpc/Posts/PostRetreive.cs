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
    [Serializable]
    public class PostRetreive : Post
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
        
        [XmlRpcMember("ping_status")]
        public String PingStatus;

        [XmlRpcMember("custom_fields")]
        public CustomFieldWithID[] CustomFields;

        [XmlRpcMember("terms")]
        public Taxonomy[] Terms;

        [XmlRpcMember("post_thumbnail")]
        public MediaItem Thumbnail;
    }
}
