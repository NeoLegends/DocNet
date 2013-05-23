using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Gives full information about a given type
    /// </summary>
    public struct TypeInformation
    {
        public MemberType MemberType { get; set; }

        public TypeMembers TypeMember { get; set; }
    }
}
