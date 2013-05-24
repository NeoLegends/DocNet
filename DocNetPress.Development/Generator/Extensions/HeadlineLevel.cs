using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Lists all available headline sizes / levels
    /// </summary>
    [Serializable]
    public enum HeadlineLevel
    {
        /// <summary>
        /// Largest headline level
        /// </summary>
        h1,

        /// <summary>
        /// Second largest headline level
        /// </summary>
        h2,

        /// <summary>
        /// Medium headline level
        /// </summary>
        h3,

        /// <summary>
        /// Medium headline level
        /// </summary>
        h4,

        /// <summary>
        /// Second smallest healine level
        /// </summary>
        h5,

        /// <summary>
        /// Smallest headline level
        /// </summary>
        h6
    }
}
