﻿using System;

namespace KataMonthlyPaySlip
{
  public static class DecimalExtension
  {
    public static decimal RoundToWholeNumber(this decimal number)
    {
      return Math.Round(number, 0);
    }
  }
}
