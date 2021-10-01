using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;

namespace ValidatingGrammarSample
{
   public sealed class DefaultErrorHandlerImpl : BaseErrorListener
   {
      private const int DefaultCapacity = 0x10;

      public DefaultErrorHandlerImpl() => Errors = new List<CaplError>(DefaultCapacity);

      public List<CaplError> Errors { get; }

      public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol,
         int line, int charPositionInLine, string msg, RecognitionException e)
      {
         var caplError = new CaplError(line, charPositionInLine, msg, offendingSymbol);
         Errors.Add(caplError);
      }
   }
}