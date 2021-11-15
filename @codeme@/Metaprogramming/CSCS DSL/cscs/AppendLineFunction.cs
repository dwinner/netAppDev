using System;

namespace CsCsLang
{
   internal class AppendLineFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var filename = Utils.GetStringOrVarValue(script);
         var line = Utils.GetItem(script);
         Utils.AppendFileText(filename, line.AsString() + Environment.NewLine);

         return Variable.EmptyInstance;
      }
   }
}