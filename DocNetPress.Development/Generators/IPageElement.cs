﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.Generators
{
    /// <summary>
    /// Allows an object to be used as page element generator for DocNetPress
    /// </summary>
    public interface IPageElement
    {
        /// <summary>
        /// Generates HTML-Code from the given assembly member ready to insert into the post content
        /// </summary>
        /// <remarks>
        /// Do not include breaks or any kind of formatting at the beginning of the result HTML, the <see cref="DocNetPress.Generators.PostGenerator"/>
        /// will take care of the space between the page elements automatically. If your generator is not able to process the given input, return null.
        /// </remarks>
        /// <param name="xmlFile">The path to the XML-File containing the assembly documentation</param>
        /// <param name="memberNodeXPath">The "member"-Attribute text</param>
        /// <param name="culture">The culture to output the HTML-Code by</param>
        /// <returns>The generated documentation HTML-Code ready to insert into the post content</returns>
        String GetPageContent(String xmlFile, String memberNodeXPath, CultureInfo culture);
    }
}
