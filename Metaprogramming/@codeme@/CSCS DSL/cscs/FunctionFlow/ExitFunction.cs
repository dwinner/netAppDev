using System;

namespace CsCsLang.FunctionFlow
{
   internal class ExitFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         Environment.Exit(0);
         return Variable.EmptyInstance;
      }
   }
}