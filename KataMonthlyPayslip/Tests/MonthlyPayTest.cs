using KataMonthlyPaySlip.Data;
using KataMonthlyPaySlip.Implementation;
using KataMonthlyPaySlip.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace KataMonthlyPaySlip.Tests
{
  [TestClass]
  public class MonthlyPayTest
  {
    public TestContext TestContext { get; set; }
    public MonthlyPay MonthPayClass { get; set; }

    const string taxData = @"{'TaxPeriodStartDate': '01 July 2012','TaxPeriodEndDate': '30 June 2013','TaxBrackets': [
        {'Min': '0','Max': '18200','Tax': '0','AdditionalCharge': '0'},
	      {'Min': '18201','Max': '37000','Tax': '0','AdditionalCharge': '0.19'},
	      {'Min': '37001','Max': '80000','Tax': '3572','AdditionalCharge': '0.325'},
	      {'Min': '80001','Max': '180000','Tax': '17547','AdditionalCharge': '0.37'},
	      {'Min': '180001','Max': '17548','Tax': '54547','AdditionalCharge': '0.45'}]}";

    string[] paySlipEntries = new string[] { @"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{'FirstName':'David','LastName':'Rudd','AnnualSalary':'60050','SuperRate':'9% ','PaymentStartDate':'01 March'}]}"
      ,@"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{'FirstName':'Ryan','LastName':'Chen','AnnualSalary':'120000','SuperRate':'10% ','PaymentStartDate':'01 March'}]}"
      ,@"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{'FirstName':'Nico','LastName':'Swanepoel','AnnualSalary':'85000','SuperRate':'21% ','PaymentStartDate':'01 September'}]}"
      ,@"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{'FirstName':'Lizzy','LastName':'Fitzgerald','AnnualSalary':'17995','SuperRate':'5% ','PaymentStartDate':'05 April'}]}"};


    [TestMethod]
    public void TestGeneratePaySlip1()
    {
      MonthPayClass = new MonthlyPay(DataObject.Load<TaxTableCollection>(taxData));

      var paySlipOutput = String.Empty;
      var paySlipEntry = paySlipEntries[0];

      IApplicationDataCollection paySlipTable = DataObject.Load<PaySlipTableCollection>(paySlipEntry);

      foreach (PaySlipEntry input in paySlipTable.GetTableEntries)
      {
        paySlipOutput += MonthPayClass.GenerateEmployeePaySlip(input);
      }

      var paySlipResult = JsonConvert.DeserializeObject<GeneratedPaySlip>(paySlipOutput);

      Assert.AreEqual("01 March - 31 March", paySlipResult.PayPeriod);
      Assert.AreEqual("5004", paySlipResult.GrossIncome);
      Assert.AreEqual("922", paySlipResult.IncomeTax);
      Assert.AreEqual("4082", paySlipResult.NetIncome);
      Assert.AreEqual("450", paySlipResult.Super);

      Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Test input -> {0}\r\n\r\nTest output -> {1}", paySlipEntry, paySlipOutput));
    }

    [TestMethod]
    public void TestGeneratePaySlip2()
    {
      MonthPayClass = new MonthlyPay(DataObject.Load<TaxTableCollection>(taxData));

      var paySlipOutput = String.Empty;
      var paySlipEntry = paySlipEntries[1];

      IApplicationDataCollection paySlipTable = DataObject.Load<PaySlipTableCollection>(paySlipEntry);

      foreach (PaySlipEntry input in paySlipTable.GetTableEntries)
      {
        paySlipOutput += MonthPayClass.GenerateEmployeePaySlip(input);
      }

      var paySlipResult = JsonConvert.DeserializeObject<GeneratedPaySlip>(paySlipOutput);

      Assert.AreEqual("01 March - 31 March", paySlipResult.PayPeriod);
      Assert.AreEqual("10000", paySlipResult.GrossIncome);
      Assert.AreEqual("2696", paySlipResult.IncomeTax);
      Assert.AreEqual("7304", paySlipResult.NetIncome);
      Assert.AreEqual("1000", paySlipResult.Super);

      Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Test input -> {0}\r\n\r\nTest output -> {1}", paySlipEntry, paySlipOutput));
    }

    [TestMethod]
    public void TestGeneratePaySlip3()
    {
      MonthPayClass = new MonthlyPay(DataObject.Load<TaxTableCollection>(taxData));

      var paySlipOutput = String.Empty;
      var paySlipEntry = paySlipEntries[2];

      IApplicationDataCollection paySlipTable = DataObject.Load<PaySlipTableCollection>(paySlipEntry);

      foreach (PaySlipEntry input in paySlipTable.GetTableEntries)
      {
        paySlipOutput += MonthPayClass.GenerateEmployeePaySlip(input);
      }

      var paySlipResult = JsonConvert.DeserializeObject<GeneratedPaySlip>(paySlipOutput);

      Assert.AreEqual("01 September - 30 September", paySlipResult.PayPeriod);
      Assert.AreEqual("7083", paySlipResult.GrossIncome);
      Assert.AreEqual("1616", paySlipResult.IncomeTax);
      Assert.AreEqual("5467", paySlipResult.NetIncome);
      Assert.AreEqual("1487", paySlipResult.Super);

      Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Test input -> {0}\r\n\r\nTest output -> {1}", paySlipEntry, paySlipOutput));
    }

    [TestMethod]
    public void TestGeneratePaySlip4()
    {
      MonthPayClass = new MonthlyPay(DataObject.Load<TaxTableCollection>(taxData));

      var paySlipOutput = String.Empty;
      var paySlipEntry = paySlipEntries[3];

      IApplicationDataCollection paySlipTable = DataObject.Load<PaySlipTableCollection>(paySlipEntry);

      foreach (PaySlipEntry input in paySlipTable.GetTableEntries)
      {
        paySlipOutput += MonthPayClass.GenerateEmployeePaySlip(input);
      }

      var paySlipResult = JsonConvert.DeserializeObject<GeneratedPaySlip>(paySlipOutput);

      Assert.AreEqual("01 April - 30 April", paySlipResult.PayPeriod);
      Assert.AreEqual("1500", paySlipResult.GrossIncome);
      Assert.AreEqual("0", paySlipResult.IncomeTax);
      Assert.AreEqual("1500", paySlipResult.NetIncome);
      Assert.AreEqual("75", paySlipResult.Super);

      Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Test input -> {0}\r\n\r\nTest output -> {1}", paySlipEntry, paySlipOutput));
    }

  }
}
