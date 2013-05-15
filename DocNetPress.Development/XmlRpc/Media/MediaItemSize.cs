using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Media
{
    [Serializable]
    public struct MediaItemSize
    {
        [XmlRpcMember("file")]
        public String File;

        [XmlRpcMember("width")]
        public String Width;

        [XmlRpcMember("height")]
        public String Height;

        [XmlRpcMember("mime-type")]
        public String MimeType;
    }
}
