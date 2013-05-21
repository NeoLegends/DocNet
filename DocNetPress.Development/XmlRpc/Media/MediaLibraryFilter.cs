using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.XmlRpc.Media
{
    [Serializable]
    public struct MediaLibraryFilter
    {
        [XmlRpcMember("number")]
        public int Number;

        [XmlRpcMember("offset")]
        public int Offset;

        [XmlRpcMember("parent_id")]
        public int ParentID;

        [XmlRpcMember("mime-type")]
        public String MimeType;
    }
}
