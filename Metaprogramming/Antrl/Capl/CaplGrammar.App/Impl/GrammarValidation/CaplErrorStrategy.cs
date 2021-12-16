using Antlr4.Runtime;

namespace CaplGrammar.Application.Impl.GrammarValidation
{
   internal class CaplErrorStrategy : DefaultErrorStrategy
   {
      protected override void ReportNoViableAlternative(Parser recognizer, NoViableAltException e)
      {
         const string message = "Syntax error in CAPL source code";
         recognizer.NotifyErrorListeners(e.OffendingToken, message, e);
      }
   }
}