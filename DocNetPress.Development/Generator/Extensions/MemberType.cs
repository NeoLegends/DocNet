using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Lists the prefix types ("T", "M", "P", "F", "N", "E") available in .NET documentation
    /// </summary>
    [Serializable]
    public enum MemberType
    {
        /// <summary>
        /// The documentation member is a type
        /// </summary>
        Type,

        /// <summary>
        /// The documentation member is a method
        /// </summary>
        Method,

        /// <summary>
        /// The documentation member is a property
        /// </summary>
        Property,

        /// <summary>
        /// The documentation member is a field
        /// </summary>
        Field,

        /// <summary>
        /// The documentation member is a namespace
        /// </summary>
        Namespace,

        /// <summary>
        /// The documentation member is an event
        /// </summary>
        Event
    }
}
