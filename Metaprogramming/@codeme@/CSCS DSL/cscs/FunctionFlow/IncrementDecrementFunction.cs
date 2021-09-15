using System;

namespace CsCsLang.FunctionFlow
{
   internal class IncrementDecrementFunction : ActionFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var prefix = string.IsNullOrWhiteSpace(m_name);
         if (prefix) m_name = Utils.GetToken(script, Constants.TokenSeparation);

         // Value to be added to the variable:
         var valueDelta = m_action == Constants.Increment ? 1 : -1;
         var returnDelta = prefix ? valueDelta : 0;

         // Check if the variable to be set has the form of x[a][b],
         // meaning that this is an array element.
         double newValue = 0;
         var arrayIndices = Utils.GetArrayIndices(ref m_name);

         var func = GetFunction(m_name);
         Utils.CheckNotNull(m_name, func);

         var currentValue = func.GetValue(script);

         if (arrayIndices.Count > 0 || script.TryCurrent() == Constants.StartArray)
         {
            if (prefix)
            {
               var tmpName = m_name + script.Rest;
               var delta = 0;
               arrayIndices = Utils.GetArrayIndices(ref tmpName, ref delta);
               script.Forward(Math.Max(0, delta - tmpName.Length));
            }

            var element = Utils.ExtractArrayElement(currentValue, arrayIndices);
            script.MoveForwardIf(Constants.EndArray);

            newValue = element.Value + returnDelta;
            element.Value += valueDelta;
         }
         else
         { // A normal variable.
            newValue = currentValue.Value + returnDelta;
            currentValue.Value += valueDelta;
         }

         AddGlobalOrLocalVariable(m_name,
            new GetVarFunction(currentValue));
         return new Variable(newValue);
      }

      public override ParserFunction NewInstance() => new IncrementDecrementFunction();
   }
}