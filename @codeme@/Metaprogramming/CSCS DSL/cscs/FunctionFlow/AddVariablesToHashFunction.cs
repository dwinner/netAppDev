using System;

namespace CsCsLang.FunctionFlow
{
   internal class AddVariablesToHashFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);
         Utils.CheckArgs(args.Count, 3, m_name);

         var varName = Utils.GetSafeString(args, 0);
         var lines = Utils.GetSafeVariable(args, 1);
         var fromLine = Utils.GetSafeInt(args, 2);
         var hash2 = Utils.GetSafeString(args, 3);
         var sepStr = Utils.GetSafeString(args, 4, "\t");
         if (sepStr == "\\t") sepStr = "\t";
         var sep = sepStr.ToCharArray();

         var function = GetFunction(varName);
         var mapVar = function != null ? function.GetValue(script) : new Variable(Variable.VarType.Array);

         for (var counter = fromLine; counter < lines.Tuple.Count; counter++)
         {
            var lineVar = lines.Tuple[counter];
            var toAdd = new Variable(counter - fromLine);
            var line = lineVar.AsString();
            var tokens = line.Split(sep);
            var hash = tokens[0];
            mapVar.AddVariableToHash(hash, toAdd);
            if (!string.IsNullOrWhiteSpace(hash2) &&
                !hash2.Equals(hash, StringComparison.OrdinalIgnoreCase)) mapVar.AddVariableToHash(hash2, toAdd);
         }

         AddGlobalOrLocalVariable(varName,
            new GetVarFunction(mapVar));
         return Variable.EmptyInstance;
      }
   }
}