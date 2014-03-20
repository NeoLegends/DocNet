using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DocNet.Documentation;

namespace DocNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Contract.Requires<ArgumentNullException>(args != null);
            Contract.Requires<ArgumentException>(args.Length >= 0);

            Documentation.Documentation docs = new XmlDocumentationParser().ParseAsync(
                Assembly.GetExecutingAssembly().Location,
                Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, "xml")
            ).Result;

            Console.WriteLine(docs.Types.Count());
            Console.ReadLine();
        }
    }
}
