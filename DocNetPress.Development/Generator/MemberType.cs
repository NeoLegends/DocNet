using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.Generator
{
    /// <summary>
    /// Lists the types of documentation members
    /// </summary>
    public enum MemberType
    {
        /// <summary>
        /// The given documentation member is a namespace
        /// </summary>
        Namespace,

        /// <summary>
        /// The given documentation member is a type (Class, Interface, Enumeration, Delegate)
        /// </summary>
        Type,

        /// <summary>
        /// The given documentation member is a field
        /// </summary>
        Field,

        /// <summary>
        /// The given documentation member is a property (including indexers)
        /// </summary>
        Property,

        /// <summary>
        /// The given documentation property is a method
        /// </summary>
        Method,

        /// <summary>
        /// The given documentation property is an event
        /// </summary>
        Event,

        /// <summary>
        /// Errors
        /// </summary>
        Error
    }
}
