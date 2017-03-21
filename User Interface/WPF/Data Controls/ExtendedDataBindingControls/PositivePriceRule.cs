using System;
using System.Globalization;
using System.Windows.Controls;

namespace DataBinding
{
   public class PositivePriceRule : ValidationRule
   {
      public decimal Min { get; set; } = 0;

      public decimal Max { get; set; } = decimal.MaxValue;

      public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
         decimal price = 0;

         try
         {
            var strValue = value as string;
            if (!string.IsNullOrEmpty(strValue))
            {
               price = decimal.Parse(strValue, NumberStyles.Any);
            }
         }
         catch (Exception)
         {
            return new ValidationResult(false, "Illegal characters.");
         }

         return price < Min || price > Max
            ? new ValidationResult(false, $"Not in the range {Min} to {Max}.")
            : new ValidationResult(true, null);
      }
   }
}