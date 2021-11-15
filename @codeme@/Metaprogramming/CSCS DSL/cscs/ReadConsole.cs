using System;

namespace CsCsLang
{
   internal class ReadConsole : ParserFunction
   {
      private readonly bool m_isNumber;

      internal ReadConsole(bool isNumber = false) => m_isNumber = isNumber;

      protected override Variable Evaluate(ParsingScript script)
      {
         script.Forward(); // Skip opening parenthesis.
         var line = Console.ReadLine();

         if (!m_isNumber) return new Variable(line);

         var number = double.NaN;
         if (!double.TryParse(line, out number)) throw new ArgumentException("Couldn't parse number [" + line + "]");

         return new Variable(number);
      }
   }
}