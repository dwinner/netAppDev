namespace CsCsLang.FunctionFlow
{
   internal class TypeFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // 1. Get the name of the variable.
         var varName = Utils.GetToken(script, Constants.EndArgArray);
         Utils.CheckNotEnd(script, m_name);

         var arrayIndices = Utils.GetArrayIndices(ref varName);

         // 2. Get the current value of the variable.
         var func = GetFunction(varName);
         Utils.CheckNotNull(varName, func);
         var currentValue = func.GetValue(script);
         var element = currentValue;

         // 2b. Special case for an array.
         if (arrayIndices.Count > 0)
         { // array element
            element = Utils.ExtractArrayElement(currentValue, arrayIndices);
            script.MoveForwardIf(Constants.EndArray);
         }

         // 3. Convert type to string.
         var type = Constants.TypeToString(element.Type);
         script.MoveForwardIf(Constants.EndArg, Constants.Space);

         var newValue = new Variable(type);
         return newValue;
      }
   }
}