using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.XmlRpc.Taxonomies
{
    [Serializable]
    public struct TermFilter
    {
        [XmlRpcMember("number")]
        public int Number;

        [XmlRpcMember("offset")]
        public int Offset;

        [XmlRpcMember("orderby")]
        public String OrderBy;

        [XmlRpcMember("order")]
        public String Order;

        [XmlRpcMember("hide_empty")]
        public bool HideEmpty;

        [XmlRpcMember("search")]
        public String Search;
    }
}
