namespace CsCsLang.FunctionFlow
{
   internal class SizeFunction : ParserFunction
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

         // 3. Take either the length of the underlying tuple or
         // string part if it is defined,
         // or the numerical part converted to a string otherwise.
         var size = element.Type == Variable.VarType.Array ? element.Tuple.Count : element.AsString().Length;

         script.MoveForwardIf(Constants.EndArg, Constants.Space);

         var newValue = new Variable(size);
         return newValue;
      }
   }
}