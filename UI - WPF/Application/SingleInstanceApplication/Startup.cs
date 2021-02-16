using System;

namespace SingleInstanceApplication
{
   public static class Startup
   {
      [STAThread]
      public static void Main(string[] args)
      {
         var wrapper = new SingleInstanceApplicationWrapper();
         wrapper.Run(args);
      }
   }
}