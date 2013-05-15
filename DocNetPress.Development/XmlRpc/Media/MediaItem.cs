using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Media
{
    [Serializable]
    public class MediaItem
    {
        [XmlRpcMember("title")]
        string Title;

        [XmlRpcMember("link")]
        string Link;

        [XmlRpcMember("caption")]
        string Caption;

        [XmlRpcMember("description")]
        string Description;

        [XmlRpcMember("date_created_gmt")]
        DateTime DateGMT;

        [XmlRpcMember("attachment_id")]
        string AttachmentID;

        [XmlRpcMember("parent")]
        int Parent;

        [XmlRpcMember("thumbnail")]
        String Thumbnail;

        [XmlRpcMember("metadata")]
        MediaItemMetadata Metadata;

        [XmlRpcMember("image_meta")]
        PostThumbnailImageMeta ImageMeta;
    }
}
