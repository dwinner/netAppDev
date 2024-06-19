using static System.Console;

namespace CodeBuilder;

internal static class Program
{
   private static void Main()
   {
      var codeBuilder = new CodeBuilder();
      codeBuilder.AppendLine("class Foo")
         .AppendLine("{")
         .AppendLine("}");
      WriteLine(codeBuilder);
   }
}