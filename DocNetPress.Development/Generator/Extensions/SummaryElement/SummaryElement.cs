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
        /// Initializes a new <see cref="DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement"/>
        /// </summary>
        public SummaryElement() { }

        /// <summary>
        /// Initializes a new <see cref="DocNetPress.Development.Generator.Extensions.SummaryElement.SummaryElement"/>
        /// </summary>
        public SummaryElement(HeadlineLevel headlineLevel)
        {
            this.HeadlineLevel = headlineLevel;
        }

        /// <summary>
        /// Outputs the documentation summary of the given <see cref="System.Type"/>
        /// </summary>
        /// <param name="typeDetails">A <see cref="System.Type"/> instance for the given type to document</param>
        /// <param name="documentationNode">An <see cref="System.Xml.XmlElement"/> instance for access to the XML documentation node</param>
        /// <param name="language">
        /// The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> determining the programming language any generated
        /// code shall be outputted in
        /// </param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> giving the desired output culture</param>
        /// <returns>HTML code of the type summary ready to insert into the WordPress post</returns>
        public string GetTypeDocumentation(Type typeDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode, culture);
        }

        /// <summary>
        /// Outputs the documentation summary of the given method
        /// </summary>
        /// <param name="methodDetails">A <see cref="System.Reflection.MethodInfo"/> for further information about the method to document</param>
        /// <param name="documentationNode">An <see cref="System.Xml.XmlElement"/> instance for access to the XML documentation node</param>
        /// <param name="language">
        /// The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> determining the programming language any generated
        /// code shall be outputted in
        /// </param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> giving the desired output culture</param>
        /// <returns>HTML code of the method summary ready to insert into the WordPress post</returns>
        public string GetMethodDocumentation(MethodInfo methodDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode, culture);
        }

        /// <summary>
        /// Outputs the documentation summary of the given field
        /// </summary>
        /// <param name="methodDetails">A <see cref="System.Reflection.FieldInfo"/> for further information about the field to document</param>
        /// <param name="documentationNode">An <see cref="System.Xml.XmlElement"/> instance for access to the XML documentation node</param>
        /// <param name="language">
        /// The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> determining the programming language any generated
        /// code shall be outputted in
        /// </param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> giving the desired output culture</param>
        /// <returns>HTML code of the method summary ready to insert into the WordPress post</returns>
        public string GetFieldDocumentation(FieldInfo fieldDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode, culture);
        }

        /// <summary>
        /// Outputs the documentation summary of the given property
        /// </summary>
        /// <param name="propertyDetails">A <see cref="System.Reflection.PropertyInfo"/> for further information about the property to document</param>
        /// <param name="documentationNode">An <see cref="System.Xml.XmlElement"/> instance for access to the XML documentation node</param>
        /// <param name="language">
        /// The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> determining the programming language any generated
        /// code shall be outputted in
        /// </param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> giving the desired output culture</param>
        /// <returns>HTML code of the method summary ready to insert into the WordPress post</returns>
        public string GetPropertyDocumentation(PropertyInfo propertyDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode, culture);
        }

        /// <summary>
        /// Outputs the documentation summary of the given event
        /// </summary>
        /// <param name="eventDetails">A <see cref="System.Reflection.EventInfo"/> for further information about the event to document</param>
        /// <param name="documentationNode">An <see cref="System.Xml.XmlElement"/> instance for access to the XML documentation node</param>
        /// <param name="language">
        /// The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> determining the programming language any generated
        /// code shall be outputted in
        /// </param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> giving the desired output culture</param>
        /// <returns>HTML code of the method summary ready to insert into the WordPress post</returns>
        public string GetEventDocumentation(EventInfo eventDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode, culture);
        }

        /// <summary>
        /// Outputs the documentation summary of the given namespace
        /// </summary>
        /// <param name="nameSpace">The full path of the namespace to document</param>
        /// <param name="documentationNode">An <see cref="System.Xml.XmlElement"/> instance for access to the XML documentation node</param>
        /// <param name="language">
        /// The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> determining the programming language any generated
        /// code shall be outputted in
        /// </param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> giving the desired output culture</param>
        /// <returns>HTML code of the method summary ready to insert into the WordPress post</returns>
        public string GetNamespaceDocumentation(string nameSpace, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode, culture);
        }

        /// <summary>
        /// Outputs the documentation summary of the given unresolved documentation element
        /// </summary>
        /// <param name="assemblyPath">The path to the assembly that should actually contain the member to document</param>
        /// <param name="fullMemberName">The full path to the member to document</param>
        /// <param name="documentationNode">An <see cref="System.Xml.XmlElement"/> instance for access to the XML documentation node</param>
        /// <param name="language">
        /// The <see cref="DocNetPress.Development.Generator.Extensions.OutputLanguage"/> determining the programming language any generated
        /// code shall be outputted in
        /// </param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> giving the desired output culture</param>
        /// <returns>HTML code of the method summary ready to insert into the WordPress post</returns>
        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GetPostContent(documentationNode, culture);
        }

        /// <summary>
        /// Generates the summary page of a given documentation node content
        /// </summary>
        /// <param name="nodeContent">The content of the node currently being parsed</param>
        /// <returns>Valid HTML-Code ready for insertion into the Post</returns>
        private String GetPostContent(XmlElement nodeContent, CultureInfo culture = null)
        {
            String summaryText = nodeContent.SelectSingleNode("./summary").InnerText;

            if (!String.IsNullOrEmpty(summaryText) && !String.IsNullOrWhiteSpace(summaryText))
            {
                using (StringWriter sw = new StringWriter())
                using (var xWriter = XmlWriter.Create(sw))
                {
                    xWriter.WriteElementString(HeadlineLevel.ToString(), Strings.ResourceManager.GetString("SummaryHeadline", culture));
                    xWriter.WriteElementString("p", summaryText);

                    return sw.ToString();
                }
            }
            else
                return null;
        }
    }
}
