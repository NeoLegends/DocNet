using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace DocNetPress.XmlRpc.Posts
{
    [Serializable]
    public struct CustomFieldWithID
    {
        [XmlRpcMember("id")]
        public String ID;

        [XmlRpcMember("key")]
        public String Key;

        [XmlRpcMember("value")]
        public String Value;
    }

    [Serializable]
    public struct CustomField
    {
        [XmlRpcMember("key")]
        public String Key;

        [XmlRpcMember("value")]
        public String Value;
    }
}
