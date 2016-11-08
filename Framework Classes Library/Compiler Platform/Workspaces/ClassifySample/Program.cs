using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.MSBuild;
using static Microsoft.CodeAnalysis.Classification.ClassificationTypeNames;

namespace ClassifySample
{
   static class Program
   {
      private static readonly Solution Solution =
         MSBuildWorkspace.Create().OpenSolutionAsync(@"..\..\..\Playground\Playground.sln").Result;

      static void Main()
      {
         var project = Solution.Projects.Single(p => p.Name == "ConfigureAwaitTest");
         var barCs = project.Documents.Single(d => d.Name == "Bar.cs");

         var tree = barCs.GetSyntaxTreeAsync().Result;
         var root = tree.GetRootAsync().Result;

         // Get all the spans in the document that are classified as language elements
         var spans = Classifier.GetClassifiedSpansAsync(barCs, root.FullSpan)
            .Result.ToDictionary(c => c.TextSpan.Start, c => c);
         var txt = tree.GetText().ToString();
         var i = 0;
         foreach (var token in txt)
         {
            ClassifiedSpan span;
            if (spans.TryGetValue(i, out span))
            {
               var color = ConsoleColor.Gray;
               switch (span.ClassificationType)
               {
                  case Keyword:
                     color = ConsoleColor.Cyan;
                     break;
                  case StringLiteral:
                  case VerbatimStringLiteral:
                     color = ConsoleColor.Red;
                     break;
                  case Comment:
                     color = ConsoleColor.Green;
                     break;
                  case ClassName:
                  case InterfaceName:
                  case StructName:
                  case EnumName:
                  case TypeParameterName:
                  case DelegateName:
                     color = ConsoleColor.Yellow;
                     break;
                  case Identifier:
                     color = ConsoleColor.DarkGray;
                     break;
               }

               Console.ForegroundColor = color;
            }

            Console.Write(token);
            i++;
         }
      }
   }
}