using System;

namespace CsCsLang
{
   internal class PrintFunction : ParserFunction
   {
      private readonly bool m_changeColor;
      private readonly ConsoleColor m_fgcolor;

      private readonly bool m_newLine = true;

      internal PrintFunction(bool newLine = true) => m_newLine = newLine;

      internal PrintFunction(ConsoleColor fgcolor)
      {
         m_fgcolor = fgcolor;
         m_changeColor = true;
      }

      protected override Variable Evaluate(ParsingScript script)
      {
         bool isList;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         var output = string.Empty;
         for (var i = 0; i < args.Count; i++) output += args[i].AsString();

         output += m_newLine ? Environment.NewLine : string.Empty;
         if (m_changeColor) Utils.PrintColor(output, m_fgcolor);
         else Interpreter.Instance.AppendOutput(output);

         return Variable.EmptyInstance;
      }
   }
}