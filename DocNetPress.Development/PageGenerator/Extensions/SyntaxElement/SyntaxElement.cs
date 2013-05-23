using DocNetPress.Development.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DocNetPress.Development.PageGenerator.Extensions.SyntaxElement
{
    /// <summary>
    /// Generates the "Syntax" part of a documentation page
    /// </summary>
    [Serializable]
    public class SyntaxElement : IPostElement
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
        
        /// <summary>
        /// Generates HTML-Code from the given assembly member ready to insert into the post content
        /// </summary>
        /// <param name="assemblyPath">The path to the DLL file for member access using reflection</param>
        /// <param name="memberType">The type of the given documentation node</param>
        /// <param name="nodeContent">The content of the read member node</param>
        /// <param name="fullMemberName">The "member"-Attribute text</param>
        /// <param name="culture">The culture to output the HTML-Code in</param>
        /// <returns>The generated documentation HTML-Code ready to insert into the post content</returns>
        public String GetPostContent(String assemblyPath, String nodeContent, TypeInformation memberType, String fullMemberName, CultureInfo culture = null)
        {
            using (StringWriter sw = new StringWriter())
            using (var xWriter = XmlWriter.Create(sw))
            {
                xWriter.WriteElementString("h" + HeadlineLevel.ToString(), Strings.Syntax);


                String content = MemberNameGenerator.GenerateMemberSignature(assemblyPath, fullMemberName, memberType);
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
    }
}
