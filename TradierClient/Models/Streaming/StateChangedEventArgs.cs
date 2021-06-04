using System;

namespace Tradier.Client.Models.Streaming
{
    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(ReadyState readyState)
        {
            ReadyState = ReadyState;
        }

        public ReadyState ReadyState { get; }
    }
}
