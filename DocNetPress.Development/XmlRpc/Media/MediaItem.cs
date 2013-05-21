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
        String Title;

        [XmlRpcMember("link")]
        String Link;

        [XmlRpcMember("caption")]
        String Caption;

        [XmlRpcMember("description")]
        String Description;

        [XmlRpcMember("date_created_gmt")]
        DateTime DateGMT;

        [XmlRpcMember("attachment_id")]
        String AttachmentID;

        [XmlRpcMember("parent")]
        int Parent;

        [XmlRpcMember("thumbnail")]
        String Thumbnail;

        [XmlRpcMember("metadata")]
        MediaItemMetadata Metadata;

        [XmlRpcMember("image_meta")]
        PostThumbnailImageMeta ImageMeta;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MediaItem() { }
    }
}
