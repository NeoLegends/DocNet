using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Posts
{
    [Serializable]
    public struct PostFilter
    {
        [XmlRpcMember("post_type")]
        public String PostType;
        
        [XmlRpcMember("post_status")]
        public String PostStatus;

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
