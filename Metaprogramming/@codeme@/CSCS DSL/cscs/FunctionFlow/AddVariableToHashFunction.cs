using System;

namespace CsCsLang.FunctionFlow
{
   internal class AddVariableToHashFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);
         Utils.CheckArgs(args.Count, 3, m_name);

         var varName = Utils.GetSafeString(args, 0);
         var toAdd = Utils.GetSafeVariable(args, 1);
         var hash = Utils.GetSafeString(args, 2);

         var function = GetFunction(varName);
         var mapVar = function != null ? function.GetValue(script) : new Variable(Variable.VarType.Array);

         mapVar.AddVariableToHash(hash, toAdd);
         for (var i = 3; i < args.Count; i++)
         {
            var hash2 = Utils.GetSafeString(args, 3);
            if (!string.IsNullOrWhiteSpace(hash2) &&
                !hash2.Equals(hash, StringComparison.OrdinalIgnoreCase)) mapVar.AddVariableToHash(hash2, toAdd);
         }

         AddGlobalOrLocalVariable(varName,
            new GetVarFunction(mapVar));

         return Variable.EmptyInstance;
      }
   }
}