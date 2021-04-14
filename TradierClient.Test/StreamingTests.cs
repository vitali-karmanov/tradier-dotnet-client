﻿using System.Threading.Tasks;
using NUnit.Framework;
using Tradier.Client.Models.Streaming;
using TradierClient.Test.Helpers;

namespace TradierClient.Test
{
    public class StreamingTests
    {
        private Tradier.Client.TradierClient _client;
        private Settings _settings;
        private Stream _session;

        [SetUp]
        public void Init()
        {
            _settings = Configuration.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);
        }

        [SetUp]
        public void Setup()
        {
            // Use SandBox API Token
            //var sandboxApiToken = _settings.SandboxApiToken;
            //_client = new Tradier.Client.TradierClient(sandboxApiToken);

            //Use Production API Token
            var apiToken = _settings.ApiToken;
            _client = new Tradier.Client.TradierClient(apiToken, true);

            _session = _client.Streaming.CreateMarketSession();
        }

        [Test]
        [TestCase("AAPL", StreamingFilter.None, false, false, false)]
        [TestCase("AAPL", StreamingFilter.Trade, false, false, false)]
        [TestCase("AAPL", StreamingFilter.Quote, false, false, false)]
        public async Task GetMultiDayHistoricalQuotesTest(string symbols, StreamingFilter filter, bool lineBreak, bool validOnly, bool advancedDetails)
        {
            using var stream = _client.Streaming.GetStreamingQuotes(
                _session.SessionId,
                symbols,
                filter,
                lineBreak,
                validOnly,
                advancedDetails
            );
            stream.Error += (s, e) =>
            {
                stream.Dispose();
                Assert.Fail("Error Occurred. Details: {0}", e.Exception.Message);
            };
            stream.MessageReceived += (s, m) =>
            {
                stream.Dispose();
                Assert.Pass(m.ToString());
            };
            await stream.StartAsync();
        }
    }
}