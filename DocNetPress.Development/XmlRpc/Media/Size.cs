using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Media
{
    [Serializable]
    public struct Sizes
    {
        [XmlRpcMember("thumbnail")]
        MediaItemSize Thumbnail;

        [XmlRpcMember("medium")]
        MediaItemSize Medium;

        [XmlRpcMember("large")]
        MediaItemSize Large;
    }
}
