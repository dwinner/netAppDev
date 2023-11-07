using Injectors.Core;
using System.IO;

namespace Injectors
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         if (args != null && args.Length == 1)
         {
            InjectorRunner.Run(new FileInfo(args[0]));
         }
      }
   }
}