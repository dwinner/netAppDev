namespace CsCsLang
{
   internal class IndexOfFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // 1. Get the name of the variable.
         var varName = Utils.GetToken(script, Constants.NextArgArray);
         Utils.CheckNotEmpty(script, varName, m_name);

         // 2. Get the current value of the variable.
         var func = GetFunction(varName);
         var currentValue = func.GetValue(script);

         // 3. Get the value to be looked for.
         var searchValue = Utils.GetItem(script);

         // 4. Apply the corresponding C# function.
         var basePart = currentValue.AsString();
         var search = searchValue.AsString();

         var result = basePart.IndexOf(search);
         return new Variable(result);
      }
   }
}