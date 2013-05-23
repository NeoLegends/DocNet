using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Gives full information about a given type
    /// </summary>
    [Serializable]
    public struct TypeInformation
    {
        /// <summary>
        /// Gives the kind of member to document
        /// </summary>
        public MemberType MemberType { get; set; }

        /// <summary>
        /// If the <see cref="DocNetPress.Development.Generator.Extensions.TypeInformation.MemberType"/> says we're a type, this gives the kind of type we are
        /// </summary>
        public TypeMembers TypeMember { get; set; }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Development.Generator.Extensions.TypeInformation"/> struct
        /// </summary>
        /// <param name="memberType">The kind of member this struct gives information about</param>
        /// <param name="typeMember">If the member we are to document is a type, what type are we?</param>
        public TypeInformation(MemberType memberType, TypeMembers typeMember)
        {
            this.MemberType = memberType;
            this.TypeMember = typeMember;
        }
    }
}
