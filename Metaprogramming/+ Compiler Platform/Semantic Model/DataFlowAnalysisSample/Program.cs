// Анализ данных в потоке управления

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DataFlowAnalysisSample
{
   internal static class Program
   {
      private static readonly SyntaxTree SourceTree = CSharpSyntaxTree.ParseText(@"
public class Sample
{
   public void Foo()
   {
        int[] outerArray = new int[10] { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4};
        for (int index = 0; index < 10; index++)
        {
             int[] innerArray = new int[10] { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4 };
             index = index + 2;
             outerArray[index - 1] = 5;
        }
   }
}");

      private static void Main()
      {
         var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
         var compilation = CSharpCompilation.Create("MyCompilation", new[] { SourceTree }, new[] { mscorlib });
         var model = compilation.GetSemanticModel(SourceTree);
         var forStatement = SourceTree.GetRoot().DescendantNodes().OfType<ForStatementSyntax>().Single();
         var result = model.AnalyzeDataFlow(forStatement);

         if (result.Succeeded)
         {
            var alwaysAssigned = result.AlwaysAssigned;
            Console.WriteLine("Assigned always symbols:");
            Output(alwaysAssigned);

            var variablesDeclared = result.VariablesDeclared;
            Console.WriteLine("Declared variables");
            Output(variablesDeclared);

            var captured = result.Captured;
            Console.WriteLine("Local Captured");
            Output(captured);

            var dataFlowsIn = result.DataFlowsIn;
            Console.WriteLine("Assigned outside, and used inside");
            Output(dataFlowsIn);

            var dataFlowsOut = result.DataFlowsOut;
            Console.WriteLine("Assigned inside, and used outside");
            Output(dataFlowsOut);

            var insideSymbols = result.ReadInside;
            Console.WriteLine("Read inside region");
            Output(insideSymbols);

            var readOutside = result.ReadOutside;
            Console.WriteLine("Read outside region");
            Output(readOutside);

            var writtenInside = result.WrittenInside;
            Console.WriteLine("Written inside region");
            Output(writtenInside);

            var writtenOutside = result.WrittenOutside;
            Console.WriteLine("Written outside region");
            Output(writtenOutside);
         }
      }

      private static void Output(ImmutableArray<ISymbol> symbols)
      {
         if (symbols.Length == 0)
         {
            Console.WriteLine("\tNone");
            return;
         }

         foreach (var symbol in symbols)
         {
            Console.WriteLine("\t{0}", symbol);
         }
      }
   }
}