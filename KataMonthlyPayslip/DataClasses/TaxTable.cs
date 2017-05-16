using KataMonthlyPaySlip.Helpers;
using KataMonthlyPaySlip.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace KataMonthlyPaySlip.Data
{
  [JsonObject]
  public partial class TaxTableCollection : IApplicationDataCollection, ITaxTable, IEnumerable<TaxBracket>
  {
    public DateTime TaxPeriodStartDate { get; set; }
    public DateTime TaxPeriodEndDate { get; set; }
    public IEnumerable<TaxBracket> TaxBrackets { get; set; }
    public IEnumerable GetTableEntries
    {
      get { return TaxBrackets; }
    }

    public void ValidateInputValues()
    {
      List<string> ValidationErrors = new List<string>();

      if (TaxPeriodStartDate.IsNullOrEmpty() || TaxPeriodEndDate.IsNullOrEmpty())
        ValidationErrors.Add(String.Format(CultureInfo.CurrentCulture, "StartDate and Enddate entries cannot be null in the Tax Table ({0})", String.Join(Display.CommaSpaceSeparator, TaxPeriodStartDate, TaxPeriodEndDate)));

      if (ValidationErrors.Any())
        throw new ValidationException(string.Format(CultureInfo.CurrentCulture, "Validation failed, {0} are outside the expected thresholds.", String.Join(Display.CommaSpaceSeparator, ValidationErrors)));
    }

    public IEnumerator<TaxBracket> GetEnumerator()
    {
      return TaxBrackets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
