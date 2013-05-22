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
        /// <param name="fullMethodName">The full name of the method to check the return type of</param>
        /// <param name="assemblyFile">The path to the assembly file to extract the return type from</param>
        /// <returns>The return type of the given method</returns>
        public static Type GetMethodReturnType(String fullMethodName, String assemblyFile)
        {
            foreach (Type type in Assembly.LoadFrom(assemblyFile).GetTypes())
            {
                MethodInfo method = type.GetMethod(FullNameToShortName(fullMethodName));
                if ((method != null) && ((type.FullName + method.Name) == fullMethodName))
                    return method.ReturnType;
            }
            return null;
        }

        /// <summary>
        /// Checks whether a given member is a class, interface, enum, and so on
        /// </summary>
        /// <param name="memberFullName">The full name of the member to check the type of</param>
        /// <param name="assemblyFile">The path to the assembly file to extract the return type from</param>
        /// <returns>Whether the given type is a class, interface, enum / and so on</returns>
        public static FrameworkTypes GetMemberType(String memberFullName, String assemblyFile)
        {
            Type t = Assembly.LoadFrom(assemblyFile).GetType(memberFullName);
            if (t != null)
            {
                if (t.IsClass)
                    return FrameworkTypes.Class;
                else if (t.IsEnum)
                    return FrameworkTypes.Enum;
                else if (t.IsInterface)
                    return FrameworkTypes.Interface;
                else if (t.IsValueType)
                    return FrameworkTypes.Struct;
                else
                    return FrameworkTypes.Delegate;
            }
            else
                throw new NullReferenceException("There was an error extracting the type from the assembly.");
        }

        /// <summary>
        /// Gets the short member name of a given full member name
        /// </summary>
        /// <param name="fullMemberName">The full member name</param>
        /// <returns>The short member name</returns>
        public static String FullNameToShortName(String fullMemberName)
        {
            return fullMemberName.Split('.', '#').Last();
        }
    }
}
