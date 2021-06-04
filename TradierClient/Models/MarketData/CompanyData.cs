using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.MarketData
{
    public class CompanyDataRootObject
    {
        public CompanyDataRootObject()
        {
            Results = new List<CompanyData>();
        }

        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("results")]
        public IEnumerable<CompanyData> Results { get; set; }
    }

    public class CompanyData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tables")]
        public CompanyDataTable Tables { get; set; }
    }

    public class CompanyDataTable
    {
        [JsonProperty("company_profile")]
        public CompanyProfile CompanyProfile { get; set; }

        [JsonProperty("asset_classification")]
        public AssetClassification AssetClassification { get; set; }

        [JsonProperty("historical_asset_classification")]
        public HistoricalAssetClassification HistoricalAssetClassification { get; set; }

        [JsonProperty("long_descriptions")]
        public string LongDescriptions { get; set; }
    }

    public class CompanyProfile
    {
        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("average_employee_number")]
        public int AverageEmployeeNumber { get; set; }

        [JsonProperty("contact_email")]
        public string ComtactEmail { get; set; }

        [JsonProperty("is_head_office_same_with_registered_office_flag")]
        public bool IsHeadOfficeSameWithRegisteredOfficeFlag { get; set; }

        [JsonProperty("total_employee_number")]
        public int TotalEmployeeNumber { get; set; }

        [JsonProperty("TotalEmployeeNumber.asOfDate")]
        public DateTime TotalEmployeeNumberAsOfDate { get; set; }

        [JsonProperty("headquarter")]
        public CompanyHeadquarter Headquarter { get; set; }
    }

    public class CompanyHeadquarter
    {
        [JsonProperty("address_line1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }
    }

    public abstract class AssetClassificationBase
    {
        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("financial_health_grade")]
        public string FinancialHealthGrade { get; set; }

        [JsonProperty("morningstar_economy_sphere_code")]
        public int MorningstarEconomySphereCode { get; set; }

        [JsonProperty("morningstar_industry_code")]
        public int MorningstarIndustryCode { get; set; }

        [JsonProperty("morningstar_industry_group_code")]
        public int MorningstarIndustryGroupCode { get; set; }

        [JsonProperty("morningstar_sector_code")]
        public int MorningstarSectorCode { get; set; }

        [JsonProperty("profitability_grade")]
        public string ProfitabilityGrade { get; set; }

        [JsonProperty("size_score")]
        public float SizeScore { get; set; }

        [JsonProperty("stock_type")]
        public int StockType { get; set; }

        [JsonProperty("style_box")]
        public int StyleBox { get; set; }

        [JsonProperty("style_score")]
        public float StyleScore { get; set; }

        [JsonProperty("value_score")]
        public float ValueScore { get; set; }
    }

    public class AssetClassification : AssetClassificationBase
    {
        [JsonProperty("c_a_n_n_a_i_c_s")]
        public int Cannaics { get; set; }

        [JsonProperty("FinancialHealthGrade.asOfDate")]
        public DateTime FinancialHealthGradeAsOfDate { get; set; }

        [JsonProperty("growth_grade")]
        public string GrowthGrade { get; set; }

        [JsonProperty("GrowthGrade.asOfDate")]
        public DateTime GrowthGradeAsOfDate { get; set; }

        [JsonProperty("n_a_c_e")]
        public float Nace { get; set; }

        [JsonProperty("n_a_i_c_s")]
        public int Naics { get; set; }

        [JsonProperty("ProfitabilityGrade.asOfDate")]
        public DateTime ProfitabilityGradeAsOfDate { get; set; }

        [JsonProperty("s_i_c")]
        public int Sic { get; set; }

        [JsonProperty("StockType.asOfDate")]
        public DateTime StockTypeAsOfDate { get; set; }

        [JsonProperty("StyleBox.asOfDate")]
        public DateTime StyleBoxAsOfDate { get; set; }
    }

    public class HistoricalAssetClassification : AssetClassificationBase
    {
        [JsonProperty("as_of_date")]
        public DateTime AsOfDate { get; set; }
    }
}
