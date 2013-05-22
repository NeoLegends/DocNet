using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    internal class UndefinedTypeException : Exception
    {
        public UndefinedTypeException() : base() { }

        public UndefinedTypeException(String message) : base(message) { }

        public UndefinedTypeException(String message, Exception innerException) : base(message, innerException) { }
    }
}
