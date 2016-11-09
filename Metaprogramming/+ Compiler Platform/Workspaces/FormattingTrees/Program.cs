using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using static System.Console;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.Formatting.Formatter;

namespace FormattingTrees
{
   internal static class Program
   {
      private static void Main()
      {
         FormatClassNode();
      }

      private static void FormatClassNode()
      {
         WriteLine(nameof(FormatClassNode));
         var code = ClassDeclaration("NewClass");
         WriteLine(code);
         WriteLine(code.NormalizeWhitespace());
         WriteLine(Format(code, new AdhocWorkspace()));
         WriteLine(Format(code, MSBuildWorkspace.Create()));
         WriteLine(code.WithAdditionalAnnotations(Annotation));
      }
   }
}