using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPress.XmlRpcHandling
{
    public class PostContent
    {
        [XmlRpcMember("post_title")]
        public String Title;

        [XmlRpcMember("post_date")]
        public DateTime Date;

        [XmlRpcMember("post_date_gmt")]
        public DateTime DateGmt;

        [XmlRpcMember("post_status")]
        public String Status;

        [XmlRpcMember("post_type")]
        public String Type;

        [XmlRpcMember("post_format")]
        public String Format;

        [XmlRpcMember("post_name")]
        public String Name;

        [XmlRpcMember("post_author")]
        public String Author;

        [XmlRpcMember("post_password")]
        public String Password;

        [XmlRpcMember("post_excerpt")]
        public String Excerpt;

        [XmlRpcMember("post_content")]
        public String Content;
    }
}
