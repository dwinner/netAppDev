namespace CsCsLang
{
   internal class WriteLinesFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         //string filename = Utils.ResultToString(Utils.GetItem(script));
         var filename = Utils.GetStringOrVarValue(script);
         var lines = Utils.GetLinesFromList(script);
         Utils.WriteFileText(filename, lines);

         return Variable.EmptyInstance;
      }
   }
}