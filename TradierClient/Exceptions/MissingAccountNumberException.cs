using System;

namespace Tradier.Client.Exceptions
{
    public class MissingAccountNumberException : Exception
    {
        public MissingAccountNumberException()
        {
        }

        public MissingAccountNumberException(string message)
            : base(message)
        {
        }

        public MissingAccountNumberException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
