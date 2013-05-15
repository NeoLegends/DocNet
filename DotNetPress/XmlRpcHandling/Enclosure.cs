using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DotNetPress.XmlRpcHandling
{
    [Serializable]
    public struct Enclosure
    {
        [XmlRpcMember("url")]
        public String Url;

        [XmlRpcMember("length")]
        public int Length;

        [XmlRpcMember("type")]
        public String Type;
    }
}
