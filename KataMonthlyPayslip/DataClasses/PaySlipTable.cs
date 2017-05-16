using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;
using KataMonthlyPaySlip.Interfaces;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using KataMonthlyPaySlip.Helpers;

namespace KataMonthlyPaySlip.Data
{
  [JsonObject]
  public partial class PaySlipTableCollection : IApplicationDataCollection, IEnumerable<PaySlipEntry>, IPaySlipTable
  {
    public PaySlipTableCollection() { }

    public decimal MinAnnualSalary { get; set; }
    public decimal MinSuperRate { get; set; }
    public decimal MaxSuperRate { get; set; }
    public IEnumerable<PaySlipEntry> PaySlipEntries { get; set; }
    public IEnumerable GetTableEntries
    {
      get{ return PaySlipEntries; }
    }

    public void ValidateInputValues()
    {
      List<string> ValidationErrors = new List<string>();

      if (PaySlipEntries.Any(ps => ps.AnnualSalary < MinAnnualSalary))
        ValidationErrors.Add(String.Format(CultureInfo.CurrentCulture, "Annual Salaries ({0})", String.Join(Display.CommaSpaceSeparator, PaySlipEntries.Where(ps => ps.AnnualSalary < MinAnnualSalary).Select(s => s.AnnualSalary.ToString(CultureInfo.CurrentCulture)).ToList())));

      if (PaySlipEntries.Any(ps => (ps.SuperRate <= MinSuperRate || ps.SuperRate >= MaxSuperRate)))
        ValidationErrors.Add(String.Format(CultureInfo.CurrentCulture, "Super Rates ({0})", String.Join(Display.CommaSpaceSeparator, PaySlipEntries.Where(ps => (ps.SuperRate <= MinSuperRate || ps.SuperRate >= MaxSuperRate)).Select(s => s.SuperRate.ToString(CultureInfo.CurrentCulture)).ToList())));

      if (ValidationErrors.Any())
        throw new ValidationException(String.Format(CultureInfo.CurrentCulture, "Validation failed, {0} are outside the expected thresholds.", String.Join(Display.CommaSpaceSeparator, ValidationErrors)));
    }

    public IEnumerator<PaySlipEntry>  GetEnumerator()
    {
      return PaySlipEntries.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
