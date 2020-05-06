using System;

namespace Tradier.Client.Config
{
    public class TradierClientConfig
    {
        private Uri ProductionUri { get; } = new Uri("https://api.tradier.com/v1/");

        private Uri SandboxUri { get; } = new Uri("https://sandbox.tradier.com/v1/");
        
        public Uri BaseUri { get; private set; } = new Uri("https://api.tradier.com/v1/");

        public string ApiToken { get; set; }
        
        public string AccountNumber { get; set; }

        public void UseSandbox()
        {
            BaseUri = SandboxUri;
        }

        public void UseProduction()
        {
            BaseUri = ProductionUri;
        }
    }
}
