using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SimpleILDisassemblerSample
{
   public class IlDisassembler
   {
      private static readonly Dictionary<short, OpCode> _OpCodes =
      (from fieldInfo in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
         where typeof(OpCode).IsAssignableFrom(fieldInfo.FieldType)
         select (OpCode) fieldInfo.GetValue(null)
         into code
         where code.OpCodeType != OpCodeType.Nternal
         select code).ToDictionary(code => code.Value);

      private readonly byte[] _il;
      private readonly Module _module;
      private StringBuilder _output;
      private int _pos;

      private IlDisassembler(MethodBase method)
      {
         _module = method.DeclaringType?.Module;
         _il = method.GetMethodBody()?.GetILAsByteArray();
      }

      public static string Disassemble(MethodBase method) => new IlDisassembler(method).Dis();

      private string Dis()
      {
         _output = new StringBuilder();
         while (_pos < _il.Length)
            DisassembleNextInstruction();

         return _output.ToString();
      }

      private void DisassembleNextInstruction()
      {
         var opStart = _pos;
         var code = ReadOpCode();
         var operand = ReadOperand(code);
         _output.AppendFormat("IL_{0:X4}:  {1,-12} {2}", opStart, code.Name, operand).AppendLine();
      }

      private string ReadOperand(OpCode code)
      {
         var operandLen = code.OperandType == OperandType.InlineNone
            ? 0
            : code.OperandType == OperandType.ShortInlineBrTarget
              || code.OperandType == OperandType.ShortInlineI
              || code.OperandType == OperandType.ShortInlineVar
               ? 1
               : code.OperandType == OperandType.InlineVar
                  ? 2
                  : code.OperandType == OperandType.InlineI8
                    || code.OperandType == OperandType.InlineR
                     ? 8
                     : code.OperandType == OperandType.InlineSwitch
                        ? 4 * (BitConverter.ToInt32(_il, _pos) + 1)
                        : 4;

         if (_pos + operandLen > _il.Length)
            throw new InvalidOperationException("Unexpected end of IL");

         var result = FormatOperand(code, operandLen);
         if (result == null)
         {
            result = string.Empty;
            for (var i = 0; i < operandLen; i++)
               result += $"{_il[_pos + i]:X2} ";
         }

         _pos += operandLen;
         return result;
      }

      private string FormatOperand(OpCode code, int operandLen)
      {
         switch (operandLen)
         {
            case 0:
               return string.Empty;
            case 4:
               return Get4ByteOperand(code);
         }

         switch (code.OperandType)
         {
            case OperandType.ShortInlineBrTarget:
               return GetShortRelativeTarget();
            case OperandType.InlineSwitch:
               return GetSwitchTarget(operandLen);
            default:
               return null;
         }
      }

      private string GetSwitchTarget(int operandLen)
      {
         var targetCount = BitConverter.ToInt32(_il, _pos);
         var targets = new string[targetCount];
         for (var i = 0; i < targetCount; i++)
         {
            var ilTarget = BitConverter.ToInt32(_il, _pos + (i + 1) * 4);
            targets[i] = $"IL_{_pos + ilTarget + operandLen:X4}";
         }

         return $"({string.Join(", ", targets)})";
      }

      private string GetShortRelativeTarget() => $"IL_{_pos + (sbyte) _il[_pos] + 1:X4}";

      private string Get4ByteOperand(OpCode code)
      {
         var intOp = BitConverter.ToInt32(_il, _pos);
         switch (code.OperandType)
         {
            case OperandType.InlineTok:
            case OperandType.InlineMethod:
            case OperandType.InlineField:
            case OperandType.InlineType:
               MemberInfo memberInfo;
               try
               {
                  memberInfo = _module.ResolveMember(intOp);
               }
               catch
               {
                  return null;
               }

               if (memberInfo == null)
                  return null;

               if (memberInfo.ReflectedType != null)
                  return $"{memberInfo.ReflectedType.FullName}.{memberInfo.Name}";
               else if (memberInfo is Type)
                  return ((Type) memberInfo).FullName;
               else
                  return memberInfo.Name;

            case OperandType.InlineString:
               return $"\"{_module.ResolveString(intOp)}\"";

            case OperandType.InlineBrTarget:
               return $"IL_{_pos + intOp + 4:X4}";

            default:
               return null;
         }
      }

      private OpCode ReadOpCode()
      {
         var byteCode = _il[_pos++];
         if (_OpCodes.ContainsKey(byteCode))
            return _OpCodes[byteCode];

         if (_pos == _il.Length)
            throw new InvalidOperationException($"Cannot find opcode {byteCode}");

         var shortCode = (short) (byteCode * 0x100 + _il[_pos++]);
         if (!_OpCodes.ContainsKey(shortCode))
            throw new InvalidOperationException($"Cannot find opcode {shortCode}");

         return _OpCodes[shortCode];
      }
   }
}