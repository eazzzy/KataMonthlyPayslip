using System;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace KataMonthlyPaySlip.Helpers
{
  public static class Display
  {
    public const string LineEnd = "|";
    public const string CommaSeparator = ",";
    public const string CommaSpaceSeparator = ", ";
    public const char FormatTopChar = (char)42;
    public const char FormatSpaceChar = (char)32;
    public const int LineWidth = 76;
    public const string NewLine = "\r\n";
    public const string PositiveResponse = "y";

    private static string paySlipSeparator = String.Concat(CommaSeparator, NewLine);
    private static string paySlipLinePrefix = String.Concat("[", NewLine);
    private static string paySlipLineSuffix = String.Concat(NewLine, "]");
    private static string doubleNewLine = String.Concat(NewLine, NewLine);

    private static string generalExceptionMessage = String.Concat("An exception occurred in the application, PaySlip generation incomplete. ", DoubleNewLine, "Error Detail:", NewLine);
    private static string exitAndCloseMessage = "Press any key to exit...";
    private static string generationCanceledMessage = String.Concat(DoubleNewLine, "Process cancelled by user.", DoubleNewLine);
    private static string inProgressMessage = "Generating pay slips.........";
    private static string generationCompleteMessage = "PaySlip generation complete. ";
    private static string saveFailureMessage = "Error trying to save the file.";
    private static string serializeFailureMessage = String.Concat(DoubleNewLine, "Data files could not be loaded or deserializer successfully, Verify the validity of the Json input and try again.", DoubleNewLine);

    private static string displayName = ConfigurationManager.AppSettings["DisplayName"];

    public static string PaySlipSeparator { get { return paySlipSeparator; } }
    public static string PaySlipLinePrefix { get { return paySlipLinePrefix; } }
    public static string PaySlipLineSuffix { get { return paySlipLineSuffix; } }
    public static string DoubleNewLine { get { return doubleNewLine; } }
    public static string ExitAndCloseMessage { get { return exitAndCloseMessage; } }
    public static string GenerationCanceledMessage { get { return generationCanceledMessage; } }
    public static string InProgressMessage { get { return inProgressMessage; } }
    public static string GenerationCompleteMessage { get { return generationCompleteMessage; } }
    public static string SaveFailureMessage { get { return saveFailureMessage; } }
    public static string SerializeFailureMessage { get { return serializeFailureMessage; } }
    public static string Header { get { return GetDisplayHeader(); } }
    public static string Initialize { get { return GetInitializeDisplayMessage(); } }

    private static string GetDisplayHeader()
    {
      int DisplayNameWidth = String.IsNullOrEmpty(displayName) ? LineWidth : displayName.Length;
      int formatSpaces = (LineWidth - DisplayNameWidth) / 2;

      return String.Concat(String.Join(NewLine, String.Concat(LineEnd, String.Empty.PadRight(LineWidth, FormatTopChar), LineEnd),
        String.Concat(LineEnd, String.Empty.PadRight(LineWidth, FormatSpaceChar), LineEnd),
        String.Concat(LineEnd, String.Empty.PadRight(formatSpaces, FormatSpaceChar), displayName,
        String.Empty.PadRight(formatSpaces, FormatSpaceChar), LineEnd),
        String.Concat(LineEnd, String.Empty.PadRight(LineWidth, FormatSpaceChar), LineEnd),
        String.Concat(LineEnd, String.Empty.PadRight(LineWidth, FormatTopChar), LineEnd)), NewLine);
    }

    private static string GetInitializeDisplayMessage()
    {
        var initializeMsg = String.Join(DoubleNewLine, "The tax table and payslip input data files exists and can be found in the",
                String.Format(CultureInfo.CurrentCulture, "\"{0}\" directory.", Path.GetDirectoryName(DataFiles.TaxTableFileName)),
                "Note: All data generated will be displayed here and also saved to the same directory as the input files.",
                "Do you want to proceed with the data specified in the above mentioned files now?", "<--> ( y | n ) <-->");

      return String.Concat(GetDisplayHeader(), NewLine, initializeMsg, DoubleNewLine);
    }

    public static string ExceptionMessage(string message)
    {
      return String.Concat(generalExceptionMessage, message, DoubleNewLine);
    }
  }
}
