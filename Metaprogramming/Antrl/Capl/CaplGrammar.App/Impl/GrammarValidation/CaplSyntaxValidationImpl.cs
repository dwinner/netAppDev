using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using CaplGrammar.Application.Contract;
using CaplGrammar.Application.Poco;
using CaplGrammar.Core;

namespace CaplGrammar.Application.Impl.GrammarValidation
{
   public sealed class CaplSyntaxValidationImpl : CaplValidationBase
   {
      public override IEnumerable<CaplIssue> GetIssues(string sourceFile)
      {
         AntlrInputStream antlrStream = new AntlrFileStream(sourceFile, CurrentEncoding);
         var caplLexer = new CaplLexer(antlrStream);
         var tokens = new CommonTokenStream(caplLexer);
         var caplParser = new CaplParser(tokens);
         caplParser.RemoveErrorListeners();
         var errorHandler = new DefaultErrorHandlerImpl();
         caplParser.AddErrorListener(errorHandler);
         var caplRoot = caplParser.primaryExpression();

         return caplRoot.IsEmpty ? errorHandler.Issues : Enumerable.Empty<CaplIssue>();
      }
   }
}