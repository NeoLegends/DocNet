using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocNetPress.Development.Generators
{
    /// <summary>
    /// Generates the summary part of a documentation page
    /// </summary>
    public class SummaryElement : IPageElement
    {
        /// <summary>
        /// XML-Access XmlDocument
        /// </summary>
        private XmlDocument xmlDoc;

        /// <summary>
        /// The path to the XmlDocument
        /// </summary>
        private String xmlFile;

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.IPageElement"/>
        /// </summary>
        /// <param name="xmlFile">The path to the XML-File to load the XML-File from</param>
        /// <param name="memberNodeXPath">The name of the XmlNode to parse</param>
        /// <param name="culture">The culture to generate the output from</param>
        /// <returns>Well-formatted HTML-Code containing the summary description of an Element</returns>
        public String GetPageContent(String xmlFile, String memberNodeXPath, CultureInfo culture)
        {
            if (this.xmlFile != xmlFile)
            {
                if (this.xmlDoc == null)
                    this.xmlDoc = new XmlDocument();
                this.xmlDoc.Load(xmlFile);
                this.xmlFile = xmlFile;
            }

            xmlDoc.SelectSingleNode(memberNodeXPath);

            throw new NotImplementedException();
        }
    }
}
