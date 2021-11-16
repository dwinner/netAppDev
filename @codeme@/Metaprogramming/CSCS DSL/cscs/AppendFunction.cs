using CsCsLang.FunctionFlow;

namespace CsCsLang
{
   internal class AppendFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // 1. Get the name of the variable.
         var varName = Utils.GetToken(script, Constants.NextArgArray);
         Utils.CheckNotEmpty(script, varName, m_name);

         // 2. Get the current value of the variable.
         var func = GetFunction(varName);
         var currentValue = func.GetValue(script);

         // 3. Get the value to be added or appended.
         var newValue = Utils.GetItem(script);

         // 4. Take either the string part if it is defined,
         // or the numerical part converted to a string otherwise.
         var arg1 = currentValue.AsString();
         var arg2 = newValue.AsString();

         // 5. The variable becomes a string after adding a string to it.
         newValue.Reset();
         newValue.String = arg1 + arg2;

         AddGlobalOrLocalVariable(varName, new GetVarFunction(newValue));

         return newValue;
      }
   }
}