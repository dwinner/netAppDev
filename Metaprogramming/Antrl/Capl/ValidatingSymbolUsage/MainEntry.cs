/*
 * Symbol usage validation
 */

using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   internal static class MainEntry
   {
      private const string CanFile = "usages.can";

      private static void Main()
      {
         var antlrStream = new AntlrFileStream(CanFile);

         var caplLexer = new CaplLexer(antlrStream);
         var tokens = new CommonTokenStream(caplLexer);
         var caplParser = new CaplParser(tokens) {BuildParseTree = true};
         var sourceTree = caplParser.primaryExpression();

         //Console.WriteLine(sourceTree.ToStringTree());

         var walker = new ParseTreeWalker();
         var def = new DefinitionPhaseVisitor();
         walker.Walk(def, sourceTree);

         //var scopes = def.Scopes;

         Console.WriteLine("Globals:");
         Console.WriteLine("-------------------------------------------------");
         var globalSpace = def.GlobalSpace?.Symbols;
         if (globalSpace != null)
         {
            foreach (var (_, value) in globalSpace)
            {
               Console.WriteLine($"{value}");
            }
         }

         Console.WriteLine("Variables:");
         Console.WriteLine("-------------------------------------------------");
         var varSpace = def.VariableSpace?.Symbols;
         if (varSpace != null)
         {
            foreach (var (_, value) in varSpace)
            {
               Console.WriteLine($"{value}");
            }
         }

         // NOTE: Descent into def.GlobalSpace recursively to reveal local symbols
      }
   }
}