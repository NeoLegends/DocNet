using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DotNetPress.XmlRpcHandling
{
    [Serializable]
    public struct CustomField
    {
        [XmlRpcMember("id")]
        public String ID;

        [XmlRpcMember("name")]
        public String Name;

        [XmlRpcMember("value")]
        public String Value;
    }
}
