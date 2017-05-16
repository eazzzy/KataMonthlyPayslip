using System;
using System.Globalization;

namespace KataMonthlyPaySlip
{
  public static class DateTimeExtension
  {
    public static bool IsNullOrEmpty(this DateTime checkDate)
    {
      return checkDate == default(DateTime);
    }

    public static DateTime ToWorkingTaxYear(this DateTime startDate, DateTime taxPeriodStartDate, DateTime taxPeriodEndDate)
    {
      int assumedYearDifferencce = startDate.Month >= taxPeriodStartDate.Month
        ? taxPeriodStartDate.Year - startDate.Year
        : taxPeriodEndDate.Year - startDate.Year;

      return startDate.AddYears(assumedYearDifferencce);
    }

    public static string ToPeriodString(this DateTime inputDate)
    {
      return String.Format(CultureInfo.CurrentCulture, "01 {0} - {1} {0}", inputDate.ToString("MMMM",CultureInfo.CurrentCulture), DateTime.DaysInMonth(inputDate.Year, inputDate.Month));
    }
  }
}
