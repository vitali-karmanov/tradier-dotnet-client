using Microsoft.Extensions.Configuration;
using TradierClient.Test.Helpers;

namespace TradierClient.Test
{
    public static class Configuration
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

        public static Settings GetApplicationConfiguration(string outputPath)
        {
            var settings = new Settings();

            var config = GetIConfigurationRoot(outputPath);

            config.GetSection("TradierClientSettings")
                .Bind(settings);

            return settings;
        }
    }
}
