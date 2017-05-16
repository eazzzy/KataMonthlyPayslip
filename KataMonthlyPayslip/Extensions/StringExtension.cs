using System;
using System.Globalization;
using System.Linq;

namespace KataMonthlyPaySlip
{
  public static class StringExtensions
  {
    public static decimal ToDecimal(this string value)
    {
      return decimal.Parse(value,CultureInfo.CurrentCulture);
    }

    public static string RemoveWhiteSpace(this string value)
    {
      return new string(value.Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }

    public static string CleanPaySlipInputString(this string value)
    {
      return !String.IsNullOrEmpty(value) ? value.Replace("%", String.Empty) : String.Empty;
    }
  }
}
