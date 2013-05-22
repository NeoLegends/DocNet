using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Lists the available Types in the .NET Framework
    /// </summary>
    public enum FrameworkTypes
    {
        /// <summary>
        /// The type represents a class
        /// </summary>
        Class,

        /// <summary>
        /// The type represents a struct
        /// </summary>
        Struct,

        /// <summary>
        /// The type represents an interface
        /// </summary>
        Interface,

        /// <summary>
        /// The type represents an enum
        /// </summary>
        Enum,

        /// <summary>
        /// The type represents a delegate
        /// </summary>
        Delegate
    }
}
