using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterm
{
    public class InvalidStringLengthException : Exception
    {
        public InvalidStringLengthException()
            : base("The string length is invalid.")
        {
        }
        public InvalidStringLengthException(string message)
            : base(message)
        {
        }
        public InvalidStringLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
