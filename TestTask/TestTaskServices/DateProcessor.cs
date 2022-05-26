using System;
using System.Globalization;

namespace TestTaskServices
{
    public class DateProcessor
    {
        public virtual (DateTime, DateTime) GetWeekScopeByWeekNumber(int weekOfYear)
        {
            DateTime jan1 = new(DateTime.Now.Year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            Calendar cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstMonday, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            DateTime result = firstMonday.AddDays(weekNum * 7);

            return (result, result.AddDays(7));
        }
    }
}
