using System;
using System.Reflection;

namespace SimpleILDisassemblerSample
{
   internal static class Program
   {
      private static void Main()
      {
         var methodInfo = typeof(Program).GetMethod(nameof(IsPrime), BindingFlags.NonPublic|BindingFlags.Static);
         var disassembledCode = IlDisassembler.Disassemble(methodInfo);
         Console.WriteLine(disassembledCode);
      }

      private static bool IsPrime(int number)
      {
         if (number % 2 == 0)
            return number == 2;

         var max = (int) Math.Sqrt(number);
         for (var i = 3; i <= max; i += 2)
            if (number % i == 0)
               return false;

         return true;
      }
   }
}