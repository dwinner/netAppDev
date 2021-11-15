using System;
using System.Collections.Generic;

namespace CsCsLang.FunctionFlow
{
   internal class GetColumnFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);
         Utils.CheckArgs(args.Count, 2, m_name);

         var arrayVar = Utils.GetSafeVariable(args, 0);
         var col = Utils.GetSafeInt(args, 1);
         var fromCol = Utils.GetSafeInt(args, 2, 0);

         var tuple = arrayVar.Tuple;

         var result = new List<Variable>(tuple.Count);
         for (var i = fromCol; i < tuple.Count; i++)
         {
            var current = tuple[i];
            if (current.Tuple == null || current.Tuple.Count <= col)
               throw new ArgumentException(m_name + ": Index [" + col + "] doesn't exist in column " +
                                           i + "/" + (tuple.Count - 1));
            result.Add(current.Tuple[col]);
         }

         return new Variable(result);
      }
   }
}