using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.Development.XmlRpc.Media
{
    [Serializable]
    public struct Sizes
    {
        [XmlRpcMember("thumbnail")]
        public MediaItemSize Thumbnail;

        [XmlRpcMember("medium")]
        public MediaItemSize Medium;

        [XmlRpcMember("large")]
        public MediaItemSize Large;
    }
}
