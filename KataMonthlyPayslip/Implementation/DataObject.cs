using KataMonthlyPaySlip.Interfaces;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace KataMonthlyPaySlip.Implementation
{
  public static class DataObject
  {
    public static T Load<T>(string jsonInput)
    {
      try
      {
        var loadedObject = JsonConvert.DeserializeObject<T>(jsonInput.CleanPaySlipInputString());

        (loadedObject as IApplicationDataCollection).ValidateInputValues();

        return loadedObject;
      }
      catch (JsonReaderException jre)
      {
        throw new FormatException(String.Format(CultureInfo.CurrentCulture, "Invalid format ({0}). Additional Information: {1}.", typeof(T).Name, jre.Message), jre);
      }
      catch (JsonSerializationException jsex)
      {
        throw new ArgumentNullException(String.Format(CultureInfo.CurrentCulture, "Missing value ({0}), All input fields are mandatory. Additional Information: {1}.", typeof(T).Name, jsex.Message), jsex);
      }
    }
  }
}
