using System.Collections;

namespace KataMonthlyPaySlip.Interfaces
{
  public interface  IApplicationDataCollection: IEnumerable
  {
    void ValidateInputValues();
    IEnumerable GetTableEntries { get; }
  }
}
