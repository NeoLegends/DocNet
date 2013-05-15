using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Media
{
    [Serializable]
    public struct PostThumbnailImageMeta
    {
        [XmlRpcMember("title")]
        public String Title;

        [XmlRpcMember("aperture")]
        public int Aperture;

        [XmlRpcMember("credit")]
        public String Credit;

        [XmlRpcMember("camera")]
        public String Camera;

        [XmlRpcMember("caption")]
        public String Caption;

        [XmlRpcMember("created_timestamp")]
        public int CreatedTimestamp;

        [XmlRpcMember("copyright")]
        public String Copyright;

        [XmlRpcMember("focal_length")]
        public int FocalLength;

        [XmlRpcMember("iso")]
        public int ISO;

        [XmlRpcMember("shutter_speed")]
        public int ShutterSpeed;
    }
}
