using Antlr4.Runtime;

namespace CaplGrammar.Application.Poco
{
   public struct CaplIssue
   {
      public CaplIssue(string message, IToken token)
      {
         Message = message;
         Token = token;
      }

      public string Message { get; }

      public IToken Token { get; }

      public override string ToString() =>
         $"Line: {Token.Line}, Column: {Token.Column}, Message: {Message}";
   }
}