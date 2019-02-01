using System.Threading;

namespace PriorityBindingDemo
{
   public class Data
   {
      public string ProcessSomeData
      {
         get
         {
            Thread.Sleep(8000);
            return "The final result is here";
         }
      }
   }
}