using System;

namespace CsCsLang
{
   internal class AbsFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Abs(arg.Value);
         return arg;
      }
   }
}