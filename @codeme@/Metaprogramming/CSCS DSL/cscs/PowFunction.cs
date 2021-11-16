using System;

namespace CsCsLang
{
   internal class PowFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg1 = script.ExecuteTo(Constants.NextArg);
         script.Forward(); // eat separation
         var arg2 = script.ExecuteTo(Constants.EndArg);

         arg1.Value = Math.Pow(arg1.Value, arg2.Value);
         return arg1;
      }
   }
}