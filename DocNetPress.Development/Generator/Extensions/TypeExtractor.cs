using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Provides static methods to get information about types from given assemblies
    /// </summary>
    public static class TypeExtractor
    {
        /// <summary>
        /// Checks whether a given member is a class, interface, enum, and so on
        /// </summary>
        /// <param name="fullMemberName">The full name of the member to check the type of</param>
        /// <param name="assemblyFile">The path to the assembly file to extract the return type from</param>
        /// <returns>Whether the given type is a class, interface, enum / and so on</returns>
        public static TypeInformation GetMemberType(String fullMemberName, String assemblyFile)
        {
            
        }

        /// <summary>
        /// Extracts the return type of a given method
        /// </summary>
        /// <param name="fullMethodName">The full name of the method to check the return type of</param>
        /// <param name="assemblyFile">The path to the assembly file to extract the return type from</param>
        /// <returns>The return type of the given method</returns>
        public static Type GetMethodReturnType(String fullMethodName, String assemblyFile)
        {
            foreach (Type type in Assembly.LoadFrom(assemblyFile).GetTypes())
            {
                MethodInfo method = type.GetMethod(MemberNameGenerator.GetShortMemberName(fullMethodName));
                if (method != null && type.FullName + method.Name == fullMethodName)
                    return method.ReturnType;
            }
            return null;
        }
    }
}
