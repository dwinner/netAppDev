namespace CsCsLang
{
   internal class MoreFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var filename = Utils.GetStringOrVarValue(script);
         var size = Constants.DefaultFileLines;

         var sizeAvailable = Utils.SeparatorExists(script);
         if (sizeAvailable)
         {
            var length = Utils.GetItem(script);
            Utils.CheckPosInt(length);
            size = (int) length.Value;
         }

         var lines = Utils.GetFileLines(filename, 0, size);
         var results = Utils.ConvertToResults(lines);

         return new Variable(results);
      }
   }
}