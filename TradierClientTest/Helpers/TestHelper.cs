using Microsoft.Extensions.Configuration;

namespace TradierClientTest.Helpers
{
    public class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("22c17c56-e539-4af5-ae78-74e598d1e8a7")
                .AddEnvironmentVariables()
                .Build();
        }

        public static TradierClientConfiguration GetApplicationConfiguration(string outputPath)
        {
            var configuration = new TradierClientConfiguration();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .GetSection("TradierClientSettings")
                .Bind(configuration);

            return configuration;
        }
    }
}
