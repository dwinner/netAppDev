namespace CsCsLang.FunctionFlow
{
   internal class TokenCounterFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);
         Utils.CheckArgs(args.Count, 2, m_name);

         var all = Utils.GetSafeVariable(args, 0);
         var varName = Utils.GetSafeString(args, 1);
         var index = Utils.GetSafeInt(args, 2);

         var function = GetFunction(varName);
         var mapVar = new Variable(Variable.VarType.Array);

         if (all.Tuple == null) return Variable.EmptyInstance;

         var currentValue = "";
         var currentCount = 0;

         var globalCount = 0;

         for (var i = 0; i < all.Tuple.Count; i++)
         {
            var current = all.Tuple[i];
            if (current.Tuple == null || current.Tuple.Count < index) break;
            var newValue = current.Tuple[index].AsString();
            if (currentValue != newValue)
            {
               currentValue = newValue;
               currentCount = 0;
            }
            mapVar.Tuple.Add(new Variable(currentCount));
            currentCount++;
            globalCount++;
         }

         AddGlobalOrLocalVariable(varName,
            new GetVarFunction(mapVar));
         return mapVar;
      }
   }
}