using System;
using System.Collections.Generic;
using System.Linq;

namespace CsCsLang
{
   internal class GetRandomFunction : ParserFunction
   {
      private static readonly Random m_random = new Random();

      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);
         Utils.CheckArgs(args.Count, 1, m_name);
         var limit = args[0].AsInt();
         Utils.CheckPosInt(args[0]);
         var numberRandoms = Utils.GetSafeInt(args, 1, 1);

         if (numberRandoms <= 1) return new Variable(m_random.Next(0, limit));

         var available = Enumerable.Range(0, limit).ToList();
         var result = new List<Variable>();

         for (var i = 0; i < numberRandoms && available.Count > 0; i++)
         {
            var nextRandom = m_random.Next(0, available.Count);
            result.Add(new Variable(available[nextRandom]));
            available.RemoveAt(nextRandom);
         }

         return new Variable(result);
      }
   }
}