using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DocNetPress.Development.Generator.Extensions.SeeAlsoElement
{
    /// <summary>
    /// An <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/> generating the "See also" part of the page
    /// </summary>
    [Serializable]
    public class SeeAlsoElement : IPageElement
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
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string GetTypeDocumentation(Type typeDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode, culture);
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string GetMethodDocumentation(MethodInfo methodDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode, culture);
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string GetFieldDocumentation(FieldInfo fieldDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode, culture);
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string GetPropertyDocumentation(PropertyInfo propertyDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode, culture);
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string GetEventDocumentation(EventInfo eventDetails, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode, culture);
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string GetNamespaceDocumentation(string nameSpace, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode, culture);
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, XmlElement documentationNode, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode, culture);
        }

        /// <summary>
        /// Generstes the see also part of a page of the given <see cref="System.Xml.XmlElement"/> and the given <see cref="System.Globalization.CultureInfo"/>
        /// </summary>
        /// <param name="documentationNode"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        private String GenerateSeeAlsoElement(XmlElement documentationNode, CultureInfo culture = null)
        {
            XmlNodeList nodes = documentationNode.SelectNodes("./seealso");
            if (nodes.Count > 0)
                return this.WriteSeeAlsoNodes(nodes, culture);
            else
                return null;
        }

        /// <summary>
        /// Writes the see also nodes into the post
        /// </summary>
        /// <param name="nodes">The "seealso"-XmlNodes</param>
        /// <param name="culture">The culture to generate the output in</param>
        /// <returns>The post content</returns>
        private String WriteSeeAlsoNodes(XmlNodeList nodes, CultureInfo culture = null)
        {
            StringBuilder result = new StringBuilder(200);

            using (StringWriter sw = new StringWriter())
            using (var xWriter = XmlWriter.Create(sw))
            {
                xWriter.WriteElementString(HeadlineLevel.ToString(), Strings.ResourceManager.GetString("SeeAlsoHeadline", culture));

                xWriter.WriteStartElement("ul");
                foreach (XmlNode node in nodes)
                {
                    xWriter.WriteStartElement("li");
                    node.WriteTo(xWriter);
                    xWriter.WriteEndElement();
                }
                xWriter.WriteEndElement();

                xWriter.WriteEndDocument();
                result.Append(sw.ToString());
            }

            return result.ToString();
        }
    }
}
