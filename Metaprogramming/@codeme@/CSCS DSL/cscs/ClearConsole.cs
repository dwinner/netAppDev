using System;

namespace CsCsLang
{
   internal class ClearConsole : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         Console.Clear();
         return Variable.EmptyInstance;
      }
   }
}