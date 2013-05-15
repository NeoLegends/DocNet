using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Media
{
    [Serializable]
    public struct MediaItemUploadData
    {
        [XmlRpcMember("name")]
        public String Name;

        [XmlRpcMember("type")]
        public String MimeType;

        [XmlRpcMember("bits")]
        public String Bits;

        [XmlRpcMember("overwrite")]
        public bool IsOverwriting;
    }
}
