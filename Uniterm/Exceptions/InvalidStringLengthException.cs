using System;

namespace Uniterm.Exceptions
{
    public class InvalidStringLengthException : Exception
    {
        public InvalidStringLengthException()
            : base("The string length is invalid.") { }

        public InvalidStringLengthException(string message)
            : base(message) { }

        public InvalidStringLengthException(string message, Exception inner)
            : base(message, inner) { }
    }
}
