using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Core
{
   public class UniqueCustomerIdAttribute : ValidationAttribute
   {
      [Inject]
      public ICustomerValidator Validator { get; set; }

      public override bool IsValid(object value)
      {
         if (Validator == null)
         {
            throw new Exception("Validator is not specified.");
         }

         return !string.IsNullOrEmpty(value as string) && Validator.ValidateUniqueness((string) value);
      }
   }
}