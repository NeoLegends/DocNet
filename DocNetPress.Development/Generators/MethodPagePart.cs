using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Generators
{
    /// <summary>
    /// Enum listing all method page parts
    /// </summary>
    public enum MethodPagePart
    {
        /// <summary>
        /// Summary
        /// </summary>
        Summary,

        /// <summary>
        /// Method / Property syntax
        /// </summary>
        Syntax,

        /// <summary>
        /// Remarks
        /// </summary>
        Remarks,

        /// <summary>
        /// Example code
        /// </summary>
        Example,

        /// <summary>
        /// Exceptions
        /// </summary>
        Exceptions,

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
