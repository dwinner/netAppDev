using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Console;

namespace UsingCollectorSample
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         if (args.Length != 1)
         {
            ShowUsage();
            return;
         }

         var path = args[0];
         if (!Directory.Exists(path))
         {
            ShowUsage();
            return;
         }

         ProcessUsingAsync(path).Wait();
      }

      private static async Task ProcessUsingAsync(string path)
      {
         const string searchPattern = "*.cs";
         var collector = new UsingCollector();

         var fileNames =
            Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories)
               .Where(fileName => !fileName.EndsWith(".g.i.cs") && !fileName.EndsWith(".g.cs"));
         foreach (var tree in fileNames.Select(File.ReadAllText).Select(code => CSharpSyntaxTree.ParseText(code)))
         {
            var root = await tree.GetRootAsync().ConfigureAwait(false);
            collector.Visit(root);
         }

         var usings = collector.UsingDirectives;
         if (usings != null)
         {
            var usingDirectiveSyntaxies = usings as UsingDirectiveSyntax[] ?? usings.ToArray();
            var usingStatics =
               usingDirectiveSyntaxies.Select(usingDirectiveSyntax => usingDirectiveSyntax.ToString())
                  .Distinct()
                  .Where(s => s.StartsWith("using static"))
                  .OrderBy(s => s);
            var orderedUsings =
               usingDirectiveSyntaxies.Select(usingDirectiveSyntax => usingDirectiveSyntax.ToString())
                  .Distinct()
                  .Except(usingStatics)
                  .OrderBy(s => s.Substring(0, s.Length - 1));
            foreach (var @using in orderedUsings.Union(usingStatics))
            {
               WriteLine(@using);
            }
         }
      }

      private static void ShowUsage() => WriteLine("Usage: UsingCollectorSample directory");
   }

   internal class UsingCollector : CSharpSyntaxWalker
   {
      private readonly List<UsingDirectiveSyntax> _usingDirectives = new List<UsingDirectiveSyntax>();

      public IEnumerable<UsingDirectiveSyntax> UsingDirectives => _usingDirectives;

      public override void VisitUsingDirective(UsingDirectiveSyntax node)
      {
         _usingDirectives.Add(node);
      }
   }
}