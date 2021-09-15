using System;

namespace CsCsLang
{
   internal class AcosFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Acos(arg.Value);
         return arg;
      }
   }
}