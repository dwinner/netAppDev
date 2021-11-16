using System;

namespace CsCsLang
{
   internal class PiFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script) => new Variable(Math.PI);
   }
}