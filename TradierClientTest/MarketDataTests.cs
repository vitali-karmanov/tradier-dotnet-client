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