namespace Tradier.Client.Models.Streaming
{
    public struct MessageEvent
    {
        public const string DefaultName = "message";

        public MessageEvent(string name, string data)
        {
            Name = name ?? DefaultName;
            Data = data;
        }

        public string Name { get; }
        public string Data { get; }
    }
}
