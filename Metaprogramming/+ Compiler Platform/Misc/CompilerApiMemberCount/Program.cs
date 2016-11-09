using System.Linq;
using Microsoft.CodeAnalysis;
using static System.Console;

namespace CompilerApiMemberCount
{
   internal static class Program
   {
      private static void Main()
      {
         var assembly = typeof (CSharpExtensions).Assembly;
         WriteLine($"Assembly {assembly.FullName}");
         var types = assembly.GetExportedTypes();
         WriteLine($"Type count {types.Length}");
         var methodCount = types.Sum(type => type.GetMethods().Length);
         WriteLine($"Method count {methodCount}");
      }
   }
}