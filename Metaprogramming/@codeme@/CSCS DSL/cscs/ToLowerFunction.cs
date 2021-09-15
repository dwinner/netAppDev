namespace CsCsLang
{
   internal class ToLowerFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // 1. Get the name of the variable.
         var varName = Utils.GetToken(script, Constants.EndArgArray);
         Utils.CheckNotEmpty(script, varName, m_name);

         // 2. Get the current value of the variable.
         var func = GetFunction(varName);
         var currentValue = func.GetValue(script);

         // 3. Take either the string part if it is defined,
         // or the numerical part converted to a string otherwise.
         var arg = currentValue.AsString();

         var newValue = new Variable(arg.ToLower());
         return newValue;
      }
   }
}