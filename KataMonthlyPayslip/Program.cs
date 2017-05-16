using KataMonthlyPaySlip.Data;
using KataMonthlyPaySlip.Helpers;
using KataMonthlyPaySlip.Implementation;
using KataMonthlyPaySlip.Interfaces;
using System;
using System.Globalization;

[assembly: CLSCompliant(true)]
namespace KataMonthlyPaySlip
{
  class Program
  {
    static void Main()
    {
      var promptInput = String.Empty;
      var feedbackMessage = String.Empty;

      var consoleHeader = Display.Header;

      try
      {
        IApplicationDataCollection taxTable = DataObject.Load<TaxTableCollection>(Utilities.GetFileContent(DataFiles.TaxTableFileName));
        IApplicationDataCollection paySlipTable = DataObject.Load<PaySlipTableCollection>(Utilities.GetFileContent(DataFiles.PaySlipDataFileName));

        var monthPay = new MonthlyPay((ITaxTable)taxTable);

        var dataInputMessage = Display.Initialize;

        Console.WriteLine(dataInputMessage);
        promptInput = Console.In.ReadLine();

        if (promptInput.Equals(Display.PositiveResponse, StringComparison.OrdinalIgnoreCase))
        {
          var paySlipOutput = String.Empty;
          bool fileSaved = false;

          Console.Clear();
          Console.WriteLine(consoleHeader);

          foreach (PaySlipEntry input in paySlipTable.GetTableEntries)
          {
            paySlipOutput += String.Concat(monthPay.GenerateEmployeePaySlip(input), Display.PaySlipSeparator);
          }

          paySlipOutput = String.Concat(Display.PaySlipLinePrefix, paySlipOutput.Substring(0, paySlipOutput.LastIndexOf(Display.PaySlipSeparator, StringComparison.OrdinalIgnoreCase)), Display.PaySlipLineSuffix);
          Console.WriteLine(paySlipOutput);
          fileSaved = Utilities.SaveOutputToFile(DataFiles.PaySlipOutputFileName, paySlipOutput);
          feedbackMessage = String.Concat(Display.DoubleNewLine, fileSaved
            ? Display.GenerationCompleteMessage
            : String.Join(Display.CommaSeparator, Display.GenerationCompleteMessage, Display.SaveFailureMessage), Display.DoubleNewLine);
        }
        else
        {
          feedbackMessage = Display.GenerationCanceledMessage;
        }
      }
      catch (Exception ex)
      {
        Console.Clear();
        Console.WriteLine(consoleHeader);
        feedbackMessage = Display.ExceptionMessage(ex.Message);
      }
      finally
      {
        Console.WriteLine(String.Format(CultureInfo.CurrentCulture, "{0}{1}", feedbackMessage, Display.ExitAndCloseMessage));
        Console.ReadKey();
      }

      return;
    }
  }
}
