using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Simplifies working with Xml documentation by adding some commonly used members
    /// </summary>
    public abstract class PageElement
    {
        /// <summary>
        /// An XmlDocument-Instance for easier dealing with XML
        /// </summary>
        protected XmlDocument XmlDocument = new XmlDocument();

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
    }
}
