using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Provides static methods to get information about types from given assemblies
    /// </summary>
    public static class TypeExtractor
    {
        /// <summary>
        /// Extracts the return type of a given method
        /// </summary>
        /// <param name="methodFullName">The full name of the method to check the return type of</param>
        /// <param name="assemblyFile">The path to the assembly file to extract the return type from</param>
        /// <returns>The return type of the given method</returns>
        public System.Type GetMethodReturnType(String methodFullName, String assemblyFile)
        {
            foreach (System.Type type in Assembly.LoadFrom(assemblyFile).GetTypes())
            {
                MethodInfo method = type.GetMethod(methodFullName.Split('.').Last());
                if ((type.FullName + method.Name) == methodFullName)
                {
                    return method.ReturnType;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks whether a given member is a class, interface, enum, and so on
        /// </summary>
        /// <param name="memberFullName">The full name of the member to check the type of</param>
        /// <param name="assemblyFile">The path to the assembly file to extract the return type from</param>
        /// <returns>Whether the given type is a class, interface, enum / and so on</returns>
        public static Type GetMemberType(String memberFullName, String assemblyFile)
        {
            System.Type t = Assembly.LoadFrom(assemblyFile).GetType(memberFullName);
            if (t.IsClass)
                return Type.Class;
            else if (t.IsEnum)
                return Type.Enum;
            else if (t.IsInterface)
                return Type.Interface;
            else if (t.IsValueType)
                return Type.Struct;
            else
                return Type.Delegate;
        }
    }
}
