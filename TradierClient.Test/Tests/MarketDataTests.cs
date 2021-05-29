using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Tradier.Client;
using TradierClient.Test.Helpers;

namespace TradierClient.Test.Tests
{
    public class MarketDataTests
    {
        private Tradier.Client.TradierClient _client;
        private Settings _settings;

        [SetUp]
        public void Init()
        {
            _settings = Configuration.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);
        }

        [SetUp]
        public void Setup()
        {
            // Use SandBox API Token
            var sandboxApiToken = _settings.SandboxApiToken;
            _client = new Tradier.Client.TradierClient(sandboxApiToken);

            //Use Production API Token
            //var apiToken = _settings.ApiToken;
            //_client = new Tradier.Client.TradierClient(apiToken, true);
        }

        [Test]
        [TestCase("CKH", false)]
        public async Task PostGetQuotesForSingleSymbol(string symbols, bool greeks)
        {
            var result = await _client.MarketData.PostGetQuotes(symbols, greeks);
            Assert.IsNotNull(result.Quote);
            Assert.AreEqual(1, result.Quote.Count);

            var firstDay = result.Quote.First();
        }

        [Test]
        [TestCase("GME", "daily")]
        public async Task GetMultiDayHistoricalQuotesTest(string symbol, string interval)
        {
            var start = TimingHelper.GetLastWednesday();
            var end = TimingHelper.GetLastThursday();
            var result = await _client.MarketData.GetHistoricalQuotes(symbol, interval, start, end);
            Assert.IsNotNull(result.Day);
            Assert.AreEqual(2, result.Day.Count);

            var firstDay = result.Day.First();
            var secondDay = result.Day.Last();
            Assert.AreEqual(start.ToString("yyyy-MM-dd"), firstDay.Date);
            Assert.NotZero(firstDay.Open);
            Assert.AreEqual(end.ToString("yyyy-MM-dd"), secondDay.Date);
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
            Assert.AreEqual(1, result.Day.Count);

            var firstDay = result.Day.First();
            Assert.AreEqual(start.ToString("yyyy-MM-dd"), firstDay.Date);
            Assert.NotZero(firstDay.Open);
        }
    }
}