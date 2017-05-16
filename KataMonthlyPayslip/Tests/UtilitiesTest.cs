using KataMonthlyPaySlip.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace KataMonthlyPaySlip.Tests
{
  [TestClass]
  public class UtilitiesTest
  {
    private const string fileContent = "Test Content";

    private static string testFile = GetTestFileName();

    private static string GetTestFileName()
    {
      FileInfo fi = new FileInfo(DataFiles.PaySlipOutputFileName);
      
      return String.IsNullOrEmpty(testFile) ? Path.Combine(fi.DirectoryName, "TestFile.txt") : testFile;
    }

    [TestMethod]
    public void TestUtilitiesSaveFile()
    {
      bool saved = Utilities.SaveOutputToFile(testFile, fileContent);

      Assert.AreEqual(true, saved);

      Utilities.DeleteFile(testFile);
    }

    [TestMethod]
    public void TestUtilitiesGetFileContent()
    {
      if(!File.Exists(testFile))
        Utilities.SaveOutputToFile(testFile, fileContent);

      var readContent = Utilities.GetFileContent(testFile);

      Assert.AreEqual(fileContent, readContent.Replace(Display.NewLine,String.Empty));

      Utilities.DeleteFile(testFile);
    }

    [TestMethod]
    public void TestUtilitiesDeleteFile()
    {
      bool deleted = Utilities.DeleteFile(testFile);

      Assert.AreEqual(true, deleted && !File.Exists(testFile));
    }

    [TestMethod]
    public void TestUtilitiesFileNotFoundExceptionHandling()
    {
      try
      {
        Utilities.GetFileContent(testFile.Replace(".txt", ".tmp"));
      }
      catch (IOException ex)
      {
        Assert.AreEqual(ex.GetType(), typeof(IOException));
      }
    }

    [TestMethod]
    public void TestUtilitiesFileLoadExceptionHandling()
    {
      try
      {
        Utilities.SaveOutputToFile(testFile, String.Empty);

         Utilities.GetFileContent(testFile);
      }
      catch (IOException ex)
      {
        Assert.AreEqual(ex.GetType(), typeof(IOException));
      }

      Utilities.DeleteFile(testFile);
    }
  }
}
