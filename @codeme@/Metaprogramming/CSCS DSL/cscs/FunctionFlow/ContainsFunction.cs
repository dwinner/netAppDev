namespace CsCsLang.FunctionFlow
{
   internal class ContainsFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // 1. Get the name of the variable.
         var varName = Utils.GetToken(script, Constants.NextOrEndArray);
         Utils.CheckNotEnd(script, m_name);

         // 2. Get the current value of the variable.
         var arrayIndices = Utils.GetArrayIndices(ref varName);

         var func = GetFunction(varName);
         Utils.CheckNotNull(varName, func);
         var currentValue = func.GetValue(script);

         // 2b. Special dealings with arrays:
         var query = arrayIndices.Count > 0 ? Utils.ExtractArrayElement(currentValue, arrayIndices) : currentValue;

         // 3. Get the value to be looked for.
         var searchValue = Utils.GetItem(script);
         Utils.CheckNotEnd(script, m_name);

         // 4. Check if the value to search for exists.
         var exists = query.Exists(searchValue, true /* notEmpty */);

         script.MoveBackIf(Constants.StartGroup);
         return new Variable(exists);
      }
   }
}