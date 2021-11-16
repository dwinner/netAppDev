using System;

namespace CsCsLang
{
   internal class FloorFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Floor(arg.Value);
         return arg;
      }
   }
}