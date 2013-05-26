using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.Development.XmlRpc.Media
{
    [Serializable]
    public class MediaItem
    {
        [XmlRpcMember("title")]
        public String Title;

        [XmlRpcMember("link")]
        public String Link;

        [XmlRpcMember("caption")]
        public String Caption;

        [XmlRpcMember("description")]
        public String Description;

        [XmlRpcMember("date_created_gmt")]
        public DateTime DateGMT;

        [XmlRpcMember("attachment_id")]
        public String AttachmentID;

        [XmlRpcMember("parent")]
        public int Parent;

        [XmlRpcMember("thumbnail")]
        public String Thumbnail;

        [XmlRpcMember("metadata")]
        public MediaItemMetadata Metadata;

        [XmlRpcMember("image_meta")]
        public PostThumbnailImageMeta ImageMeta;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MediaItem() { }
    }
}
