using System.Threading;

public class NumberService : INumberService
{
   private int _number;
   public int GetNumber() => Interlocked.Increment(ref _number);
}