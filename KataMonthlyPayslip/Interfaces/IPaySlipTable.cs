using KataMonthlyPaySlip.Data;
using System.Collections.Generic;

namespace KataMonthlyPaySlip.Interfaces
{
  public interface IPaySlipTable
  {
    decimal MinAnnualSalary { get; set; }
    decimal MinSuperRate { get; set; }
    decimal MaxSuperRate { get; set; }
    IEnumerable<PaySlipEntry> PaySlipEntries { get; set; }
  }
}
