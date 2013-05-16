using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Generators
{
    /// <summary>
    /// Enum giving the order of the page
    /// </summary>
    public enum PageOrder
    {
        /// <summary>
        /// Standard order of page items
        /// </summary>
        /// <remarks>
        /// 1. Method / Member signature
        /// 2. Summary
        /// 3. Remarks
        /// 4. Example
        /// 5. Exceptions
        /// 6. Inheritance Hierarchy
        /// 7. See also
        /// </remarks>
        Standard,

        /// <summary>
        /// Orders the Page just like the XML file
        /// </summary>
        AsInXMLFile,

        /// <summary>
        /// Fully customized order
        /// </summary>
        /// <remarks>
        /// Give the order via <see cref="DocNetPress.Generators.ClassPagePart"/>-Enum
        /// </remarks>
        Custom
    }
}
