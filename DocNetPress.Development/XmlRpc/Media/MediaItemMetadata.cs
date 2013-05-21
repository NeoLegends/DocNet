using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.Development.XmlRpc.Media
{
    [Serializable]
    public struct MediaItemMetadata
    {
        [XmlRpcMember("width")]
        public int Width;

        [XmlRpcMember("height")]
        public int Height;

        [XmlRpcMember("file")]
        public String File;

        [XmlRpcMember("sizes")]
        public MediaItemSize Sizes;
    }
}
