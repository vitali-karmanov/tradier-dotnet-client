namespace Tradier.Client.Models.Streaming
{
    // Represents the state of the connection.
    public enum ReadyState
    {
        // The initial state of the connection.
        Raw = 0,
        // The connection has not yet been established, or it was closed and is reconnecting.
        Connecting = 1,
        // The connection is open and is processing events as it receives them.
        Open = 2,
        // The connection is closed. This could also occur when an error is received.
        Closed = 3,
        // The connection has been shutdown explicitly by the consumer using the Close method.
        Shutdown = 4
    }
}
