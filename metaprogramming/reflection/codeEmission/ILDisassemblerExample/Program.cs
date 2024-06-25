using System.Reflection;

namespace ILDisassemblerExample;

internal static class Program
{
   private static void Main()
   {
      var method = typeof(Program).GetMethod("Add", BindingFlags.Static | BindingFlags.NonPublic)!;
      Console.WriteLine(Disassembler.Disassemble(method));
   }

   private static double Add(double x, double y) => x + y;
}