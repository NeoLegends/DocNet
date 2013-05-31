using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DocNetPress.Development.Generator.Extensions.SyntaxElement
{
    /// <summary>
    /// Generates the "Syntax" part of a documentation page in C#
    /// </summary>
    [Serializable]
    public class SyntaxElement : IPageElement
    {
        /// <summary>
        /// Backing field for OutputField
        /// </summary>
        private OutputField _OutputField = OutputField.CrayonSyntaxHighlighter;

        /// <summary>
        /// The way the results are being outputted
        /// </summary>
        public OutputField OutputField 
        {
            get
            {
                return _OutputField;
            }
            set
            {
                _OutputField = value;
            }
        }

        /// <summary>
        /// Backing field for HeadlineLevel
        /// </summary>
        private HeadlineLevel _HeadlineLevel = HeadlineLevel.h2;

        /// <summary>
        /// The size of the headline
        /// </summary>
        public HeadlineLevel HeadlineLevel
        {
            get
            {
                return _HeadlineLevel;
            }
            set
            {
                _HeadlineLevel = value;
            }
        }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Development.Generator.Extensions.SyntaxElement.SyntaxElement"/>
        /// </summary>
        public SyntaxElement() { }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Development.Generator.Extensions.SyntaxElement.SyntaxElement"/>
        /// </summary>
        public SyntaxElement(HeadlineLevel headlineLevel)
        {
            this.HeadlineLevel = headlineLevel;
        }

        #region Implementation

        #region Type Documentation

        /// <summary>
        /// Generates documentation for a given <see cref="System.Type"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the documentation from</param>
        /// <param name="documentationNode">The inner text of the .NET documentation node containing all custom documentation text</param>
        /// <param name="culture">The culture to generate the output in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        public string GetTypeDocumentation(Type typeDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateTypeSignature(typeDetails, language), language, culture);
        }

        /// <summary>
        /// Internal version of <see cref="M:DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement.GetTypeDocumentation"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the documentation from</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        private String GenerateTypeSignature(Type typeDetails, OutputLanguage language)
        {
            StringBuilder result = new StringBuilder(150);

            result.AppendLine(ElementGenerator.GetAttributeSignature(typeDetails, language));
            result.Append(ElementGenerator.GetTypeAccessModificatorsAndTypes(typeDetails, language));
            result.Append(ElementGenerator.GetSpecialTypeAliases(typeDetails, language) + " ");
            result.Append(ElementGenerator.GetGenericSignature(typeDetails, language));
            result.Append(ElementGenerator.GetTypeInheritanceSignature(typeDetails, language));

            return result.ToString();
        }

        #endregion

        #region Method Documentation

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.MethodInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and a given culture to generate the output in
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Type"/> for further information about the method to be documented</param>
        /// <param name="documentationNode">The documentation code containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post
        /// </returns>
        public string GetMethodDocumentation(MethodInfo methodDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateMethodSignature(methodDetails, language), language, culture);
        }

        /// <summary>
        /// Generates the full method signature from the given <see cref="System.Reflection.MethodInfo"/>
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Reflection.MethodInfo"/> giving further information about the method</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>A nicely looking method signature</returns>
        private String GenerateMethodSignature(MethodInfo methodDetails, OutputLanguage language)
        {
            StringBuilder result = new StringBuilder(150);

            result.AppendLine(this.GetAttributeSignature(methodDetails, language));
            result.Append(this.GetMethodAccessModificatorSignature(methodDetails));
            result.Append(this.GetMethodDataTypeAndName(methodDetails, language));
            result.Append(this.GenerateMethodGenericParameterSignature(methodDetails));
            result.Append(this.GetMethodParameterSignature(methodDetails));

            return result.ToString();
        }

        /// <summary>
        /// Generates the method access modificator signature
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Reflection.MethodInfo"/> to generate the access modificator signature from</param>
        /// <returns>The method access modificator signature</returns>
        private String GetMethodAccessModificatorSignature(MethodInfo methodDetails)
        {
            StringBuilder result = new StringBuilder(50);

            // Access modificators
            if (methodDetails.IsPublic)
                result.Append("public ");
            else if (methodDetails.IsPrivate)
                result.Append("private ");
            else if (methodDetails.IsFamilyOrAssembly)
                result.Append("internal ");
            else if (methodDetails.IsFamily)
                result.Append("protected ");

            // Further method attributes
            if (methodDetails.IsStatic)
                result.Append("static ");
            if (methodDetails.IsVirtual)
                result.Append("virtual ");

            return result.ToString();
        }

        /// <summary>
        /// Concatenates method type and name
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Reflection.MethodInfo"/> to concatenate type and name from</param>
        /// <returns>The methods data type, name and a whitespace</returns>
        private String GetMethodDataTypeAndName(MethodInfo methodDetails, OutputLanguage language)
        {
            return this.GetSpecialTypeAliases(methodDetails.ReturnType, language) + " " + methodDetails.Name;
        }

        /// <summary>
        /// Generates the parameter signature of the given method (with braces)
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Reflection.MethodInfo"/> to generate the parameter signature from</param>
        /// <returns>The parameter signature of the given method</returns>
        private String GetMethodParameterSignature(MethodInfo methodDetails)
        {
            StringBuilder result = new StringBuilder(75);

            result.Append("(");
            ParameterInfo[] methodParamters = methodDetails.GetParameters();
            result.Append(String.Join(", ", methodParamters.Select(pi => pi.ParameterType.Name + " " + pi.Name)));
            result.Append(");");

            return result.ToString();
        }

        /// <summary>
        /// Generates the generic parameter signature of the given <see cref="System.Reflection.MethodInfo"/>
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Reflection.MethodInfo"/> to generate the generic parameter signature of</param>
        /// <returns>The generic parameter signature of the given method</returns>
        private String GenerateMethodGenericParameterSignature(MethodInfo methodDetails)
        {
            if (methodDetails.IsGenericMethod)
            {
                StringBuilder result = new StringBuilder(25);

                result.Append("<");
                Type[] genericTypes = methodDetails.GetGenericArguments();
                result.Append(String.Join(", ", genericTypes.Select(t => t.Name)));
                result.Append(">");

                return result.ToString();
            }
            else
                return null;
        }

        #endregion

        #region Field Documentation

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.FieldInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="fieldDetails">The <see cref="System.Reflection.FieldInfo"/> providing further information about the field to document</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post
        /// </returns>
        public string GetFieldDocumentation(FieldInfo fieldDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateFieldSignature(fieldDetails, language), language, culture);
        }

        /// <summary>
        /// Internal version of <see cref="M:DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement.GetFieldDocumentation"/>
        /// </summary>
        /// <param name="fieldDetails">The <see cref="System.Reflection.FieldInfo"/> to generate the documentation from</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        private String GenerateFieldSignature(FieldInfo fieldDetails, OutputLanguage language)
        {
            StringBuilder result = new StringBuilder(100);

            result.AppendLine(this.GetAttributeSignature(fieldDetails, language));
            result.Append(this.GetFieldAccessModificatorSignature(fieldDetails));
            result.Append(this.GetFieldTypeAndName(fieldDetails));

            return result.ToString();
        }

        /// <summary>
        /// Generates the access modificator signature of the given <see cref="System.Reflection.FieldInfo"/>
        /// </summary>
        /// <param name="fieldDetails">The <see cref="System.Reflection.FieldInfo"/> to generate the access modificator signature from</param>
        /// <returns>The access modificator signature of the given field</returns>
        private String GetFieldAccessModificatorSignature(FieldInfo fieldDetails)
        {
            StringBuilder result = new StringBuilder(50);

            if (fieldDetails.IsPublic)
                result.Append("public ");
            else if (fieldDetails.IsPrivate)
                result.Append("private ");
            else if (fieldDetails.IsFamilyOrAssembly)
                result.Append("internal ");
            else if (fieldDetails.IsFamily)
                result.Append("protected ");

            if (fieldDetails.IsStatic && !fieldDetails.IsLiteral)
                result.Append("static ");
            if (fieldDetails.IsLiteral)
                result.Append("const ");
            else if (fieldDetails.IsInitOnly)
                result.Append("readonly ");

            return result.ToString();
        }

        /// <summary>
        /// Gets type and name of the given <see cref="System.Reflection.FieldInfo"/>
        /// </summary>
        /// <param name="fieldDetails">The <see cref="System.Reflection.FieldInfo"/> to get type and name of</param>
        /// <returns>Field type and name with a semicolon at the end</returns>
        private String GetFieldTypeAndName(FieldInfo fieldDetails)
        {
            return fieldDetails.FieldType.Name + " " + fieldDetails.Name + ";";
        }

        #endregion

        #region Property Documentation

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.PropertyInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="propertyDetails">Provides further information about the property to be documentated</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post
        /// </returns>
        public string GetPropertyDocumentation(PropertyInfo propertyDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GeneratePropertySignature(propertyDetails, language), language, culture);
        }

        /// <summary>
        /// Internal version of <see cref="M:DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement.GetPropertyDocumentation"/>
        /// </summary>
        /// <param name="propertyDetails">The <see cref="System.Reflection.PropertyInfo"/> to generate the documentation from</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        private String GeneratePropertySignature(PropertyInfo propertyDetails, OutputLanguage language)
        {
            StringBuilder result = new StringBuilder(100);

            result.AppendLine(this.GetAttributeSignature(propertyDetails, language));
            result.Append(this.GetPropertyTypeAndName(propertyDetails));
            result.Append(this.GetPropertyGetterSetterSignature(propertyDetails));

            return result.ToString();
        }

        /// <summary>
        /// Gets the type and name of the given property
        /// </summary>
        /// <param name="propertyDetails">The <see cref="System.Reflection.PropertyInfo"/> to extract the values from</param>
        /// <returns>Property type and name concatenated with a whitespace at the end</returns>
        private String GetPropertyTypeAndName(PropertyInfo propertyDetails)
        {
            return propertyDetails.PropertyType.Name + " " + propertyDetails.Name + " ";
        }

        /// <summary>
        /// Generates the getter / setter signature from the given <see cref="System.Reflection.PropertyInfo"/>
        /// </summary>
        /// <param name="propertyDetails">The <see cref="System.Reflection.PropertyInfo"/> to extract the getter / setter signature from</param>
        /// <returns>The getter / setter signature</returns>
        private String GetPropertyGetterSetterSignature(PropertyInfo propertyDetails)
        {
            StringBuilder result = new StringBuilder(25);

            result.Append("{ ");
            result.Append(this.GetPropertyGetSetMethodSignature(propertyDetails.GetGetMethod(true), true));
            result.Append(this.GetPropertyGetSetMethodSignature(propertyDetails.GetSetMethod(true), false));
            result.Append("}");

            return result.ToString();
        }

        /// <summary>
        /// Generates the signature for either the getter or the setter method of a property
        /// </summary>
        /// <param name="accessor">The getter / setter method</param>
        /// <param name="getOrSet">Whether the method shall generate a get or set accessor signature</param>
        /// <returns>The accessor signature</returns>
        private String GetPropertyGetSetMethodSignature(MethodInfo accessor, bool getOrSet)
        {
            StringBuilder result = new StringBuilder(25);

            if (accessor.IsPrivate)
                result.Append("private ");
            else if (accessor.IsFamilyOrAssembly)
                result.Append("internal ");
            else if (accessor.IsFamily)
                result.Append("protected ");

            if (accessor.IsStatic)
                result.Append("static ");

            if (getOrSet)
                result.Append("get; ");
            else
                result.Append("set; ");

            return result.ToString();
        }

        #endregion

        #region Event Documentation

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.EventInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="eventDetails"><see cref="System.Reflection.EventInfo"/> containing further data about the event to document</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post
        /// </returns>
        public string GetEventDocumentation(EventInfo eventDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateEventSignature(eventDetails, language), language, culture);
        }

        /// <summary>
        /// Internal version of <see cref="M:DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement.GetEventDocumentation"/>
        /// </summary>
        /// <param name="eventDetails">The <see cref="System.Reflection.EventInfo"/> to generate the documentation from</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        private String GenerateEventSignature(EventInfo eventDetails, OutputLanguage language)
        {
            StringBuilder result = new StringBuilder(100);

            // Attributes
            result.AppendLine(this.GetAttributeSignature(eventDetails, language));

            // Event signature
            result.Append("public event ");
            result.Append(eventDetails.EventHandlerType.Name + " ");
            result.Append(eventDetails.Name + ";");

            return result.ToString();
        }

        #endregion

        #region Namespace Documentation

        /// <summary>
        /// The <see cref="DocNetPress.Development.Generator.Extensions.SyntaxElement.SyntaxElement"/> does not support namespace syntax as there is no
        /// special syntax for namespaces
        /// </summary>
        public string GetNamespaceDocumentation(string nameSpace, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return null;
        }

        #endregion

        #region Error Documentation

        /// <summary>
        /// The <see cref="DocNetPress.Development.Generator.Extensions.SyntaxElement.SyntaxElement"/> does not support resolving unresolved paths
        /// </summary>
        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return null;
        }

        #endregion

        /// <summary>
        /// Generates the whole attribute signature from the given <see cref="System.Reflection.MemberInfo"/>
        /// </summary>
        /// <param name="memberInfo">The <see cref="System.Reflection.MemberInfo"/> to extract the attribute signature from</param>
        /// <returns>The finished attribute signature</returns>
        private String GetAttributeSignature(MemberInfo memberInfo, OutputLanguage language)
        {
            StringBuilder result = new StringBuilder(150);

            // Attributes
            foreach (CustomAttributeData attribute in memberInfo.GetCustomAttributesData())
            {
                if (language == OutputLanguage.CSharp)
                    result.Append("[");
                else if (language == OutputLanguage.VBNET)
                    result.Append("<");

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

        /// <summary>
        /// Generates the standard syntax box from the given content
        /// </summary>
        /// <param name="content">The content to write into the syntax box</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>The finished HTML-Code</returns>
        private String WriteSyntaxBox(String content, OutputLanguage language, CultureInfo culture = null)
        {
            using (StringWriter sw = new StringWriter())
            using (var xWriter = XmlWriter.Create(sw))
            {
                // Headline
                xWriter.WriteElementString(HeadlineLevel.ToString(), Strings.ResourceManager.GetString("SyntaxHeadline", culture));

                // Output
                if (OutputField == OutputField.CrayonSyntaxHighlighter)
                {
                    xWriter.WriteStartElement("pre");
                    xWriter.WriteAttributeString("class", "lang:" + this.GetOutputLanguageString(language) + " decode=true");
                    xWriter.WriteString(content);
                    xWriter.WriteEndElement();
                }
                else if (OutputField == OutputField.QuoteBox)
                {
                    xWriter.WriteElementString("blockquote", content);
                }

                // Finished writing, return generated Code 
                xWriter.WriteEndDocument();
                return sw.ToString();
            }
        }

        /// <summary>
        /// Generates the Crayon language string corresponding to the given <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/>
        /// </summary>
        /// <param name="language">The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> giving the desired output language</param>
        /// <returns>The Crayon conform language string</returns>
        private String GetOutputLanguageString(OutputLanguage language)
        {
            if (language == OutputLanguage.CSharp)
                return "c#";
            else if (language == OutputLanguage.FSharp)
                return "default";
            else if (language == OutputLanguage.JScript)
                return "js";
            else if (language == OutputLanguage.VBNET)
                return "vb";
            else
                return null;
        }

        /// <summary>
        /// Checks whether the type is one of the primitive types and returns the C#-Alias
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> to get the C#-name of</param>
        /// <returns>The name of the <see cref="System.Type"/> as it is used in C#</returns>
        private String GetSpecialTypeAliases(Type type, OutputLanguage language)
        {
            if (type == typeof(bool))
                return "bool";
            else if (type == typeof(int))
                return "int";
            else if (type == typeof(uint))
                return "uint";
            else if (type == typeof(long))
                return "long";
            else if (type == typeof(ulong))
                return "ulong";
            else if (type == typeof(byte))
                return "byte";
            else if (type == typeof(sbyte))
                return "sbyte";
            else if (type == typeof(short))
                return "short";
            else if (type == typeof(ushort))
                return "ushort";
            else if (type == typeof(double))
                return "double";
            else if (type == typeof(float))
                return "float";
            else if (type == typeof(decimal))
                return "decimal";
            else if (type == typeof(char))
                return "char";
            else if (type == typeof(void))
                return "void";
            else
                return type.Name;
        }

        #endregion
    }
}
