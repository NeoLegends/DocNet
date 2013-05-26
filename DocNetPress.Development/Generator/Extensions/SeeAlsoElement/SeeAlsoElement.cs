using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace DocNetPress.Development.Generator.Extensions.SeeAlsoElement
{
    [Serializable]
    public class SeeAlsoElement : IPageElement
    {
        /// <summary>
        /// The PageElement's name
        /// </summary>
        public string Name
        {
            get 
            {
                return "SeeAlsoElement";
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
        /// backing field for SeeAlsoNodeName
        /// </summary>
        private String _SeeAlsoNodeName = "seealso";

        /// <summary>
        /// The (local) name of the node taken as "see also" element
        /// </summary>
        public String SeeAlsoNodeName
        {
            get
            {
                return _SeeAlsoNodeName;
            }
            set
            {
                _SeeAlsoNodeName = value;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsCSharp
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsVBNET
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsFSharp
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Derived from <see cref="DocNetPress.Development.Generator.Extensions.IPageElement"/>
        /// </summary>
        public bool SupportsJScript
        {
            get
            {
                return true;
            }
        }

        public string GetTypeDocumentation(Type typeDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode);
        }

        public string GetMethodDocumentation(MethodInfo methodDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode);
        }

        public string GetFieldDocumentation(FieldInfo fieldDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode);
        }

        public string GetPropertyDocumentation(PropertyInfo propertyDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode);
        }

        public string GetEventDocumentation(EventInfo eventDetails, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode);
        }

        public string GetNamespaceDocumentation(string nameSpace, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode);
        }

        public string GetErrorDocumentation(string assemblyPath, string fullMemberName, XmlElement documentationNode, OutputLanguage language, CultureInfo culture = null)
        {
            return this.GenerateSeeAlsoElement(documentationNode);
        }

        private String GenerateSeeAlsoElement(XmlElement documentationNode)
        {
            XmlNodeList nodes = documentationNode.SelectNodes("./" + this.SeeAlsoNodeName);

            if (nodes.Count > 0)
            {
                StringBuilder result = new StringBuilder(150);



                return result.ToString();
            }
            else
                return null;
        }
    }
}
