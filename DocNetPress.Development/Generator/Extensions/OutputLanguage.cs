using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Lists all languages to output code in
    /// </summary>
    [Serializable]
    public enum OutputLanguage
    {
        /// <summary>
        /// All generated code shall be outputted in C#
        /// </summary>
        CSharp,

        /// <summary>
        /// All generated code shall be outputted in VB.NET
        /// </summary>
        VBNET,

        /// <summary>
        /// All generated code shall be outputted in F#
        /// </summary>
        FSharp,

        /// <summary>
        /// All generated code shall be outputted in JScript
        /// </summary>
        JScript
    }
}
