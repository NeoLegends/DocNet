using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Allows an object to be used as type page element generator for DocNetPress
    /// </summary>
    /// <remarks>
    /// If you can't return any documentation code / text for the given input, simply return null. Also make sure you don't change nodes
    /// with the cref attribute, they will be resolved into links to the other documentation pages after the page has been put together.
    /// </remarks>
    public interface IPageElement
    {
        /// <summary>
        /// The size of the headline to use
        /// </summary>
        HeadlineLevel HeadlineLevel { set; }

        /// <summary>
        /// Generates type documentation based on the given <see cref="System.Type"/>, the current documentation node, the output 
        /// language and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="typeDetails">The <see cref="System.Type"/> for further information about the kind of type to be documented</param>
        /// <param name="documentationNode">The <see cref="System.Xml.XmlElement"/> containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post (or null if the your generator wasn't able to output code into or your generator
        /// can not parse the given input at all)
        /// </returns>
        String GetTypeDocumentation(Type typeDetails, XmlElement documentationNode, CultureInfo culture = null);

        /// <summary>
        /// Generates method documentation based on the given <see cref="System.Reflection.MethodInfo"/>, the current documentation node, the output 
        /// language and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="methodDetails">The <see cref="System.Type"/> for further information about the method to be documented</param>
        /// <param name="documentationNode">The <see cref="System.Xml.XmlElement"/> containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post (or null if the your generator wasn't able to output code into or your generator
        /// can not parse the given input at all)
        /// </returns>
        String GetMethodDocumentation(MethodInfo methodDetails, XmlElement documentationNode, CultureInfo culture = null);

        /// <summary>
        /// Generates field documentation based on the given <see cref="System.Reflection.FieldInfo"/>, the current documentation node, the output 
        /// language and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="fieldDetails">The <see cref="System.Reflection.FieldInfo"/> providing further information about the field to document</param>
        /// <param name="documentationNode">The <see cref="System.Xml.XmlElement"/> containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post (or null if the your generator wasn't able to output code into or your generator
        /// can not parse the given input at all)
        /// </returns>
        String GetFieldDocumentation(FieldInfo fieldDetails, XmlElement documentationNode, CultureInfo culture = null);

        /// <summary>
        /// Generates property documentation based on the given <see cref="System.Reflection.PropertyInfo"/>, the current documentation node, the output 
        /// language and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="propertyDetails">Provides further information about the property to be documentated</param>
        /// <param name="documentationNode">The <see cref="System.Xml.XmlElement"/> containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post (or null if the your generator wasn't able to output code into or your generator
        /// can not parse the given input at all)
        /// </returns>
        String GetPropertyDocumentation(PropertyInfo propertyDetails, XmlElement documentationNode, CultureInfo culture = null);

        /// <summary>
        /// Generates event documentation based on the given <see cref="System.Reflection.EventInfo"/>, the current documentation node, the output 
        /// language and optionally a given culture to generate the output in
        /// </summary>
        /// <param name="eventDetails"><see cref="System.Reflection.EventInfo"/> containing further data about the event to document</param>
        /// <param name="documentationNode">The <see cref="System.Xml.XmlElement"/> containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Valid HTML-Code ready to insert into a WordPress post (or null if the your generator wasn't able to output code into or your generator
        /// can not parse the given input at all)
        /// </returns>
        String GetEventDocumentation(EventInfo eventDetails, XmlElement documentationNode, CultureInfo culture = null);

        /// <summary>
        /// Generates namespace documenation based on the namespace path, the current documentation node, the output language and optionally a given culture
        /// to generate the output in
        /// </summary>
        /// <param name="nameSpace">The full name / path of the namespace</param>
        /// <param name="documentationNode">The <see cref="System.Xml.XmlElement"/> containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Generated HTML-Code from the given documentation node (or null if the your generator wasn't able to output code into or your generator
        /// can not parse the given input at all)
        /// </returns>
        String GetNamespaceDocumentation(String nameSpace, XmlElement documentationNode, CultureInfo culture = null);

        /// <summary>
        /// This method is fired when there's a reference inside the XML documentation code the compiler couldn't resolve at compile time, so it's not determined what
        /// the documentated element actually is. (It can be a type, field, property, event, etc) I leave it up to you to deal with those cases
        /// </summary>
        /// <param name="assemblyPath">The path to the assembly the documentation code belongs to</param>
        /// <param name="fullMemberName">The full name of the member that failed to document</param>
        /// <param name="documentationNode">The <see cref="System.Xml.XmlElement"/> containing all user-written documentation text</param>
        /// <param name="culture">The culture to generate the documentation in</param>
        /// <param name="language">In which language the generator is supposed to output the eventually generated code in</param>
        /// <returns>
        /// Generated HTML-Code from the given documentation node (or null if the your generator wasn't able to output code into or your generator
        /// can not parse the given input at all)
        /// </returns>
        String GetErrorDocumentation(String assemblyPath, String fullMemberName, XmlElement documentationNode, CultureInfo culture = null);
    }
}
