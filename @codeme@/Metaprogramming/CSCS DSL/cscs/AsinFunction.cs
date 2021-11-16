using System;

namespace CsCsLang
{
   internal class AsinFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Asin(arg.Value);
         return arg;
      }
   }
}