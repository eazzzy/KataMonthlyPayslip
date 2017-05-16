using System;
namespace KataMonthlyPaySlip.Data
{
  public class PaySlipEntry
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal AnnualSalary { get; set; }
    public decimal SuperRate { get; set; }
    public DateTime PaymentStartDate { get; set; }
  }
}
