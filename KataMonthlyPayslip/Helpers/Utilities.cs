using System;
using System.Globalization;
using System.IO;

namespace KataMonthlyPaySlip.Helpers
{
  public static class Utilities
  {
    private static string workingFile = String.Empty;

    public static string GetFileContent(string dataFileName)
    {
      workingFile = dataFileName;

      if (File.Exists(workingFile))
      {
        var jsonContent = String.Empty;

        using (StreamReader sr = new StreamReader(workingFile))
        {
           jsonContent = sr.ReadToEnd();
        }

        if (!String.IsNullOrEmpty(jsonContent.RemoveWhiteSpace()))
        {
           return jsonContent;
        }
        else
        {
           throw ReaderException(new FileLoadException("The content of the file could not be read or is empty."));
        }
      }
      else
      {
        throw ReaderException(new FileNotFoundException("The file cannot not be found."));
      }
    }

    public static bool SaveOutputToFile(string outputFileFullName, string fileContent)
    {
      workingFile = outputFileFullName;

      try
      {
        using (StreamWriter writetext = new StreamWriter(workingFile))
        {
          writetext.WriteLine(fileContent);
        }
      }
      catch (AccessViolationException accex)
      {
        throw SaveException(accex);
      }
      catch (IOException ioex)
      {
        throw SaveException(ioex);
      }
      catch (NotSupportedException nsupex)
      {
        throw SaveException(nsupex);
      }

      return File.Exists(workingFile);
    }

    public static bool DeleteFile(string fileFullName)
    {
      workingFile = fileFullName;

      try
      {
        File.Delete(workingFile);
      }
      catch (FileNotFoundException fnfex)
      {
        throw DeleteException(fnfex);
      }
      catch (IOException ioex)
      {
        throw DeleteException(ioex);
      }
      catch (NotSupportedException nsupex)
      {
        throw DeleteException(nsupex);
      }
      catch (UnauthorizedAccessException authex)
      {
        throw DeleteException(authex);
      }

      return !File.Exists(workingFile);
    }

    private static IOException ReaderException(Exception ex)
    {
      return new IOException(String.Format(CultureInfo.CurrentCulture, String.Join(Display.DoubleNewLine,"An error occurred reading file \"{0}\".","Error Detail: {1}"), workingFile, ex.Message), ex);
    }

    private static IOException SaveException(Exception ex)
    {
      return new IOException(String.Format(CultureInfo.CurrentCulture, "File {0} could not be saved. Error Detail: {1}", workingFile, ex.Message), ex);
    }

    private static IOException DeleteException(Exception ex)
    {
      return new IOException(String.Format(CultureInfo.CurrentCulture, "File {0} could not be deleted. Error Detail: {1}", workingFile, ex.Message), ex);
    }
  }
}
