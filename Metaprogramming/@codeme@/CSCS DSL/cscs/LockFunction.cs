namespace CsCsLang
{
   internal class LockFunction : ParserFunction
   {
      private static readonly object lockObject = new object();

      protected override Variable Evaluate(ParsingScript script)
      {
         var body = Utils.GetBodyBetween(script, Constants.StartArg,
            Constants.EndArg);
         var threadScript = new ParsingScript(body);

         lock (lockObject)
         {
            threadScript.ExecuteAll();
         }
         return Variable.EmptyInstance;
      }
   }
}