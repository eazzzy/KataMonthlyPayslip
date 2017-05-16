using System;
using System.Configuration;
using System.IO;

namespace KataMonthlyPaySlip.Helpers
{
  public static class DataFiles
  {
    private static string DataFilePath = Path.Combine(String.Join(@"\", ConfigurationManager.AppSettings["DataFilesPath"]));

    private static string paySlipOutputFileName = Path.Combine(DataFilePath, ConfigurationManager.AppSettings["SavePaySlipOutputFileName"]);
    private static string paySlipDataFileName = Path.Combine(DataFilePath, ConfigurationManager.AppSettings["PaySlipDataFileName"]);
    private static string taxTableFileName = Path.Combine(DataFilePath, ConfigurationManager.AppSettings["TaxTableDataFileName"].ToString());

    public static string PaySlipOutputFileName { get { return paySlipOutputFileName; } }
    public static string PaySlipDataFileName { get { return paySlipDataFileName; } }
    public static string TaxTableFileName { get { return taxTableFileName; } }
  }
}
