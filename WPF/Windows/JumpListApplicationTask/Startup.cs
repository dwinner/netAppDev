using System;

namespace JumpListApplicationTask
{
   public static class Startup
   {
      [STAThread]
      public static void Main(string[] args)
      {
         var wrapper = new JumpListApplicationTaskWrapper();
         wrapper.Run(args);
      }
   }
}