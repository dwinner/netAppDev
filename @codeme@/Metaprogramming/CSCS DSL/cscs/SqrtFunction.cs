using System;

namespace CsCsLang
{
   internal class SqrtFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Sqrt(arg.Value);
         return arg;
      }
   }
}