/*
 * Simple parsing for CAPL source code
 */

using System;
using Antlr4.Runtime;
using CaplGrammar.Core;

namespace ValidatingGrammarSample
{
   internal static class Program
   {
      private const string CaplSourceCode = "arithmetic_operators.can";

      private static void Main()
      {
         AntlrInputStream antlrStream = new AntlrFileStream(CaplSourceCode);

         var caplLexer = new CaplLexer(antlrStream);
         var tokens = new CommonTokenStream(caplLexer);
         var caplParser = new CaplParser(tokens);
         caplParser.RemoveErrorListeners();
         var errorHandler = new DefaultErrorHandlerImpl();
         caplParser.AddErrorListener(errorHandler);
         var caplRoot = caplParser.primaryExpression();

         Console.WriteLine(caplRoot.ToStringTree());

         if (caplRoot.IsEmpty)
         {
            errorHandler.Errors.ForEach(error => Console.WriteLine(error));
         }
      }
   }
}