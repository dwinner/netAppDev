using System.Threading;

namespace MathServiceLibrary
{   
   public class BasicMath : IBasicMath
   {
      public int Add(int x, int y)
      {
         Thread.Sleep(5000);  // Эмулировать длительный процесс
         return x + y;
      }
   }
}
