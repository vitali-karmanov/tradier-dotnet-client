using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tradier.Client;
using Tradier.Client.Models.Streaming;
using TradierClientTest.Helpers;

namespace TradierClientTest
{
    public class StreamingTests
    {
        private TradierClient _client;
        private TradierClientConfiguration _configuration;
        private Stream _session;

        [SetUp]
        public void Init()
        {
            _configuration = TestHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);
        }

        [SetUp]
        public void Setup()
        {
            var apiToken = _configuration.ApiToken;
            _client = new TradierClient(apiToken, true);

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
