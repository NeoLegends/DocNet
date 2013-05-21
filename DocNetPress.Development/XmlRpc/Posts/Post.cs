using CookComputing.XmlRpc;
using DocNetPress.Development.Development.XmlRpc.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.XmlRpc.Posts
{
    /// <summary>
    /// A general WordPress post class
    /// </summary>
    [Serializable]
    public class Post
    {
        /// <summary>
        /// The post's title
        /// </summary>
        [XmlRpcMember("post_title")]
        public String Title;

        /// <summary>
        /// The post's name
        /// </summary>
        [XmlRpcMember("post_name")]
        public String Name;

        /// <summary>
        /// The author of the post
        /// </summary>
        [XmlRpcMember("post_author")]
        public String Author;

        /// <summary>
        /// The post's content
        /// </summary>
        [XmlRpcMember("post_content")]
        public String Content;

        /// <summary>
        /// The <see cref="System.DateTime"/> the post was created
        /// </summary>
        [XmlRpcMember("post_date")]
        public DateTime Date;

        /// <summary>
        /// The <see cref="System.DateTime"/> the post was created in GMT
        /// </summary>
        [XmlRpcMember("post_date_gmt")]
        public DateTime DateGmt;

        /// <summary>
        /// If it is password protected, the posts password
        /// </summary>
        [XmlRpcMember("post_password")]
        public String Password;

        /// <summary>
        /// The post's status
        /// </summary>
        [XmlRpcMember("post_status")]
        public String Status;

        /// <summary>
        /// The post type (page, post)
        /// </summary>
        [XmlRpcMember("post_type")]
        public String Type;

        [XmlRpcMember("post_format")]
        public String Format;

        [XmlRpcMember("post_excerpt")]
        public String Excerpt;

        [XmlRpcMember("comment_status")]
        public String CommentState;

        /// <summary>
        /// Whether the post is sticky and shall be kept at the top or not
        /// </summary>
        [XmlRpcMember("sticky")]
        public bool IsSticky;

        [XmlRpcMember("enclosure")]
        public Enclosure Enclosure;

        /// <summary>
        /// All custom fields
        /// </summary>
        [XmlRpcMember("custom_fields")]
        public CustomField[] CustomFields;

        /// <summary>
        /// The post's thumbnail
        /// </summary>
        [XmlRpcMember("post_thumbnail")]
        public int Thumbnail;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Post() { }
    }
}
