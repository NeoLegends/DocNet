using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions.SyntaxElement
{
    /// <summary>
    /// Lists the available outputs for code
    /// </summary>
    [Serializable]
    public enum OutputField
    {
        /// <summary>
        /// Uses crayon syntax highlighter
        /// </summary>
        Crayon,

        /// <summary>
        /// Uses the standard WordPress quotebox
        /// </summary>
        QuoteBox
    }
}
