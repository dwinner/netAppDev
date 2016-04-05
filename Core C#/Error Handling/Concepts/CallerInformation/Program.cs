/**
 * Информация о вызывающем компоненте
 */

using System;
using System.Runtime.CompilerServices;

namespace CallerInformation
{
   class Program
   {
      static void Main()
      {
         var p = new Program();
         Log();
         p.SomeProperty = 33;

         Action action = () => Log();
         action();

         Console.ReadLine();
      }

      private int _someProperty;

      public int SomeProperty
      {
         get
         {
            return _someProperty;
         }
         set
         {
            Log();
            _someProperty = value;
         }
      }

      private static void Log([CallerLineNumber] int line = -1, [CallerFilePath] string path = null, [CallerMemberName] string name = null)
      {
         Console.WriteLine((line < 0) ? "No line" : string.Format("Line {0}", line));
         Console.WriteLine(path ?? "No file path");
         Console.WriteLine(name ?? "No member name");
         Console.WriteLine();
      }
   }
}
