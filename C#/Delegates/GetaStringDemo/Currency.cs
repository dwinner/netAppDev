namespace GetaStringDemo
{
   public struct Currency
   {
      public uint Dollars { get; set; }

      public ushort Cents { get; set; }

      public Currency(uint dollars, ushort cents)
         : this()
      {
         Dollars = dollars;
         Cents = cents;
      }

      public override string ToString()
      {
         return string.Format("${0}.{1,-2:00}", Dollars, Cents);
      }

      public static string GetCurrencyUnit()
      {
         return "Dollar";
      }

      public static explicit operator Currency(float value)
      {
         checked
         {
            var dollars = (uint)value;
            var cents = (ushort)((value - dollars) * 100);
            return new Currency(dollars, cents);
         }
      }

      public static implicit operator float(Currency value)
      {
         return value.Dollars + (value.Cents / 100.0f);
      }

      public static implicit operator Currency(uint value)
      {
         return new Currency(value, 0);
      }

      public static implicit operator uint(Currency value)
      {
         return value.Dollars;
      }
   }
}