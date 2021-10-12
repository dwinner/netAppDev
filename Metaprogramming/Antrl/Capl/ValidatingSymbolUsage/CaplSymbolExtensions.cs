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
            CaplParser.Double => Double,
            CaplParser.Word => Word,
            CaplParser.Dword => DWord,
            CaplParser.Qword => QWord,
            CaplParser.Struct => Struct,
            CaplParser.Enum => Enum,
            CaplParser.Timer => Timer,
            CaplParser.MsTimer => MsTimer,
            CaplParser.Message => Message,
            CaplParser.MultiplexedMessage => MultiplexedMessage,
            CaplParser.DiagRequest => DiagRequest,
            CaplParser.DiagResponse => DiagResponse,
            CaplParser.Signal => Signal,
            _ => Invalid
         };
   }
}