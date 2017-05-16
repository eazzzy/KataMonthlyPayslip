using KataMonthlyPaySlip.Data;
using KataMonthlyPaySlip.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace KataMonthlyPaySlip.Tests
{
  [TestClass]
  public class PaySlipTableTest
  {
    [TestMethod]
    public void TestPaySlipInvalidSalaryExceptionHandling()
    {
      var testData = @"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{
                      'FirstName':'David',
                      'LastName':'Rudd',
                      'AnnualSalary':'salary',
                      'SuperRate':'9%',
                      'PaymentStartDate':'01 March'}]}";

      try
      {
        DataObject.Load<PaySlipTableCollection>(testData);
      }
      catch (FormatException fe)
      {
        Assert.AreEqual(fe.GetType(), typeof(FormatException), fe.Message, fe.InnerException.Message);
      }
    }

    [TestMethod]
    public void TestPaySlipMissingFieldValueExceptionHandling()
    {
      var testData = @"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{
                      'FirstName':'David',
                      'LastName':'Rudd',
                      'AnnualSalary':'',
                      'SuperRate':'9%',
                      'PaymentStartDate':'01 March'}]}";

      try
      {
        DataObject.Load<PaySlipTableCollection>(testData);
      }
      catch (ArgumentNullException ex)
      {
        Assert.AreEqual(ex.GetType(), typeof(ArgumentNullException));
      }
    }

    [TestMethod]
    public void TestPaySlipInvalidSuperExceptionHandling()
    {
      var testData = @"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{
                      'FirstName':'David',
                      'LastName':'Rudd',
                      'AnnualSalary':'60050',
                      'SuperRate':'super',
                      'PaymentStartDate':'01 March'}]}";

      try
      {
        DataObject.Load<PaySlipTableCollection>(testData);
      }
      catch (FormatException fe)
      {
        Assert.AreEqual(fe.GetType(), typeof(FormatException), fe.Message, fe.InnerException.Message);
      }
    }

    [TestMethod]
    public void TestPaySlipInvalidInputDateFormatExceptionHandling()
    {
      var testData = @"{'MinAnnualSalary':'0','MinSuperRate':'0','MaxSuperRate':'50','PaySlipEntries': [{
                      'FirstName':'David',
                      'LastName':'Rudd',
                      'AnnualSalary':'60050',
                      'SuperRate':'9%',
                      'PaymentStartDate':'51 March'}]}";

      try
      {
        DataObject.Load<PaySlipTableCollection>(testData);
      }
      catch (InvalidDataException ex)
      {
        Assert.AreEqual(ex.GetType(), typeof(InvalidDataException));
      }
    }
  }
}
