using CaplGrammar.Core;
using static ValidatingSymbolUsage.BuiltInType;

namespace ValidatingSymbolUsage
{
   public static class CaplSymbolExtensions
   {
      // TODO: Add other type there
      public static BuiltInType GetCaplType(this int @this) =>
         @this switch
         {
            CaplParser.Void => Void,
            CaplParser.Int => Int,
            CaplParser.Float => Float,
            CaplParser.Char => Char,
            CaplParser.Byte => Byte,
            CaplParser.Long => Long,
            CaplParser.Int64 => Int64,
            CaplParser.Double => BuiltInType.Double,
            CaplParser.Word => BuiltInType.Word,
            CaplParser.Dword => BuiltInType.DWord,
            CaplParser.Qword => BuiltInType.QWord,
            CaplParser.Struct => BuiltInType.Struct,
            CaplParser.Enum => BuiltInType.Enum,
            CaplParser.Timer => BuiltInType.Timer,
            _ => Invalid
         };
   }
}