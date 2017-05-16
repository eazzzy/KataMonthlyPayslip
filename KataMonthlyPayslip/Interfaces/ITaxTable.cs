using KataMonthlyPaySlip.Data;
using System;
using System.Collections.Generic;

namespace KataMonthlyPaySlip.Interfaces
{
  public interface ITaxTable
  {
    DateTime TaxPeriodStartDate { get; set; }
    DateTime TaxPeriodEndDate { get; set; }
    IEnumerable<TaxBracket> TaxBrackets { get; set; }
  }
}
