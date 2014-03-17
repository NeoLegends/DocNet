using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressDocNet.Documentation
{
    /// <summary>
    /// Represents a parser class parsing .NET XML documentation.
    /// </summary>
    [ContractClass(typeof(IDocumentationParserContracts))]
    public interface IDocumentationParser
    {
        /// <summary>
        /// Asynchronously parses documentation for the specified assembly.
        /// </summary>
        /// <param name="assemblyPath">The full path to the assembly being documented.</param>
        /// <param name="documentationPath">The path to the file containing the documentation.</param>
        /// <param name="includePrivate"><c>true</c> if the method shall parse documentation for private members as well, otherwise <c>false</c>.</param>
        /// <returns>A <see cref="Task{T}"/> representing the asynchronous parsing process.</returns>
        Task<Documentation> ParseAsync(String assemblyPath, String documentationPath);
    }

    /// <summary>
    /// Contains the contract definitions for <see cref="IDocumentationParser"/>.
    /// </summary>
    [ContractClassFor(typeof(IDocumentationParser))]
    abstract class IDocumentationParserContracts : IDocumentationParser
    {
        /// <summary>
        /// Contains contract definitions.
        /// </summary>
        Task<Documentation> IDocumentationParser.ParseAsync(String assemblyPath, String documentationPath)
        {
            Contract.Requires<ArgumentNullException>(assemblyPath != null);
            Contract.Requires<ArgumentNullException>(documentationPath != null);
            Contract.Ensures(Contract.Result<Task<Documentation>>() != null);

            return null;
        }
    }
}
