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
        private OutputField _OutputField = OutputField.Crayon;

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

        #region Type Documentation

        public string GetTypeDocumentation(Type typeDetails, string documentationNode, CultureInfo culture = null)
        {
            return null;
        }

        #endregion

        #region Method Documentation

        public string GetMethodDocumentation(MethodInfo methodDetails, string documentationNode, CultureInfo culture = null)
        {
            // Variables
            using (StringWriter sw = new StringWriter())
            using (var xWriter = XmlWriter.Create(sw))
            {
                // Headline
                xWriter.WriteElementString(HeadlineLevel.ToString(), Strings.SyntaxHeadline);
                String content = this.GenerateMethodSignature(methodDetails);
                
                // Output
                if (OutputField == OutputField.Crayon)
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
                Type attributeType = attribute.GetType();
                result.Append("[");
                result.Append(attributeType.Name);
                result.Append("]");
                result.Append(Environment.NewLine);
            }

            // Method Body
            if (methodDetails.IsPublic)
                result.Append("public ");
            else if (methodDetails.IsPrivate)
                result.Append("private ");
            else if (methodDetails.IsFamily)
                result.Append("protected ");

            // Further method attributes
            if (methodDetails.IsStatic)
                result.Append("static ");
            if (methodDetails.IsVirtual)
                result.Append("virtual ");

            // Method name and parameters
            result.Append(methodDetails.ReturnType.Name + " ");
            result.Append(methodDetails.Name);
            result.Append("(");
            foreach (ParameterInfo pInfo in methodDetails.GetParameters())
                result.Append(pInfo.ParameterType.Name + " " + pInfo.Name);
            result.Append(");");

            // Finished
            return result.ToString();
        }

        #endregion

        #region Field Documentation

        public string GetFieldDocumentation(FieldInfo fieldDetails, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Property Documentation

        public string GetPropertyDocumentation(PropertyInfo propertyDetails, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event Documentation

        public string GetEventDocumentation(EventInfo eventDetails, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Error Documentation

        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
