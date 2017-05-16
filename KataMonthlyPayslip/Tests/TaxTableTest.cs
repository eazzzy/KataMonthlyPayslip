using KataMonthlyPaySlip.Data;
using KataMonthlyPaySlip.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KataMonthlyPaySlip.Tests
{
  [TestClass]
  public class TaxTableTest
  {
    [TestMethod]
    public void TestTaxTableInvalidInputDateFormatExceptionHandling()
    {
      var testData = @"{'TaxPeriodStartDate': '51 July 2012','TaxPeriodEndDate': '30 June 2013','TaxBrackets': [{
                      'Min': '0',
                      'Max': '18200',
                      'Tax': '0',
                      'AdditionalCharge': '0'}]}";

      try
      {
        DataObject.Load<TaxTableCollection>(testData);
      }
      catch (FormatException fe)
      {
        Assert.AreEqual(fe.GetType(), typeof(FormatException), fe.Message, fe.InnerException.Message);
      }
    }

    [TestMethod]
    public void TestTaxTableMissingMinValueExceptionHandling()
    {
      var testData = @"{'TaxPeriodStartDate': '01 July 2012','TaxPeriodEndDate': '30 June 2013','TaxBrackets': [{
                      'Min': '',
                      'Max': '18200',
                      'Tax': '0',
                      'AdditionalCharge': '0'}]}";

      try
      {
        DataObject.Load<TaxTableCollection>(testData);
      }
      catch (ArgumentNullException je)
      {
        Assert.AreEqual(je.GetType(), typeof(ArgumentNullException));
      }
    }

    [TestMethod]
    public void TestTaxTableInvalidMaxValueExceptionHandling()
    {
      var testData = @"{'TaxPeriodStartDate': '01 July 2012','TaxPeriodEndDate': '30 June 2013','TaxBrackets': [{
                      'Min': '0',
                      'Max': 'max',
                      'Tax': '0',
                      'AdditionalCharge': '0'}]}";

      try
      {
        DataObject.Load<TaxTableCollection>(testData);
      }
      catch (FormatException je)
      {
        Assert.AreEqual(je.GetType(), typeof(FormatException));
      }
    }

    [TestMethod]
    public void TestTaxTableInvalidTaxValueExceptionHandling()
    {
      var testData = @"{'TaxPeriodStartDate': '01 July 2012','TaxPeriodEndDate': '30 June 2013','TaxBrackets': [{
                      'Min': '0',
                      'Max': '18200',
                      'Tax': 'tax',
                      'AdditionalCharge': '0'}]}";

      try
      {
        DataObject.Load<TaxTableCollection>(testData);
      }
      catch (FormatException je)
      {
        Assert.AreEqual(je.GetType(), typeof(FormatException));
      }
    }

    [TestMethod]
    public void TestTaxTableInvalidAdditionalChargeValueExceptionHandling()
    {
      var testData = @"{'TaxPeriodStartDate': '01 July 2012','TaxPeriodEndDate': '30 June 2013','TaxBrackets': [{
                      'Min': '0',
                      'Max': '18200',
                      'Tax': '0',
                      'AdditionalCharge': 'charge'}]}";

      try
      {
        DataObject.Load<TaxTableCollection>(testData);
      }
      catch (FormatException je)
      {
        Assert.AreEqual(je.GetType(), typeof(FormatException));
      }
    }
  }
}
