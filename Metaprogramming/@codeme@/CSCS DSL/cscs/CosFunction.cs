using System;

namespace CsCsLang
{
   internal class CosFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Cos(arg.Value);
         return arg;
      }
   }
}