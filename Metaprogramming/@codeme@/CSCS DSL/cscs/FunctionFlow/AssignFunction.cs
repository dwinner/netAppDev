using System.Collections.Generic;

namespace CsCsLang.FunctionFlow
{
   internal class AssignFunction : ActionFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var varValue = Utils.GetItem(script);

         // Check if the variable to be set has the form of x[a][b]...,
         // meaning that this is an array element.
         var arrayIndices = Utils.GetArrayIndices(ref m_name);

         if (arrayIndices.Count == 0)
         {
            AddGlobalOrLocalVariable(m_name, new GetVarFunction(varValue));
            return varValue;
         }

         Variable array;

         var pf = GetFunction(m_name);
         if (pf != null) array = pf.GetValue(script);
         else array = new Variable();

         ExtendArray(array, arrayIndices, 0, varValue);

         AddGlobalOrLocalVariable(m_name, new GetVarFunction(array));
         return array;
      }

      public override ParserFunction NewInstance() => new AssignFunction();

      public static void ExtendArray(Variable parent,
         List<Variable> arrayIndices,
         int indexPtr,
         Variable varValue)
      {
         if (arrayIndices.Count <= indexPtr) return;

         var index = arrayIndices[indexPtr];
         var currIndex = ExtendArrayHelper(parent, index);

         if (arrayIndices.Count - 1 == indexPtr)
         {
            parent.Tuple[currIndex] = varValue;
            return;
         }

         var son = parent.Tuple[currIndex];
         ExtendArray(son, arrayIndices, indexPtr + 1, varValue);
      }

      private static int ExtendArrayHelper(Variable parent, Variable indexVar)
      {
         parent.SetAsArray();

         var arrayIndex = parent.GetArrayIndex(indexVar);
         if (arrayIndex < 0)
         {
            // This is not a "normal index" but a new string for the dictionary.
            var hash = indexVar.AsString();
            arrayIndex = parent.SetHashVariable(hash, Variable.NewEmpty());
            return arrayIndex;
         }

         if (parent.Tuple.Count <= arrayIndex)
            for (var i = parent.Tuple.Count; i <= arrayIndex; i++) parent.Tuple.Add(Variable.NewEmpty());
         return arrayIndex;
      }
   }
}