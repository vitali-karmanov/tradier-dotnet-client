namespace TradierClient.Test.Helpers
{
    public class TradierClientSettings
    {
        public string ApiToken { get; set; } = string.Empty;
        public string SandboxApiToken { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public bool UseProduction { get; set; } = false;
    }
}
