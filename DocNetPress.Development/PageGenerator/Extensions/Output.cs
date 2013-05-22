using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Lists the available outputs for code
    /// </summary>
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

    /// <summary>
    /// Lists the available output languages for code
    /// </summary>
    public enum OutputLanguage
    {
        /// <summary>
        /// Generated code will be outputted as C#
        /// </summary>
        CSharp,

        /// <summary>
        /// Generated code will be outputted as VB.NET
        /// </summary>
        VisualBasic
    }
}
