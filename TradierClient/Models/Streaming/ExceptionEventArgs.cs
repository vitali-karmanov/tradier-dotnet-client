using System;

namespace Tradier.Client.Models.Streaming
{
    public class ExceptionEventArgs : EventArgs
    {
        public ExceptionEventArgs(System.Exception ex)
        {
            Exception = ex;
        }

        public System.Exception Exception { get; }
    }
}
