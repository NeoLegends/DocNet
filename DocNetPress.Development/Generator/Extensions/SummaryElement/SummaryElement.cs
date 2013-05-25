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
    public class SummaryElement : IPageElement
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
        /// Backing field for SummaryNodeName
        /// </summary>
        private String _SummaryNodeName = "summary";

        /// <summary>
        /// The name of the node (default is "summary") being taken as data source for the summary page element
        /// </summary>
        public String SummaryNodeName
        {
            get
            {
                return _SummaryNodeName;
            }
            set
            {
                _SummaryNodeName = value;
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
                return "SummaryElement";
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
                return true;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsFSharp
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsJScript
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Generates the summary page of a given documentation node content
        /// </summary>
        /// <param name="nodeContent">The content of the node currently being parsed</param>
        /// <returns>Valid HTML-Code ready for insertion into the Post</returns>
        private String GetPostContent(String nodeContent)
        {
            // Load up the Xml Code and get the summary node
            xmlDocument.LoadXml(nodeContent);
            String summary = xmlDocument.SelectSingleNode("./" + this.SummaryNodeName).InnerText;

            // If summary is null, we have no summary node available so we return null just as requested
            if (summary != null)
            {
                // Compose the HTML-Code and return it
                using (StringWriter sw = new StringWriter())
                using (var xWriter = XmlWriter.Create(sw))
                {
                    xWriter.WriteElementString(HeadlineLevel.ToString(), Strings.SummaryHeadline);
                    xWriter.WriteString(Environment.NewLine + summary);

                    return sw.ToString();
                }
            }
            else
                return null;
        }

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Type"/>, the inner text of the documentation XmlNode currently being parsed
        /// and a given culture to generate the output in
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> for further information about the kind of type to be documented</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post or null if your <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// is not able to parse the given input
        /// </returns>
        public String GetTypeDocumentation(Type type, String documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode);
        }

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.MethodInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and a given culture to generate the output in
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> for further information about the method to be documented</param>
        /// <param name="documentationNode">The documentation code containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post or null if your <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// is not able to parse the given input
        /// </returns>
        public string GetMethodDocumentation(MethodInfo methodDetails, string documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode);
        }

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.FieldInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="fieldDetails">The <see cref="System.Reflection.FieldInfo"/> providing further information about the field to document</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post or null if your <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// is not able to parse the given input
        /// </returns>
        public string GetFieldDocumentation(FieldInfo fieldDetails, string documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode);
        }

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.PropertyInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="propertyDetails">Provides further information about the property to be documentated</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post or null if your <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// is not able to parse the given input
        /// </returns>
        public string GetPropertyDocumentation(PropertyInfo propertyDetails, string documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode);
        }

        /// <summary>
        /// Generates documentation based on the given <see cref="System.Reflection.EventInfo"/>, the inner text of the documentation XmlNode currently being parsed
        /// and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="eventDetails"><see cref="System.Reflection.EventInfo"/> containing further data about the event to document</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post or null if your <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// is not able to parse the given input
        /// </returns>
        public string GetEventDocumentation(EventInfo eventDetails, string documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode);
        }

        /// <summary>
        /// This method is fired when there's a reference inside the XML documentation code the compiler couldn't resolve at compile time, so it's not determined what
        /// the documentated element actually is. (It can be a type, field, property, event, etc) I leave it up to you to deal with those cases
        /// </summary>
        /// <param name="assemblyPath">The path to the assembly the documentation code belongs to</param>
        /// <param name="fullMemberName">The full name of the member that failed to document</param>
        /// <param name="documentationNode">The documentation node containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <returns>
        /// Generated HTML-Code from the given documentation node or null if your <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// is not able to parse the given input
        /// </returns>
        public string GetErrorDocumentation(string assemblyPath, string memberAttributeText, string documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode);
        }
    }
}
