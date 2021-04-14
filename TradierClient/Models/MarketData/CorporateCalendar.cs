using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.MarketData
{
    public class CorporateCalendarRootObject
    {
        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("results")]
        public List<CorporateCalendarData> Results { get; set; }
    }

    public class CorporateCalendarData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tables")]
        public CorporateCalendarTable Tables { get; set; }
    }

    public class CorporateCalendarTable
    {
        [JsonProperty("corporate_calendars")]
        public List<CorporateCalendar> CorporateCalendars { get; set; }
    }

    public class CorporateCalendar
    {
        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("begin_date_time")]
        [JsonConverter(typeof(YYYYMMDDConverter))]
        public DateTime BeginDateTime { get; set; }

        [JsonProperty("end_date_time")]
        [JsonConverter(typeof(YYYYMMDDConverter))]
        public DateTime EndDateTime { get; set; }

        [JsonProperty("event_type")]
        public int EventType { get; set; }

        [JsonProperty("estimated_date_for_next_event")]
        [JsonConverter(typeof(YYYYMMDDConverter))]
        public DateTime EstimatedDateForNextEvent { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("event_fiscal_year")]
        public int EventFiscalYear { get; set; }

        [JsonProperty("event_status")]
        public string EventStatus { get; set; }

        [JsonProperty("time_zone")]
        [JsonConverter(typeof(YYYYMMDDConverter))]
        public DateTime TimeZone { get; set; }
    }
}
