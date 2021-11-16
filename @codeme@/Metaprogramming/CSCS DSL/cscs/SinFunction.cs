using System;

namespace CsCsLang
{
   internal class SinFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Sin(arg.Value);
         return arg;
      }
   }
}