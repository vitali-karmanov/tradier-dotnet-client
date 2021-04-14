using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Tradier.Client;
using TradierClientTest.Helpers;

namespace TradierClientTest
{
    public class MarketDataTests
    {
        private TradierClient _client;
        private TradierClientConfiguration _configuration;

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
        }

        [Test]
        [TestCase("GME", "daily")]
        public async Task GetMultiDayHistoricalQuotesTest(string symbol, string interval)
        {
            var start = TimingHelper.GetLastWednesday();
            var end = TimingHelper.GetLastThursday();
            var result = await _client.MarketData.GetHistoricalQuotes(symbol, interval, start, end);

            Assert.IsNotNull(result.Day);
            Assert.AreEqual(result.Day.Count, 2);
            var firstDay = result.Day.First();
            var secondDay = result.Day.Last();
            Assert.AreEqual(firstDay.Date, start.ToString("yyyy-MM-dd"));
            Assert.NotZero(firstDay.Open);
            Assert.AreEqual(secondDay.Date, end.ToString("yyyy-MM-dd"));
            Assert.NotZero(secondDay.Open);
        }

        [Test]
        [TestCase("GME", "daily")]
        public async Task GetSingleDayHistoricalQuotesTest(string symbol, string interval)
        {
            var start = TimingHelper.GetLastWednesday();
            var end = TimingHelper.GetLastWednesday();
            var result = await _client.MarketData.GetHistoricalQuotes(symbol, interval, start, end);

            Assert.IsNotNull(result.Day);
            Assert.AreEqual(result.Day.Count, 1);
            var firstDay = result.Day.First();
            Assert.AreEqual(firstDay.Date, start.ToString("yyyy-MM-dd"));
            Assert.NotZero(firstDay.Open);
        }
    }
}