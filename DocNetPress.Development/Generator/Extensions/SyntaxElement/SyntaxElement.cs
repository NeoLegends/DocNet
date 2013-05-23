using DocNetPress.Development.Resources;
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
    /// Generates the "Syntax" part of a documentation page
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
        /// Backing field for HeadlineLevel
        /// </summary>
        private int _HeadlineLevel = 2;

        /// <summary>
        /// The level of the headline to use
        /// </summary>
        public int HeadlineLevel
        {
            get
            {
                return _HeadlineLevel;
            }
            set
            {
                if (value > 6)
                    _HeadlineLevel = 6;
                else if (value < 1)
                    _HeadlineLevel = 1;
                else
                    _HeadlineLevel = value;
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
        
        public string GetTypeDocumentation(Type typeDetails, string documentationNode, CultureInfo culture = null)
        {
            /* Old Colde

                // Variables
                using (StringWriter sw = new StringWriter())
                using (var xWriter = XmlWriter.Create(sw))
                {
                    // Headline
                    xWriter.WriteElementString("h" + HeadlineLevel.ToString(), Strings.Syntax);

                    // Content
                    String content = String.Empty;
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
             
            */ 
        }

        public string GetMethodDocumentation(MethodInfo methodDetails, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public string GetFieldDocumentation(FieldInfo fieldDetails, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public string GetPropertyDocumentation(PropertyInfo propertyDetails, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public string GetEventDocumentation(EventInfo eventDetails, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, string documentationNode, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }
    }
}
