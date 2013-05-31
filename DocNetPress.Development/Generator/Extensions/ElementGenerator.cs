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
        /// Generates the access modificator signature of the given <see cref="System.Type"/> and adds "class", "interface", "enum", etc
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the access modificator signature from</param>
        /// <returns>The access modificator signature of the given <see cref="System.Type"/></returns>
        public static String GetTypeAccessModificatorsAndTypes(Type typeDetails, OutputLanguage language = OutputLanguage.CSharp)
        {
            StringBuilder result = new StringBuilder(35);

            if (language == OutputLanguage.CSharp)
            {
                if (typeDetails.IsClass)
                    result.Append("public " + (typeDetails.IsAbstract ? "abstract " : (typeDetails.IsSealed ? "sealed " : null)) + "class ");
                else if (typeDetails.IsInterface)
                    result.Append("public interface ");
                else if (typeDetails.IsEnum)
                    result.Append("public enum ");
                else if (typeDetails.IsValueType)
                    result.Append("public struct ");
            }
            else if (language == OutputLanguage.VBNET)
            {

            }

            return result.ToString();
        }

        /// <summary>
        /// Generates the generic signature of the given <see cref="System.Type"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the generic signature from</param>
        /// <returns>The generic signature of the given <see cref="System.Type"/></returns>
        public static String GetGenericSignature(Type typeDetails, OutputLanguage language = OutputLanguage.CSharp)
        {
            if (typeDetails.IsGenericType)
                return GenerateGenericSignature(typeDetails.GetGenericArguments(), language);
            else
                return null;
        }

        /// <summary>
        /// Generates the generic signature of the given <see cref="System.Type"/>
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Type"/> to generate the generic signature from</param>
        /// <returns>The generic signature of the given <see cref="System.Type"/></returns>
        public static String GetGenericSignature(MethodInfo methodDetails, OutputLanguage language = OutputLanguage.CSharp)
        {
            Type[] genericArguments = methodDetails.GetGenericArguments();
            if (genericArguments.Length > 0)
                return GenerateGenericSignature(genericArguments, language);
            else
                return null;
        }

        private static String GenerateGenericSignature(Type[] genericParameters, OutputLanguage language = OutputLanguage.CSharp)
        {
            StringBuilder result = new StringBuilder(50);

            if (language == OutputLanguage.CSharp)
            {
                result.Append("<");
                result.Append(String.Join(", ", genericParameters.Select(t => t.Name)));
                result.Append(">");
            }
            else if (language == OutputLanguage.VBNET)
            {
                result.Append("(Of");
                result.Append(String.Join(", ", genericParameters.Select(t => t.Name)));
                result.Append(")");
            }

            return result.ToString();
        }

        /// <summary>
        /// Generates the inheritance signature of the given <see cref="System.Type"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the inhericante signature from</param>
        /// <returns>The inheritance signature of the given <see cref="System.Type"/></returns>
        public static String GetTypeInheritanceSignature(Type typeDetails, OutputLanguage language = OutputLanguage.CSharp)
        {
            StringBuilder result = new StringBuilder(25);
            Type[] interfaces = typeDetails.GetInterfaces();

            if (typeDetails.BaseType != null || interfaces.Length > 0)
            {
                if (language == OutputLanguage.CSharp)
                {
                    result.Append(": ");
                    result.Append(typeDetails.BaseType != null ? typeDetails.BaseType.Name + ", " : null);
                    result.Append(String.Join(", ", interfaces.Select(t => t.Name)));
                }
                else if (language == OutputLanguage.VBNET)
                {

                }
            }

            return result.ToString();
        }
    }
}
