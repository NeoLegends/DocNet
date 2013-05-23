using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress.Development.Generator.Extensions
{
    /// <summary>
    /// Allows an object to be used as type page element generator for DocNetPress
    /// </summary>
    public interface IPageElement
    {
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
        String GetTypeDocumentation(Type typeDetails, String documentationNode, CultureInfo culture = null);

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
        String GetMethodDocumentation(MethodInfo methodDetails, String documentationNode, CultureInfo culture = null);

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
        String GetFieldDocumentation(FieldInfo fieldDetails, String documentationNode, CultureInfo culture = null);

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
        String GetPropertyDocumentation(PropertyInfo propertyDetails, String documentationNode, CultureInfo culture = null);

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
        String GetEventDocumentation(EventInfo eventDetails, String documentationNode, CultureInfo culture = null);

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
        String GetErrorDocumentation(String assemblyPath, String fullMemberName, String documentationNode, CultureInfo culture = null);
    }
}
