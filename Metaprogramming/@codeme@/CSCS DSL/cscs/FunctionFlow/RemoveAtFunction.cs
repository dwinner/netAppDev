namespace CsCsLang.FunctionFlow
{
   internal class RemoveAtFunction : ParserFunction
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
         Utils.CheckArray(currentValue, varName);

         // 3. Get the variable to remove.
         var item = Utils.GetItem(script);
         Utils.CheckNonNegativeInt(item);

         currentValue.Tuple.RemoveAt(item.AsInt());

         AddGlobalOrLocalVariable(varName,
            new GetVarFunction(currentValue));
         return Variable.EmptyInstance;
      }
   }
}