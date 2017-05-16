namespace KataMonthlyPaySlip.Data
{
  public class TaxBracket
  {
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public decimal Tax { get; set; }
    public decimal AdditionalCharge { get; set; }
  }
}
