namespace CsCsLang.FunctionFlow
{
   internal class GetAllKeysFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var varName = Utils.GetItem(script);
         Utils.CheckNotNull(varName, m_name);

         var results = varName.GetAllKeys();

         return new Variable(results);
      }
   }
}