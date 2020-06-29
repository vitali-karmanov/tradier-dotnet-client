using System;

namespace Tradier.Client.Exceptions
{
    public class MissingDefaultAccountNumberException : Exception
    {
        public MissingDefaultAccountNumberException()
        {
        }

        public MissingDefaultAccountNumberException(string message)
            : base(message)
        {
        }

        public MissingDefaultAccountNumberException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
