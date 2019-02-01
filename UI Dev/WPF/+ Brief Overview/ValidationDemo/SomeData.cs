using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ValidationDemo
{
   public class SomeData : IDataErrorInfo
   {
      private int _value1;

      public int Value1
      {
         get { return _value1; }
         set
         {
            if (value < 5 || value > 12)
               throw new ArgumentException("Value must be less than 5 or greater than 12");

            _value1 = value;
         }
      }

      public int Value2 { get; set; }

      [Range(0, 33)]
      public int Value3 { get; set; }

      string IDataErrorInfo.this[string columnName]
         =>
            columnName == nameof(Value2) && (Value2 < 0 || Value2 > 80)
               ? "Age must not be less than 0 or greater than 80"
               : null;

      string IDataErrorInfo.Error => null;
   }
}