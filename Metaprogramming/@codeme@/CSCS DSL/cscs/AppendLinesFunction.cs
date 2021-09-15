namespace CsCsLang
{
   internal class AppendLinesFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var filename = Utils.GetStringOrVarValue(script);
         var lines = Utils.GetLinesFromList(script);
         Utils.AppendFileText(filename, lines);

         return Variable.EmptyInstance;
      }
   }
}