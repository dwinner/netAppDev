using static System.Console;

namespace Strategy
{
   public class SecondStrategy : IStrategy
   {
      public void Algorithm()
         => WriteLine("Algorithm of the first strategy");
   }
}