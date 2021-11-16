namespace CsCsLang.FunctionFlow
{
   internal class TokenizeLinesFunction : ParserFunction
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
         var sepStr = Utils.GetSafeString(args, 3, "\t");
         if (sepStr == "\\t") sepStr = "\t";
         var sep = sepStr.ToCharArray();

         //var function = GetFunction(varName);
         var allTokensVar = new Variable(Variable.VarType.Array);

         for (var counter = fromLine; counter < lines.Tuple.Count; counter++)
         {
            var lineVar = lines.Tuple[counter];
            //var toAdd = new Variable(counter - fromLine);
            var line = lineVar.AsString();
            var tokens = line.Split(sep);
            var tokensVar = new Variable(Variable.VarType.Array);
            foreach (var token in tokens) tokensVar.Tuple.Add(new Variable(token));
            allTokensVar.Tuple.Add(tokensVar);
         }

         AddGlobalOrLocalVariable(varName,
            new GetVarFunction(allTokensVar));

         return Variable.EmptyInstance;
      }
   }
}