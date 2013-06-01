using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Contains methods helping generating special commonly used text for .NET documentation
    /// </summary>
    public static class ElementGenerator
    {
        /// <summary>
        /// Contains all C#-Specific language data type aliases
        /// </summary>
        private static readonly Dictionary<Type, String> aliases = new Dictionary<Type, String>()
            {
                { typeof (byte), "byte" },
                { typeof (sbyte), "sbyte" },
                { typeof (short), "short" },
                { typeof (ushort), "ushort" },
                { typeof (int), "int" },
                { typeof (uint), "uint" },
                { typeof (long), "long" },
                { typeof (ulong), "ulong" },
                { typeof (float), "float" },
                { typeof (double), "double" },
                { typeof (decimal), "decimal" },
                { typeof (object), "object" },
                { typeof (bool), "bool" },
                { typeof (char), "char" },
                { typeof (string), "string" },
                { typeof (void), "void" }
            };

        /// <summary>
        /// Generates the access modificator signature of the given <see cref="System.Type"/> and adds "class", "interface", "enum", etc
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the access modificator signature from</param>
        /// <returns>The access modificator signature of the given <see cref="System.Type"/></returns>
        public static String GetAccessModificatorsAndMemberType(Type typeDetails)
        {
            StringBuilder result = new StringBuilder(35);

            if (typeDetails.IsClass)
                result.Append("public " + (typeDetails.IsAbstract ? "abstract " : (typeDetails.IsSealed ? "sealed " : null)) + "class ");
            else if (typeDetails.IsInterface)
                result.Append("public interface ");
            else if (typeDetails.IsEnum)
                result.Append("public enum ");
            else if (typeDetails.IsValueType)
                result.Append("public struct ");

            return result.ToString();
        }

        /// <summary>
        /// Generates the generic signature of the given <see cref="System.Type"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the generic signature from</param>
        /// <returns>The generic signature of the given <see cref="System.Type"/></returns>
        public static String GetGenericSignature(Type typeDetails)
        {
            if (typeDetails.IsGenericType)
                return GetGenericSignatureInternal(typeDetails.GetGenericArguments());
            else
                return null;
        }

        /// <summary>
        /// Generates the generic signature of the given <see cref="System.Type"/>
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Type"/> to generate the generic signature from</param>
        /// <returns>The generic signature of the given <see cref="System.Type"/></returns>
        public static String GetGenericSignature(MethodInfo methodDetails)
        {
            Type[] genericArguments = methodDetails.GetGenericArguments();
            if (genericArguments.Length > 0)
                return GetGenericSignatureInternal(genericArguments);
            else
                return null;
        }

        /// <summary>
        /// Internal version of GetGenericSignature
        /// </summary>
        private static String GetGenericSignatureInternal(Type[] genericParameters)
        {
            String result = String.Empty;

            result += "<";
            result += String.Join(", ", genericParameters.Select(t => t.Name));
            result += ">";

            return result;
        }

        /// <summary>
        /// Generates the inheritance signature of the given <see cref="System.Type"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the inhericante signature from</param>
        /// <returns>The inheritance signature of the given <see cref="System.Type"/></returns>
        public static String GetTypeInheritanceSignature(Type typeDetails)
        {
            Type[] interfaces = typeDetails.GetInterfaces();

            if (typeDetails.BaseType != null || interfaces.Length > 0)
            {
                String result = String.Empty;
                result += ": ";
                result += typeDetails.BaseType != null ? typeDetails.BaseType.Name + ", " : null;
                result += String.Join(", ", interfaces.Select(t => t.Name));
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// Checks whether the type is one of the primitive types and returns the C#-Alias
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> to get the C#-name of</param>
        /// <returns>The name of the <see cref="System.Type"/> as it is used in C#</returns>
        public static String GetFriendlyName(Type type)
        {
            return aliases.ContainsKey(type) ? aliases[type] : type.Name;
        }

        /// <summary>
        /// Generates the whole attribute signature from the given <see cref="System.Reflection.MemberInfo"/>
        /// </summary>
        /// <param name="memberInfo">The <see cref="System.Reflection.MemberInfo"/> to extract the attribute signature from</param>
        /// <returns>The finished attribute signature</returns>
        public static String GetAttributeSignature(MemberInfo memberInfo)
        {
            StringBuilder result = new StringBuilder(150);

            foreach (CustomAttributeData attribute in memberInfo.GetCustomAttributesData())
            {
                // Attribute name
                result.Append("[");
                result.Append(attribute.Constructor.DeclaringType.Name);
                result.Append("(");

                // Parameters
                ParameterInfo[] attributeConstructorParameters = attribute.Constructor.GetParameters();
                String attributeParameterSignature = String.Join(", ", attributeConstructorParameters.Select(pInfo => pInfo.ParameterType.Name + " " + pInfo.Name));
                result.Append(attributeParameterSignature);

                result.Append(")]");
            }

            return result.ToString();
        }
    }
}
