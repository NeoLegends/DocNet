using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DocNetPress.Development.PageGenerator.Extensions
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

        /// <summary>
        /// Generates the member signature from the given assembly, the full member name and it's <see cref="System.Reflection.MemberTypes"/>
        /// </summary>
        /// <param name="assemblyPath">The assembly to extract the member signature from</param>
        /// <param name="fullMemberName">The full member name</param>
        /// <param name="memberTypeInformation"> Gives the current member type we're dealing with</param>
        /// <returns></returns>
        public static String GenerateMemberSignature(String assemblyPath, String fullMemberName, TypeInformation memberTypeInformation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the propery signature from the given assembly and the full property name
        /// </summary>
        /// <param name="assemblyPath">The assembly to extract the property signature from</param>
        /// <param name="fullPropertyName">The full property name</param>
        /// <returns>The full property signature with the short property name</returns>
        private static String GeneratePropertySignature(String assemblyPath, String fullPropertyName)
        {
            String shortPropertyName = MemberNameGenerator.GetShortMemberName(fullPropertyName);
            Type type = Assembly.LoadFrom(assemblyPath).GetTypes().FirstOrDefault(t => t.FullName == MemberNameGenerator.GetParentMemberNameElement(fullPropertyName));
            PropertyInfo pInfo = type.GetProperty(shortPropertyName);

            if (pInfo.CanRead && !pInfo.CanWrite)
                return shortPropertyName + " { get; }";
            else if (!pInfo.CanRead && pInfo.CanWrite)
                return shortPropertyName + " { set; }";
            else if (pInfo.CanRead && pInfo.CanWrite)
                return shortPropertyName + " { get; set; }";
            else
                return null;
        }
    }
}
