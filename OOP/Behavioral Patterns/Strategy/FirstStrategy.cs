using static System.Console;

namespace Strategy
{
   public class FirstStrategy : IStrategy
   {
      public void Algorithm()
         => WriteLine("Algorithm of the first strategy");
   }
}