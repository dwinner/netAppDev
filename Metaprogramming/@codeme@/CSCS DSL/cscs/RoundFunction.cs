using System;

namespace CsCsLang
{
   internal class RoundFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);
         Utils.CheckArgs(args.Count, 1, m_name);
         var numberDigits = Utils.GetSafeInt(args, 1, 0);

         args[0].Value = Math.Round(args[0].Value, numberDigits);
         return args[0];
      }
   }
}