using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public static class CheckSymbolsUtils
   {
      // TODO: Add other type there
      public static BuiltInType GetCaplType(int tokenType) =>
         tokenType switch
         {
            CaplParser.Void => BuiltInType.Void,
            CaplParser.Int => BuiltInType.Int,
            CaplParser.Float => BuiltInType.Float,
            CaplParser.Char => BuiltInType.Char,
            _ => BuiltInType.Invalid
         };
   }
}