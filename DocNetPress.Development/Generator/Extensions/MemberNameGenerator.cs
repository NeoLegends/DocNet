using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Provides methods for easy dealing with member names
    /// </summary>
    public static class MemberNameGenerator
    {
        /// <summary>
        /// Gets the short member name of a given full member name
        /// </summary>
        /// <param name="fullMemberName">The full member name</param>
        /// <returns>The short member name</returns>
        public static String GetShortMemberName(String fullMemberName)
        {
            return fullMemberName.Split('.', '#').Last();
        }

        /// <summary>
        /// Gets the short member name of a given full member name without any parameters
        /// </summary>
        /// <param name="fullMemberName">The full member name</param>
        /// <returns>The short member name</returns>
        public static String GetShortMemberNameWithoutParameters(String fullMemberName)
        {
            if (fullMemberName.Contains('('))
                return GetShortMemberName(fullMemberName.Split('(')[0]);
            else
                return GetShortMemberName(fullMemberName);
        }

        /// <summary>
        /// Gets the parent element name of a given member name
        /// </summary>
        /// <param name="fullMemberName">The full member name to get the containing element from</param>
        /// <returns>The extracted parent element name</returns>
        /// <example>
        /// Console.WriteLine(TypeExtractor.GetParentMemnerNameElement("System.String"); -> Prints "System"
        /// </example>
        public static String GetParentMemberNameElement(String fullMemberName)
        {
            String[] parts = fullMemberName.Split('.', '#');
            StringBuilder result = new StringBuilder(125);
            for (int i = 0; i < parts.Length - 1; i++)
                result.Append(parts[i]);
            return result.ToString();
        }
    }
}
