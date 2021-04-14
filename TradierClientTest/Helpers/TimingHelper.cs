using System;

namespace TradierClientTest.Helpers
{
    public static class TimingHelper
    {
        public static DateTime GetLastWednesday()
        {
            var latestWeekday = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            while (latestWeekday.DayOfWeek != DayOfWeek.Wednesday)
            {
                latestWeekday -= TimeSpan.FromDays(1);
            }
            return latestWeekday;
        }

        public static DateTime GetLastThursday()
        {
            var latestWeekday = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            while (latestWeekday.DayOfWeek != DayOfWeek.Thursday)
            {
                latestWeekday -= TimeSpan.FromDays(1);
            }
            return latestWeekday;
        }
    }
}
