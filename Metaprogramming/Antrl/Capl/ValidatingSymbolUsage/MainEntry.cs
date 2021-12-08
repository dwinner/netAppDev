/*
 * Symbol usage validation
 */

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
         var caplParser = new CaplParser(tokens) { BuildParseTree = true };
         var caplAst = caplParser.primaryExpression();

         var walker = new ParseTreeWalker();
         var definitionPhase = new DefinitionPhaseVisitor();
         walker.Walk(definitionPhase, caplAst);

         var globalScope = definitionPhase.GlobalScope;
         var scopes = definitionPhase.Scopes;
         var referencePhase = new ReferencePhaseVisitor(globalScope, scopes);
         walker.Walk(referencePhase, caplAst);
      }
   }
}