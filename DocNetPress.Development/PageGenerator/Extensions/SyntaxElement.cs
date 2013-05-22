using DocNetPress.Development.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Generates the "Syntax" part of a documentation page
    /// </summary>
    public class SyntaxElement : IPostElement
    {
        /// <summary>
        /// An XmlDocument-Instance for easier dealing with XML
        /// </summary>
        protected XmlDocument xmlDocument = new XmlDocument();

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
        /// Backing field for OutputLanguage
        /// </summary>
        private OutputLanguage _OutputLanguage = OutputLanguage.CSharp;

        public OutputLanguage OutputLanguage 
        {
            get
            {
                return _OutputLanguage;
            }
            set
            {
                _OutputLanguage = value;
            }
        }
        
        /// <summary>
        /// Generates HTML-Code from the given assembly member ready to insert into the post content
        /// </summary>
        /// <param name="assemblyPath">The path to the DLL file for member access using reflection</param>
        /// <param name="nodeType">The type of the given documentation node</param>
        /// <param name="nodeContent">The content of the read member node</param>
        /// <param name="nodeMemberAttribute">The "member"-Attribute text</param>
        /// <param name="culture">The culture to output the HTML-Code in</param>
        /// <returns>The generated documentation HTML-Code ready to insert into the post content</returns>
        public String GetPostContent(String assemblyPath, String nodeContent, MemberTypes nodeType, String nodeMemberAttribute, CultureInfo culture = null)
        {
            using (StringWriter sw = new StringWriter())
            using (var xWriter = XmlWriter.Create(sw))
            {
                xWriter.WriteElementString("h" + HeadlineLevel.ToString(), Strings.Syntax);

                // Write content
                if (nodeType == MemberTypes.Method)
                {
                    if (OutputField == OutputField.Crayon)
                    {
                        xWriter.WriteStartElement("pre");
                        xWriter.WriteAttributeString("class", "lang:" + ((this.OutputLanguage == OutputLanguage.CSharp) ? "c#" : "vb") + "decode=true");
                        xWriter.WriteString(
                    }
                    else if (OutputField == OutputField.QuoteBox)
                    {
                        
                    }
                }
                else if (nodeType == MemberTypes.Property)
                {

                }
                else if (nodeType == MemberTypes.Field)
                {

                }
                else if (nodeType == MemberTypes.Event)
                {

                }

                return sw.ToString();
            }
        }

        private static void GenerateMemberSignature(String assemblyPath, String nodeMemberAttribute, MemberTypes nodeType)
        {
            return "public " + TypeExtractor.GetMethodReturnType(assemblyPath, nodeMemberAttribute).ToString() + TypeExtractor.FullNameToShortName(nodeMemberAttribute);
        }
    }
}
