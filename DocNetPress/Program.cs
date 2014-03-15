using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocNetPress
{
    class Program
    {
        static void Main(string[] args)
        {
            Contract.Requires<ArgumentNullException>(args != null);
            Contract.Requires<ArgumentException>(args.Length >= 0);
        }
    }
}
