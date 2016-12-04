// Анализ потока управления

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ControlFlowAnalysisSample
{
   internal static class Program
   {
      private static void Main()
      {
         ControlFlowBetween();
         SimpleDemo();
      }

      private static void ControlFlowBetween()
      {
         var sourceTree = CSharpSyntaxTree.ParseText(@"
class C
{
    void M(int x)
    {
        L1: ; // 1
        if (x == 0) goto L1;    //firstIf
        if (x == 1) goto L2;
        if (x == 3) goto L3;
        L3: ;                   //label3
        L2: ; // 2
        if(x == 4) goto L3;
    }
}
");
         var mscorlib = MetadataReference.CreateFromFile(typeof (object).Assembly.Location);
         var compilation = CSharpCompilation.Create("MyCompilation", new[] {sourceTree}, new[] {mscorlib});
         var model = compilation.GetSemanticModel(sourceTree);

         var firstIf = sourceTree.GetRoot().DescendantNodes().OfType<IfStatementSyntax>().First();
         var label3 = sourceTree.GetRoot().DescendantNodes().OfType<LabeledStatementSyntax>().Skip(1).Take(1).Single();

         var result = model.AnalyzeControlFlow(firstIf, label3);
         Console.WriteLine(result.EntryPoints);
         Console.WriteLine(result.ExitPoints);
      }

      private static void SimpleDemo()
      {
         var sourceTree = CSharpSyntaxTree.ParseText(@"
class C
{
   void M()
   {
      for (int i = 0; i < 10; i++)
      {
            if (i == 3)
               continue;
            if (i == 8)
               break;
      }
   }
}
");
         var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
         var compilation = CSharpCompilation.Create("MyCompilation", new[] { sourceTree }, new[] { mscorlib });
         var model = compilation.GetSemanticModel(sourceTree);

         var firstFor = sourceTree.GetRoot().DescendantNodes().OfType<ForStatementSyntax>().Single();
         var result = model.AnalyzeControlFlow(firstFor.Statement);

         Console.WriteLine(result.Succeeded); // true
         Console.WriteLine(result.ExitPoints.Length); // 2 - continue, break         
      }
   }
}