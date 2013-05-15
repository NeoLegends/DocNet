using CookComputing.XmlRpc;
using DocNetPress.XmlRpc.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.XmlRpc.Taxonomies
{
    /// <summary>
    /// Represents a WordPress-Taxonomy
    /// </summary>
    [Serializable]
    public struct Taxonomy
    {
        /// <summary>
        /// The taxonomy's name
        /// </summary>
        [XmlRpcMember("name")]
        String Name;

        /// <summary>
        /// The taxonomy's label
        /// </summary>
        [XmlRpcMember("label")]
        String Label;

        /// <summary>
        /// Whether the taxonomy is hierarchially sorted or not
        /// </summary>
        [XmlRpcMember("hierarchical")]
        bool IsHierarchical;

        /// <summary>
        /// Whether the taxonomy is public or not
        /// </summary>
        [XmlRpcMember("public")]
        bool IsPublic;

        [XmlRpcMember("show_ui")]
        bool IsShowUI;

        [XmlRpcMember("_builtin")]
        bool IsBuiltin;

        [XmlRpcMember("labels")]
        CustomField Labels;

        [XmlRpcMember("cap")]
        CustomField Cap;

        [XmlRpcMember("object_type")]
        CustomField[] ObjectType;
    }
}
