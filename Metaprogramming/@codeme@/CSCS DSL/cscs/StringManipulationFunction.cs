using System;

namespace CsCsLang
{
   internal class StringManipulationFunction : ParserFunction
   {
      public enum Mode
      {
         CONTAINS,
         STARTS_WITH,
         ENDS_WITH,
         INDEX_OF,
         EQUALS,
         REPLACE,
         UPPER,
         LOWER,
         TRIM,
         SUBSTRING
      }

      private readonly Mode m_mode;

      public StringManipulationFunction(Mode mode) => m_mode = mode;

      protected override Variable Evaluate(ParsingScript script)
      {
         var rest = script.Rest;
         var isList = false;
         var args = Utils.GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         Utils.CheckArgs(args.Count, 1, m_name);
         var source = Utils.GetSafeString(args, 0);
         var argument = Utils.GetSafeString(args, 1);
         var parameter = Utils.GetSafeString(args, 2, "case");
         var startFrom = Utils.GetSafeInt(args, 3, 0);
         var length = Utils.GetSafeInt(args, 4, source.Length);

         var comp = StringComparison.Ordinal;
         if (parameter.Equals("nocase") || parameter.Equals("no_case")) comp = StringComparison.OrdinalIgnoreCase;

         switch (m_mode)
         {
            case Mode.CONTAINS:
               return new Variable(source.IndexOf(argument, comp) >= 0);
            case Mode.STARTS_WITH:
               return new Variable(source.StartsWith(argument, comp));
            case Mode.ENDS_WITH:
               return new Variable(source.EndsWith(argument, comp));
            case Mode.INDEX_OF:
               return new Variable(source.IndexOf(argument, startFrom, comp));
            case Mode.EQUALS:
               return new Variable(source.Equals(argument, comp));
            case Mode.REPLACE:
               return new Variable(source.Replace(argument, parameter));
            case Mode.UPPER:
               return new Variable(source.ToUpper());
            case Mode.LOWER:
               return new Variable(source.ToLower());
            case Mode.TRIM:
               return new Variable(source.Trim());
            case Mode.SUBSTRING:
               return new Variable(source.Substring(startFrom, length));
         }

         return new Variable(-1);
      }
   }
}