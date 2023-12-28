namespace PointerPlayground2
{
   internal readonly struct CurrencyStruct
   {
      public CurrencyStruct(long dollars, byte cents) =>
         (Dollars, Cents) = (dollars, cents);

      public readonly long Dollars;

      public readonly byte Cents;

      public override string ToString() => $"$ {Dollars}.{Cents}";
   }

   internal class CurrencyClass
   {
      public readonly byte Cents;
      public readonly long Dollars;

      public CurrencyClass(long dollars, byte cents) =>
         (Dollars, Cents) = (dollars, cents);

      public override string ToString() => $"$ {Dollars}.{Cents}";
   }
}