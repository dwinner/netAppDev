namespace CsCsLang
{
   internal class ReadCSCSFileFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var filename = Utils.GetStringOrVarValue(script);
         var lines = Utils.GetFileLines(filename);

         var results = Utils.ConvertToResults(lines);
         Interpreter.Instance.AppendOutput("Read " + lines.Length + " line(s).", true);

         return new Variable(results);
      }
   }
}