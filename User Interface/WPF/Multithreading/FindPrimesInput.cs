namespace Multithreading
{
   public sealed class FindPrimesInput
   {
      public FindPrimesInput(int from, int to)
      {
         To = to;
         From = from;
      }

      public int To { get; private set; }

      public int From { get; private set; }
   }
}