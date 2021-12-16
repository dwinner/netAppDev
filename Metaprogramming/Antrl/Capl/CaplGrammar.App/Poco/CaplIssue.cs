using Antlr4.Runtime;

namespace CaplGrammar.Application.Poco
{
   public struct CaplIssue
   {
      public CaplIssue(string message, IToken token, int actualLine, int actualColumn)
      {
         Message = message;
         Token = token;
         ActualLine = actualLine;
         ActualColumn = actualColumn;
      }

      public string Message { get; }

      public IToken Token { get; }

      public int ActualLine { get; }

      public int ActualColumn { get; }

      public override string ToString() =>
         $"Line: {Token.Line}, Column: {Token.Column}, Message: {Message}";
   }
}