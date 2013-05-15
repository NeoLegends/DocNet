using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.XmlRpc.Media
{
    [Serializable]
    public struct MediaItemUploadResult
    {
        [XmlRpcMember("id")]
        public String ID;

        [XmlRpcMember("file")]
        public String File;

        [XmlRpcMember("url")]
        public String Url;

        [XmlRpcMember("type")]
        public String MimeType;
    }
}
