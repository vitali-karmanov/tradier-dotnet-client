using System;

namespace TradierClient.Test.Helpers
{
    public static class TimingHelper
    {
        public static DateTime GetLastWednesday()
        {
            var lastWednesday = DateTime.Now.AddDays(-2);
            while (lastWednesday.DayOfWeek != DayOfWeek.Wednesday)
            {
                lastWednesday = lastWednesday.AddDays(-1);
            }
            return lastWednesday;
        }

        public static DateTime GetLastThursday()
        {
            var lastThursday = DateTime.Now.AddDays(-1);
            while (lastThursday.DayOfWeek != DayOfWeek.Thursday)
            {
                lastThursday = lastThursday.AddDays(-1);
            }
            return lastThursday;
        }
    }
}
