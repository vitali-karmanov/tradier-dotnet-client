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
            Assert.IsNotNull(result.Quote.First());
            Assert.AreEqual(1, result.Quote.Count);
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

        [Test]
        [TestCase("AAPL")]
        [TestCase("AAPL,GOOG")]
        public async Task GetCompanyInfoTest(string symbols)
        {
            var result = await _client.MarketData.GetCompany(symbols);

            var companyData = result.FirstOrDefault().Results.FirstOrDefault(x => x.Tables?.CompanyProfile != null);
            Assert.IsNotNull(companyData);
            Assert.IsNotNull(companyData?.Tables?.CompanyProfile?.CompanyId);
        }

        [Test]
        [TestCase("AAPL")]
        [TestCase("AAPL,GOOG")]
        public async Task GetCorporateCalendarTest(string symbols)
        {
            var result = await _client.MarketData.GetCorporateCalendars(symbols);

            var companyData = result.FirstOrDefault().Results.FirstOrDefault(x => x.Tables?.CorporateCalendars != null);
            Assert.IsNotNull(companyData);
            Assert.IsNotNull(companyData?.Tables?.CorporateCalendars?.FirstOrDefault()?.CompanyId);
        }

        [Test]
        public async Task GetEtbSecuritiesTest()
        {
            var result = await _client.MarketData.GetEtbSecurities();
            Assert.IsNotNull(result.Security.FirstOrDefault()?.Symbol);
        }

        [Test]
        [TestCase("Alphabet")]
        public async Task SearchCompaniesTest(string symbol)
        {
            var result = await _client.MarketData.SearchCompanies(symbol);
            Assert.IsNotNull(result.Security.FirstOrDefault()?.Symbol);
        }

        [Test]
        [TestCase("GOOG")]
        public async Task LookupSymbolTest(string query)
        {
            var result = await _client.MarketData.LookupSymbol(query);
            Assert.IsNotNull(result.Security.FirstOrDefault()?.Symbol);
        }
    }
}