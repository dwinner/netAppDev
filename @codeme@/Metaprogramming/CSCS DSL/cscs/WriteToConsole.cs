using System;

namespace CsCsLang
{
   internal class WriteToConsole : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         bool isList;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         //if (args.Count > 0 && args[0] == "TIMESTAMP")

         for (var i = 0; i < args.Count; i++) Console.Write(args[i].AsString());
         Console.WriteLine();

         return Variable.EmptyInstance;
      }
   }
}