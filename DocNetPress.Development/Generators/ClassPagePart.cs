using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Generators
{
    /// <summary>
    /// Enum listing all class page parts
    /// </summary>
    public enum ClassPagePart
    {
        /// <summary>
        /// Summary
        /// </summary>
        Summary,

        /// <summary>
        /// The classes inheritance hierarchy
        /// </summary>
        InheritanceHierarchy,

        /// <summary>
        /// Remarks
        /// </summary>
        Remarks,

        /// <summary>
        /// Example code
        /// </summary>
        Example,

        /// <summary>
        /// See also
        /// </summary>
        SeeAlso,

        /// <summary>
        /// All custom documentation tags
        /// </summary>
        CustomTags
    }
}
