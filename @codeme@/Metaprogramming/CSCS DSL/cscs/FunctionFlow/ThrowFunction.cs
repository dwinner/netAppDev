using System;

namespace CsCsLang.FunctionFlow
{
   internal class ThrowFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // 1. Extract what to throw.
         var arg = Utils.GetItem(script);

         // 2. Convert it to a string.
         var result = arg.AsString();

         // 3. Throw it!
         throw new ArgumentException(result);
      }
   }
}