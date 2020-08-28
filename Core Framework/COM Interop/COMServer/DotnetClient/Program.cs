using System;
using System.Runtime.InteropServices;
using Wrox.ProCSharp.Interop.Server;

namespace DotnetClient
{
   internal static class Program
   {
      [STAThread]
      private static void Main()
      {
         var obj = new COMDemo();
         IWelcome welcome = obj;
         Console.WriteLine(welcome.Greeting("Stephanie"));

         obj.Completed += () => Console.WriteLine("Calculation completed");

         var math = (IMath) welcome;
         var x = math.Add(4, 5);
         Console.WriteLine(x);

         Marshal.ReleaseComObject(math);
      }
   }
}