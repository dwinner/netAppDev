using System;

namespace CsCsLang
{
   internal class ExpFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var result = script.ExecuteTo(Constants.EndArg);
         result.Value = Math.Exp(result.Value);
         return result;
      }
   }
}