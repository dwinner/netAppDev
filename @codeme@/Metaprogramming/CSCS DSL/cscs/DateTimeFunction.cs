using System;

namespace CsCsLang
{
   internal class DateTimeFunction : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         var strFormat = Utils.GetSafeString(args, 0, "hh:mm:ss.fff");
         Utils.CheckNotEmpty(strFormat, m_name);

         var when = DateTime.Now.ToString(strFormat);
         return new Variable(when);
      }
   }
}