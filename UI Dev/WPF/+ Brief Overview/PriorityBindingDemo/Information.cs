using System.Threading;

namespace PriorityBindingDemo
{
   public class Information
   {
      public string Info1 => "Please wait...";

      public string Info2
      {
         get
         {
            Thread.Sleep(5000);
            return "Please wait a little more";
         }
      }
   }
}