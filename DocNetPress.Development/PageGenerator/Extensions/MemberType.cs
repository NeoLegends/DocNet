using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Lists the main types available in .NET documentation
    /// </summary>
    [Serializable]
    public enum MemberType
    {
        Type,

        Method,

        Property,

        Field,
        
        Namespace,

        Event
    }
}
