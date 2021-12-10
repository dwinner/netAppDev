using CaplGrammar.Core;

namespace ValidatingSymbolUsage
{
   public static class CaplSymbolExtensions
   {
      // TODO: Add other type there
      public static BuiltInType GetCaplType(this int @this)
      {
         switch (@this)
         {
            case CaplParser.Void: return BuiltInType.Void;
            case CaplParser.Int: return BuiltInType.Int;
            case CaplParser.Float: return BuiltInType.Float;
            case CaplParser.Char: return BuiltInType.Char;
            case CaplParser.Byte: return BuiltInType.Byte;
            case CaplParser.Long: return BuiltInType.Long;
            case CaplParser.Int64: return BuiltInType.Int64;
            case CaplParser.Double: return BuiltInType.Double;
            case CaplParser.Word: return BuiltInType.Word;
            case CaplParser.Dword: return BuiltInType.DWord;
            case CaplParser.Qword: return BuiltInType.QWord;
            case CaplParser.Struct: return BuiltInType.Struct;
            case CaplParser.Enum: return BuiltInType.Enum;
            case CaplParser.Timer: return BuiltInType.Timer;
            case CaplParser.MsTimer: return BuiltInType.MsTimer;
            case CaplParser.Message: return BuiltInType.Message;
            case CaplParser.MultiplexedMessage: return BuiltInType.MultiplexedMessage;
            case CaplParser.DiagRequest: return BuiltInType.DiagRequest;
            case CaplParser.DiagResponse: return BuiltInType.DiagResponse;
            case CaplParser.Signal: return BuiltInType.Signal;
            default: return BuiltInType.Invalid;
         }
      }
   }
}