namespace _07_Closures
{
   public class Factor
   {
      private readonly int _factor;

      public Factor(int fact) => _factor = fact;

      public ProcStrategy Multiplier
      {
         get { return x => x * _factor; }
      }

      public ProcStrategy Adder
      {
         get { return x => x + _factor; }
      }
   }
}