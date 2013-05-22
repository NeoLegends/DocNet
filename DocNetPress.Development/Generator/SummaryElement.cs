using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocNetPress.Development.Generator
{
    /// <summary>
    /// Generates the summary part of a documentation page
    /// </summary>
    public class SummaryElement : IPostElement
    {
        /// <summary>
        /// Backing field for HeadlineLevel
        /// </summary>
        private int _HeadlineLevel = 2;

        /// <summary>
        /// The headline level that shall be used for the "Summary"-Headline
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
        /// <param name="nodeType">The type of the given documentation node</param>
        /// <param name="nodeContent">The content of the read member node</param>
        /// <param name="nodeMemberAttribute">The "member"-Attribute text</param>
        /// <param name="culture">The culture to output the HTML-Code in</param>
        /// <returns>The generated documentation HTML-Code ready to insert into the post content</returns>
        public String GetPostContent(MemberType nodeType, String nodeContent, String nodeMemberAttribute, CultureInfo culture = null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(nodeContent);
            String summary = xmlDoc["summary"].InnerText;

            if (summary != null)
            {
                using (StringWriter sw = new StringWriter())
                using (var xWriter = XmlWriter.Create(sw))
                {
                    xWriter.WriteElementString(
                            "h" + HeadlineLevel.ToString(), 
                            (culture != null) ? SummaryElement.GetLocalizedSummaryText(culture) : "Summary"
                        );
                    xWriter.WriteString(Environment.NewLine + summary);

                    return sw.ToString();
                }
            }
            
            return null;
        }

        /// <summary>
        /// Gets the Summary headline based on the given culture
        /// </summary>
        /// <param name="culture">The culture to localize the summary headline by</param>
        /// <returns>The localized culture</returns>
        private static String GetLocalizedSummaryText(CultureInfo culture)
        {
            switch (culture.TwoLetterISOLanguageName)
            {
                case "en":
                    return "Summary";
                case "de":
                    return "Zusammenfassung";
                case "fr":
                    return "Résumé";
                case "it":
                    return "Sommario";
                case "ru":
                    return "подведе́ние ито́гов";
                case "es":
                    return "El Resumen";
                default:
                    return "Summary";
            }
        }
    }
}
