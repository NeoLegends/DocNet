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

        #region Implementation

        #region Type Documentation

        public string GetTypeDocumentation(Type typeDetails, string documentationNode, CultureInfo culture = null)
        {
            return null;
        }

        #endregion

        #region Method Documentation

        public string GetMethodDocumentation(MethodInfo methodDetails, string documentationNode, CultureInfo culture = null)
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
            foreach (object attribute in methodDetails.GetCustomAttributes(true))
            {
                result.Append("[");
                result.Append(attribute.GetType().Name);
                result.Append("]");
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

            // Method name and parameters
            result.Append(methodDetails.ReturnType.Name == "Void" ? "void" : methodDetails.ReturnType.Name + " ");
            result.Append(methodDetails.Name);
            result.Append("(");
            foreach (ParameterInfo pInfo in methodDetails.GetParameters())
                result.Append( // FIX: Add support for ref / out parameters
                    pInfo.ParameterType.Name + " " + pInfo.Name + 
                    (pInfo.IsOptional ? " = " + pInfo.RawDefaultValue.ToString() : null)
                );
            result.Append(");");

            // Finished
            return result.ToString();
        }

        #endregion

        #region Field Documentation

        public string GetFieldDocumentation(FieldInfo fieldDetails, string documentationNode, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateFieldSignature(fieldDetails));
        }

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
            if (fieldDetails.IsStatic)
                result.Append("static ");
            if (fieldDetails.IsInitOnly)
                result.Append("readonly ");

            // Field name
            result.Append(fieldDetails.FieldType.Name + " ");
            result.Append(fieldDetails.Name + ";");

            return result.ToString();
        }

        #endregion

        #region Property Documentation

        public string GetPropertyDocumentation(PropertyInfo propertyDetails, string documentationNode, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GeneratePropertySignature(propertyDetails));
        }

        private String GeneratePropertySignature(PropertyInfo propertyDetails)
        {
            StringBuilder result = new StringBuilder(100);

            result.Append(propertyDetails.PropertyType.Name + " ");
            result.Append(propertyDetails.Name + " ");

            // Get / Set Accessors
            MethodInfo getAccessor = propertyDetails.GetGetMethod(true),
                       setAccessor = propertyDetails.GetSetMethod(true);
            result.Append("{ ");
            if (getAccessor != null)
            {
                if (getAccessor.IsPublic)
                    result.Append("public get; ");
                if (getAccessor.IsPrivate)
                    result.Append("private get; ");
                else if (getAccessor.IsFamilyOrAssembly)
                    result.Append("internal get; ");
                else if (getAccessor.IsFamily)
                    result.Append("protected get; ");
            }
            if (setAccessor != null)
            {
                if (setAccessor.IsPublic)
                    result.Append("public set; ");
                if (setAccessor.IsPrivate)
                    result.Append("private set; ");
                else if (setAccessor.IsFamilyOrAssembly)
                    result.Append("internal set; ");
                else if (setAccessor.IsFamily)
                    result.Append("protected set; ");
            }
            result.Append("}");

            return result.ToString();
        }

        #endregion

        #region Event Documentation

        public string GetEventDocumentation(EventInfo eventDetails, string documentationNode, CultureInfo culture = null)
        {
            return this.WriteSyntaxBox(this.GenerateEventSignature(eventDetails));
        }

        private String GenerateEventSignature(EventInfo propertyDetails)
        {

        }

        #endregion

        #region Error Documentation

        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
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
