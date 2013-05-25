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
        /// Backing field for xmlDocument
        /// </summary>
        private XmlDocument _xmlDocument = new XmlDocument();

        /// <summary>
        /// An XmlDocument-Instance for easier dealing with XML
        /// </summary>
        private XmlDocument xmlDocument
        {
            get
            {
                if (_xmlDocument == null)
                    _xmlDocument = new XmlDocument();
                return _xmlDocument;
            }
            set
            {
                _xmlDocument = value;
            }
        }

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
        /// Derived from <see cref="DocNetPress.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string Name
        {
            get 
            {
                return "SyntaxElement"; 
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsCSharp
        {
            get
            { 
                return true;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsVBNET
        {
            get 
            {
                return false;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsFSharp
        {
            get 
            {
                return false;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsJScript
        {
            get
            {
                return false;
            }
        }

        #region Implementation

        #region Type Documentation

        /// <summary>
        /// Generates documentation for a given <see cref="System.Type"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the documentation from</param>
        /// <param name="documentationNode">The inner text of the .NET documentation node containing all custom documentation text</param>
        /// <param name="culture">The culture to generate the output in</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        public string GetTypeDocumentation(Type typeDetails, string documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateTypeSignature(typeDetails));
        }

        /// <summary>
        /// Internal version of <see cref="M:DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement.GetTypeDocumentation"/>
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> to generate the documentation from</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        private String GenerateTypeSignature(Type typeDetails)
        {
            StringBuilder result = new StringBuilder(150);

            // Method attributes
            foreach (CustomAttributeData attribute in typeDetails.GetCustomAttributesData())
            {
                result.Append("[");
                result.Append(attribute.Constructor.DeclaringType.Name);
                result.Append("(");

                // Parameters
                ParameterInfo[] attributeConstructorParameters = attribute.Constructor.GetParameters();
                String attributeParameterSignature = String.Join(", ", attributeConstructorParameters.Select(pi => pi.ParameterType.Name + " " + pi.Name));
                result.Append(attributeParameterSignature);

                result.Append(")]");
                result.Append(Environment.NewLine);
            }

            // Access modificators
            if (typeDetails.IsClass)
                result.Append("public " + (typeDetails.IsAbstract ? "abstract " : (typeDetails.IsSealed ? "sealed " : null)) + "class ");
            else if (typeDetails.IsInterface)
                result.Append("public interface ");
            else if (typeDetails.IsEnum)
                result.Append("public enum ");
            else if (typeDetails.IsValueType)
                result.Append("public struct ");

            // Class name
            result.Append(typeDetails.Name + " ");

            // Generic parameters
            if (typeDetails.IsGenericType)
            {
                result.Append("<");
                String genericParameterSignature = String.Join(", ", typeDetails.GetGenericArguments().Select(t => t.Name));
                result.Append(genericParameterSignature);
                result.Append(">");
            }

            // Deriving stuff
            if (typeDetails.BaseType != null || typeDetails.GetInterfaces().Length > 0)
                result.Append(": ");
            result.Append(typeDetails.BaseType != null ? typeDetails.BaseType.Name + ", " : null);
            String deriveSignature = String.Join(", ", typeDetails.GetInterfaces().Select(t => t.Name));
            result.Append(deriveSignature);

            return result.ToString();
        }

        #endregion

        #region Method Documentation

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.MethodInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and a given culture to generate the output in
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> for further information about the method to be documented</param>
        /// <param name="documentationNode">The documentation code containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post
        /// </returns>
        public string GetMethodDocumentation(MethodInfo methodDetails, string documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateMethodSignature(methodDetails));
        }

        /// <summary>
        /// Generates the full method signature from the given <see cref="System.Reflection.MethodInfo"/>
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Reflection.MethodInfo"/> giving further information about the method</param>
        /// <returns>A nicely looking method signature</returns>
        private String GenerateMethodSignature(MethodInfo methodDetails)
        {
            StringBuilder result = new StringBuilder(150);

            // Method attributes
            foreach (CustomAttributeData attribute in methodDetails.GetCustomAttributesData())
            {
                result.Append("[");
                result.Append(attribute.Constructor.DeclaringType.Name);
                result.Append("(");

                // Parameters
                ParameterInfo[] attributeConstructorParameters = attribute.Constructor.GetParameters();
                String attributeParameterSignature = String.Join(", ", attributeConstructorParameters.Select(pi => pi.ParameterType.Name + " " + pi.Name));
                result.Append(attributeParameterSignature);

                result.Append(")]");
                result.Append(Environment.NewLine);
            }

            // Method Body
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

            // Method name
            result.Append(methodDetails.ReturnType.Name == "Void" ? "void " : methodDetails.ReturnType.Name + " ");
            result.Append(methodDetails.Name);

            result.Append("(");

            // Parameters
            ParameterInfo[] methodParamters = methodDetails.GetParameters();
            String parameterSignature = String.Join(", ", methodParamters.Select(pi => pi.ParameterType.Name + " " + pi.Name));

            result.Append(parameterSignature);
            result.Append(");");

            // Finished
            return result.ToString();
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
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post
        /// </returns>
        public string GetFieldDocumentation(FieldInfo fieldDetails, string documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateFieldSignature(fieldDetails));
        }

        /// <summary>
        /// Internal version of <see cref="M:DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement.GetFieldDocumentation"/>
        /// </summary>
        /// <param name="fieldDetails">The <see cref="System.Reflection.FieldInfo"/> to generate the documentation from</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        private String GenerateFieldSignature(FieldInfo fieldDetails)
        {
            StringBuilder result = new StringBuilder(100);

            // Access modificator
            if (fieldDetails.IsPublic)
                result.Append("public ");
            else if (fieldDetails.IsPrivate)
                result.Append("private ");
            else if (fieldDetails.IsFamilyOrAssembly)
                result.Append("internal ");
            else if (fieldDetails.IsFamily)
                result.Append("protected ");

            // Further field attributes
            if (fieldDetails.IsStatic && !fieldDetails.IsLiteral)
                result.Append("static ");
            if (fieldDetails.IsLiteral)
                result.Append("const ");
            else if (fieldDetails.IsInitOnly)
                result.Append("readonly ");

            // Field name
            result.Append(fieldDetails.FieldType.Name + " ");
            result.Append(fieldDetails.Name + ";");

            return result.ToString();
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
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post
        /// </returns>
        public string GetPropertyDocumentation(PropertyInfo propertyDetails, string documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GeneratePropertySignature(propertyDetails));
        }

        /// <summary>
        /// Internal version of <see cref="M:DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement.GetPropertyDocumentation"/>
        /// </summary>
        /// <param name="propertyDetails">The <see cref="System.Reflection.PropertyInfo"/> to generate the documentation from</param>
        /// <returns>The finished HTML code ready to insert into a WordPress post</returns>
        private String GeneratePropertySignature(PropertyInfo propertyDetails)
        {
            StringBuilder result = new StringBuilder(100);

            // Attributes
            foreach (CustomAttributeData attribute in propertyDetails.GetCustomAttributesData())
            {
                result.Append("[");
                result.Append(attribute.Constructor.DeclaringType.Name);
                result.Append("(");

                // Parameters
                ParameterInfo[] attributeConstructorParameters = attribute.Constructor.GetParameters();
                String attributeParameterSignature = String.Join(", ", attributeConstructorParameters.Select(pi => pi.ParameterType.Name + " " + pi.Name));
                result.Append(attributeParameterSignature);

                result.Append(")]");
                result.Append(Environment.NewLine);
            }

            result.Append(propertyDetails.PropertyType.Name + " ");
            result.Append(propertyDetails.Name + " ");

            // Get / Set Accessors
            MethodInfo getAccessor = propertyDetails.GetGetMethod(true),
                       setAccessor = propertyDetails.GetSetMethod(true);
            result.Append("{ ");
            if (getAccessor != null)
            {
                if (getAccessor.IsPublic)
                    result.Append("public ");
                else if (getAccessor.IsPrivate)
                    result.Append("private ");
                else if (getAccessor.IsFamilyOrAssembly)
                    result.Append("internal ");
                else if (getAccessor.IsFamily)
                    result.Append("protected ");

                if (getAccessor.IsStatic)
                    result.Append("static ");

                result.Append("get; ");
            }
            if (setAccessor != null)
            {
                if (setAccessor.IsPublic)
                    result.Append("public ");
                if (setAccessor.IsPrivate)
                    result.Append("private ");
                else if (setAccessor.IsFamilyOrAssembly)
                    result.Append("internal ");
                else if (setAccessor.IsFamily)
                    result.Append("protected ");

                if (setAccessor.IsStatic)
                    result.Append("static ");

                result.Append("set; ");
            }
            result.Append("}");

            return result.ToString();
        }

        #endregion

        #region Event Documentation

        public string GetEventDocumentation(EventInfo eventDetails, string documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateEventSignature(eventDetails));
        }

        private String GenerateEventSignature(EventInfo eventDetails)
        {
            StringBuilder result = new StringBuilder(100);

            throw new NotImplementedException();
        }

        #endregion

        #region Error Documentation

        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, string documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return null;
        }

        #endregion

        /// <summary>
        /// Generates the standard syntax box from the given content
        /// </summary>
        /// <param name="content">The content to write into the syntax box</param>
        /// <returns>The finished HTML-Code</returns>
        private String WriteSyntaxBox(String content)
        {
            // Variables
            using (StringWriter sw = new StringWriter())
            using (var xWriter = XmlWriter.Create(sw))
            {
                // Headline
                xWriter.WriteElementString(HeadlineLevel.ToString(), Strings.SyntaxHeadline);

                // Output
                if (OutputField == OutputField.CrayonSyntaxHighlighter)
                {
                    xWriter.WriteStartElement("pre");
                    xWriter.WriteAttributeString("class", "lang:c# decode=true");
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

        #endregion
    }
}
