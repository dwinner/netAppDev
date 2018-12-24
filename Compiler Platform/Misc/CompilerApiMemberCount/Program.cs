using System.Linq;
using static System.Console;
using CSharpExtensions = Microsoft.CodeAnalysis.CSharp.CSharpExtensions;

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