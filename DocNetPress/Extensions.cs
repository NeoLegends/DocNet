using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress
{
    /// <summary>
    /// Contains extension methods to various .NET Framework types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets all members from an <see cref="Assembly"/>.
        /// </summary>
        /// <param name="asm">The <see cref="Assembly"/> to get the members from.</param>
        /// <returns>A collection of all members in the <see cref="Assembly"/>.</returns>
        public static IEnumerable<MemberInfo> GetMembers(this Assembly asm)
        {
            Contract.Requires<ArgumentNullException>(asm != null);

            return asm.GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        }

        /// <summary>
        /// Gets all members from an <see cref="Assembly"/>.
        /// </summary>
        /// <param name="flags"><see cref="BindingFlags"/> used to define the search for members.</param>
        /// <param name="asm">The <see cref="Assembly"/> to get the members from.</param>
        /// <returns>A collection of all members in the <see cref="Assembly"/>.</returns>
        public static IEnumerable<MemberInfo> GetMembers(this Assembly asm, BindingFlags flags)
        {
            Contract.Requires<ArgumentNullException>(asm != null);

            IEnumerable<Type> assemblyTypes = (flags == BindingFlags.NonPublic) ? asm.GetTypes() : asm.GetExportedTypes();
            return assemblyTypes.SelectMany(type => type.GetMembers(flags));
        }
    }
}
