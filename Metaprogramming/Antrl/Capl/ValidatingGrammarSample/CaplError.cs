using Antlr4.Runtime;

namespace ValidatingGrammarSample
{
   public struct CaplError
   {
      public CaplError(int line, int column, string message, IToken token)
      {
         Line = line;
         Column = column;
         Message = message;
         Token = token;
      }

      public int Line { get; }

      public int Column { get; }

      public string Message { get; }

      public IToken Token { get; }

      public override string ToString() =>
         $"{nameof(Line)}: {Line}, {nameof(Column)}: {Column}, {nameof(Message)}: {Message}, {nameof(Token)}: {Token}";
   }
}