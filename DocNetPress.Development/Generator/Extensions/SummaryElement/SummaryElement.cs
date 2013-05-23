using DocNetPress.Development.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocNetPress.Development.Generator.Extensions.SummaryElement
{
    /// <summary>
    /// Generates the summary part of a documentation page
    /// </summary>
    [Serializable]
    public class SummaryElement :  IPostElement
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
        /// Generates HTML-Code from the given assembly member ready to insert into the post content
        /// </summary>
        /// <param name="assemblyPath">The path to the DLL file for member access using reflection</param>
        /// <param name="memberType">The type of the given documentation node</param>
        /// <param name="nodeContent">The content of the read member node</param>
        /// <param name="nodeMemberAttribute">The "member"-Attribute text</param>
        /// <param name="culture">The culture to output the HTML-Code in</param>
        /// <returns>The generated documentation HTML-Code ready to insert into the post content</returns>
        public String GetPostContent(String assemblyPath, String nodeContent, MemberTypes memberType, String nodeMemberAttribute, CultureInfo culture = null)
        {
            // Load up the Xml Code and get the summary node
            xmlDocument.LoadXml(nodeContent);
            String summary = xmlDocument["summary"].InnerText;

            // If summary is null, we have no summary node available so we return null just as requested
            if (summary != null)
            {
                // Compose the HTML-Code and return it
                using (StringWriter sw = new StringWriter())
                using (var xWriter = XmlWriter.Create(sw))
                {
                    xWriter.WriteElementString("h" + HeadlineLevel.ToString(), Strings.Summary);
                    xWriter.WriteString(Environment.NewLine + summary);
                    return sw.ToString();
                }
            }
            else
                return null;
        }
    }
}
