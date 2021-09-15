using System;

namespace CsCsLang
{
   internal class CeilFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Ceiling(arg.Value);
         return arg;
      }
   }
}