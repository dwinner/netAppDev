using System;
using System.Collections.Generic;

namespace CsCsLang
{
   internal class TokenizeFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         Utils.CheckArgs(args.Count, 1, m_name);
         var data = Utils.GetSafeString(args, 0);

         var sep = Utils.GetSafeString(args, 1, "\t");
         if (sep == "\\t") sep = "\t";
         var sepArray = sep.ToCharArray();
         var tokens = data.Split(sepArray);

         var option = Utils.GetSafeString(args, 2);

         var results = new List<Variable>();
         for (var i = 0; i < tokens.Length; i++)
         {
            var token = tokens[i];
            if (i > 0 && string.IsNullOrWhiteSpace(token) &&
                option.StartsWith("prev", StringComparison.OrdinalIgnoreCase)) token = tokens[i - 1];
            results.Add(new Variable(token));
         }

         return new Variable(results);
      }
   }
}