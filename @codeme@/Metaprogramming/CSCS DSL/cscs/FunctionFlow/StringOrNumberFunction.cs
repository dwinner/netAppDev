using System.Globalization;

namespace CsCsLang.FunctionFlow
{
   internal class StringOrNumberFunction : ParserFunction
   {
      public string Item { private get; set; }

      protected override Variable Evaluate(ParsingScript script)
      {
         // First check if the passed expression is a string between quotes.
         if (Item.Length > 1 &&
             Item[0] == Constants.Quote &&
             Item[Item.Length - 1] == Constants.Quote) return new Variable(Item.Substring(1, Item.Length - 2));

         // Otherwise this should be a number.
         if (!double.TryParse(Item, NumberStyles.Number | NumberStyles.AllowExponent,
            CultureInfo.InvariantCulture, out var num)) Utils.ThrowException(script, "parseToken", Item, "parseTokenExtra");
         return new Variable(num);
      }
   }
}