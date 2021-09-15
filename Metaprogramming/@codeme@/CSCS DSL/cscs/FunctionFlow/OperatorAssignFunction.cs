namespace CsCsLang.FunctionFlow
{
   internal class OperatorAssignFunction : ActionFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         // Value to be added to the variable:
         var right = Utils.GetItem(script);

         var arrayIndices = Utils.GetArrayIndices(ref m_name);

         var func = GetFunction(m_name);
         Utils.CheckNotNull(m_name, func);

         var currentValue = func.GetValue(script);
         var left = currentValue;

         if (arrayIndices.Count > 0)
         { // array element
            left = Utils.ExtractArrayElement(currentValue, arrayIndices);
            script.MoveForwardIf(Constants.EndArray);
         }

         if (left.Type == Variable.VarType.Number) NumberOperator(left, right, m_action);
         else StringOperator(left, right, m_action);

         if (arrayIndices.Count > 0)
         { // array element
            AssignFunction.ExtendArray(currentValue, arrayIndices, 0, left);
            AddGlobalOrLocalVariable(m_name,
               new GetVarFunction(currentValue));
         }
         else
            AddGlobalOrLocalVariable(m_name,
               new GetVarFunction(left));
         return left;
      }

      private static void NumberOperator(Variable valueA,
         Variable valueB, string action)
      {
         switch (action)
         {
            case "+=":
               valueA.Value += valueB.Value;
               break;
            case "-=":
               valueA.Value -= valueB.Value;
               break;
            case "*=":
               valueA.Value *= valueB.Value;
               break;
            case "/=":
               valueA.Value /= valueB.Value;
               break;
            case "%=":
               valueA.Value %= valueB.Value;
               break;
            case "&=":
               valueA.Value = (int) valueA.Value & (int) valueB.Value;
               break;
            case "|=":
               valueA.Value = (int) valueA.Value | (int) valueB.Value;
               break;
            case "^=":
               valueA.Value = (int) valueA.Value ^ (int) valueB.Value;
               break;
         }
      }

      private static void StringOperator(Variable valueA,
         Variable valueB, string action)
      {
         switch (action)
         {
            case "+=":
               if (valueB.Type == Variable.VarType.String) valueA.String += valueB.AsString();
               else valueA.String += valueB.Value;
               break;
         }
      }

      public override ParserFunction NewInstance() => new OperatorAssignFunction();
   }
}