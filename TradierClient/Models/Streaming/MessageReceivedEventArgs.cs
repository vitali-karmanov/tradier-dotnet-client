using System;

namespace Tradier.Client.Models.Streaming
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(MessageEvent message)
        {
            Message = message;
        }

        public MessageEvent Message { get; }
        public string EventName { get { return Message.Name; } }
    }
}
