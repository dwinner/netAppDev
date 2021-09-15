using System;

namespace CsCsLang
{
   internal class WriteLineFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var filename = Utils.GetStringOrVarValue(script);
         var line = Utils.GetItem(script);
         Utils.WriteFileText(filename, line.AsString() + Environment.NewLine);

         return Variable.EmptyInstance;
      }
   }
}