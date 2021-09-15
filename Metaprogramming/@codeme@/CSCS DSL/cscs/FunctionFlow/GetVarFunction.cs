using System;
using System.Collections.Generic;

namespace CsCsLang.FunctionFlow
{
   internal class GetVarFunction : ParserFunction
   {
      private List<Variable> m_arrayIndices;
      private int m_delta;

      private readonly Variable m_value;

      internal GetVarFunction(Variable value) => m_value = value;

      public int Delta
      {
         set => m_delta = value;
      }

      public List<Variable> Indices
      {
         set => m_arrayIndices = value;
      }

      protected override Variable Evaluate(ParsingScript script)
      {
         // First check if this element is part of an array:
         if (script.TryPrev() == Constants.StartArray)
         {
            // There is an index given - it must be for an element of the tuple.
            if (m_value.Tuple == null || m_value.Tuple.Count == 0)
               throw new ArgumentException("No tuple exists for the index");

            if (m_arrayIndices == null)
            {
               var startName = script.Substr(script.Pointer - 1);
               m_arrayIndices = Utils.GetArrayIndices(ref startName, ref m_delta);
            }

            script.Forward(m_delta);
            while (script.MoveForwardIf(Constants.EndArray))
            {
            }

            var result = Utils.ExtractArrayElement(m_value, m_arrayIndices);
            return result;
         }

         // Otherwise just return the stored value.
         return m_value;
      }
   }
}