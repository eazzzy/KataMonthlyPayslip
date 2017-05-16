using System;
using System.Linq;
using Newtonsoft.Json;
using KataMonthlyPaySlip.Interfaces;
using System.Globalization;
using KataMonthlyPaySlip.Helpers;
using KataMonthlyPaySlip.Data;

namespace KataMonthlyPaySlip.Implementation
{
  public class MonthlyPay
  {
    public ITaxTable CurrentTaxTable { get; set; }
    private const int MonthsInYear = 12;

    public MonthlyPay() { }

    public MonthlyPay(ITaxTable taxTable)
    {
      CurrentTaxTable = taxTable;
    }

    public string GenerateEmployeePaySlip(PaySlipEntry paySlipEntry)
    {
      string returnMessage = String.Empty;

      if (paySlipEntry != null)
      {
        var grossIncome = GetMonthlyGrossIncomeFromAnnualSalary(paySlipEntry.AnnualSalary);
        var incomeTax = GetMonthlyIncomeTaxFromAnnualSalary(paySlipEntry.AnnualSalary);

        var payslip = new GeneratedPaySlip()
        {
          Name = String.Join(Display.CommaSpaceSeparator, paySlipEntry.FirstName, paySlipEntry.LastName),
          PayPeriod = paySlipEntry
                        .PaymentStartDate
                        .ToWorkingTaxYear(CurrentTaxTable.TaxPeriodStartDate, CurrentTaxTable.TaxPeriodEndDate)
                        .ToPeriodString(),
          GrossIncome = grossIncome.ToString(CultureInfo.CurrentCulture),
          IncomeTax = incomeTax.ToString(CultureInfo.CurrentCulture),
          NetIncome = (grossIncome - incomeTax).ToString(CultureInfo.CurrentCulture),
          Super = GetSuperFromGrossIncome(grossIncome, paySlipEntry.SuperRate).ToString(CultureInfo.CurrentCulture)
        };

        returnMessage = JsonConvert.SerializeObject(payslip);
      }

      return returnMessage;
    }

    private static decimal GetMonthlyGrossIncomeFromAnnualSalary(decimal annualSalary)
    {
      return (annualSalary / MonthsInYear).RoundToWholeNumber();
    }

    private decimal GetMonthlyIncomeTaxFromAnnualSalary(decimal anualIncome)
    {
      var bracket = CurrentTaxTable.TaxBrackets.Where(i => i.Min <= anualIncome).LastOrDefault();
      var incomeTax = ((bracket.Tax + (anualIncome - bracket.Min) * bracket.AdditionalCharge) / MonthsInYear).RoundToWholeNumber();

      return incomeTax;
    }

    private static decimal GetSuperFromGrossIncome(decimal grossIncome, decimal super)
    {
      decimal superPercentage = 1 + (super / 100);

      return ((grossIncome * superPercentage) - grossIncome).RoundToWholeNumber();
    }
  }
}
