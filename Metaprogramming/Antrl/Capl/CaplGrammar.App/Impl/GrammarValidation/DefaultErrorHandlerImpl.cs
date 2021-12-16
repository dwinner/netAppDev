using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using CaplGrammar.Application.Poco;

namespace CaplGrammar.Application.Impl.GrammarValidation
{
   internal sealed class DefaultErrorHandlerImpl : BaseErrorListener
   {
      private const int DefaultCapacity = 0x10;

      public DefaultErrorHandlerImpl() => Issues = new List<CaplIssue>(DefaultCapacity);

      public List<CaplIssue> Issues { get; }

      public override void SyntaxError(TextWriter output,
         IRecognizer recognizer,
         IToken offendingSymbol,
         int line,
         int charPositionInLine,
         string msg,
         RecognitionException e)
      {
         var caplIssue = new CaplIssue(msg, offendingSymbol);
         Issues.Add(caplIssue);
      }
   }
}