using System;

namespace CsCsLang
{
   internal class LogFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var arg = script.ExecuteTo(Constants.EndArg);
         arg.Value = Math.Log(arg.Value);
         return arg;
      }
   }
}