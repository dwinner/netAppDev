/*
 * Symbol usage validation
 */

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   internal static class Program
   {
      private const string CanFile = "usages.can";

      private static void Main()
      {
         AntlrInputStream antlrStream = new AntlrFileStream(CanFile);

         var caplLexer = new CaplLexer(antlrStream);
         var tokens = new CommonTokenStream(caplLexer);
         var caplParser = new CaplParser(tokens) { BuildParseTree = true };
         var sourceTree = caplParser.primaryExpression();

         //Console.WriteLine(sourceTree.ToStringTree());

         var walker = new ParseTreeWalker();
         var def = new DefPhase();
         walker.Walk(def, sourceTree);

         var globals = def.Globals;
         var scopes = def.Scopes;
      }
   }
}