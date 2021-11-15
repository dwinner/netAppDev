namespace CsCsLang.FunctionFlow
{
   internal class AddFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // 1. Get the name of the variable.
         var varName = Utils.GetToken(script, Constants.NextOrEndArray);
         Utils.CheckNotEnd(script, m_name);

         // 2. Get the current value of the variable.
         var func = GetFunction(varName);
         Utils.CheckNotNull(varName, func);
         var currentValue = func.GetValue(script);

         // 3. Get the variable to add.
         var item = Utils.GetItem(script);

         // 4. Add it to the tuple.
         currentValue.AddVariable(item);

         AddGlobalOrLocalVariable(varName,
            new GetVarFunction(currentValue));

         return currentValue;
      }
   }
}