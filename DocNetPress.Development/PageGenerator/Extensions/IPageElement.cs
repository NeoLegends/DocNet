using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Allows an object to be used as page element generator for DocNetPress
    /// </summary>
    public interface IPostElement
    {
        /// <summary>
        /// Generates HTML-Code from the given assembly member ready to insert into the post content
        /// </summary>
        /// <remarks>
        /// Do not include breaks or any kind of formatting at the beginning of the result HTML, the <see cref="DocNetPress.Generators.PostGenerator"/>
        /// will take care of the space between the page elements automatically. If your generator is not able to process the given input, return null.
        /// 
        /// If possible, take care of the given culture and change the language
        /// </remarks>
        /// <param name="assemblyPath">The path to the DLL file for member access using reflection</param>
        /// <param name="nodeType">The type of the given documentation node</param>
        /// <param name="nodeContent">The content of the read member node</param>
        /// <param name="nodeMemberAttribute">The "member"-Attribute text</param>
        /// <param name="culture">The culture to output the HTML-Code in</param>
        /// <returns>The generated documentation HTML-Code ready to insert into the post content</returns>
        String GetPostContent(String assemblyPath, String nodeContent, MemberType nodeType, String nodeMemberAttribute, CultureInfo culture = null);
    }
}
