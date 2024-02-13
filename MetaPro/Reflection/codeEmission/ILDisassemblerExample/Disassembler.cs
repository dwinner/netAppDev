#nullable disable

using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ILDisassemblerExample;

public class Disassembler
{
   private static readonly Dictionary<short, OpCode> Opcodes = new();
   private readonly byte[] _il; // The raw byte code
   private readonly Module _module; // This will come in handy later

   private StringBuilder _output; // The result to which we'll keep appending
   private int _pos; // The position we're up to in the byte code

   static Disassembler()
   {
      var opcodes = new Dictionary<short, OpCode>();
      foreach (var fi in typeof(OpCodes).GetFields
                  (BindingFlags.Public | BindingFlags.Static))
      {
         if (typeof(OpCode).IsAssignableFrom(fi.FieldType))
         {
            var code = (OpCode)fi.GetValue(null); // Get field's value
            if (code.OpCodeType != OpCodeType.Nternal)
            {
               Opcodes.Add(code.Value, code);
            }
         }
      }
   }

   private Disassembler(MethodBase method)
   {
      _module = method.DeclaringType.Module;
      _il = method.GetMethodBody().GetILAsByteArray();
   }

   public static string Disassemble(MethodBase method)
      => new Disassembler(method).Dis();

   private string Dis()
   {
      _output = new StringBuilder();
      while (_pos < _il.Length)
      {
         DisassembleNextInstruction();
      }

      return _output.ToString();
   }

   private void DisassembleNextInstruction()
   {
      var opStart = _pos;

      var code = ReadOpCode();
      var operand = ReadOperand(code);

      _output.AppendFormat("IL_{0:X4}:  {1,-12} {2}",
         opStart, code.Name, operand);
      _output.AppendLine();
   }

   private OpCode ReadOpCode()
   {
      var byteCode = _il[_pos++];
      if (Opcodes.ContainsKey(byteCode))
      {
         return Opcodes[byteCode];
      }

      if (_pos == _il.Length)
      {
         throw new Exception("Unexpected end of IL");
      }

      var shortCode = (short)(byteCode * 256 + _il[_pos++]);

      if (!Opcodes.ContainsKey(shortCode))
      {
         throw new Exception("Cannot find opcode " + shortCode);
      }

      return Opcodes[shortCode];
   }

   private string ReadOperand(OpCode c)
   {
      var operandLength =
         c.OperandType == OperandType.InlineNone
            ? 0
            : c.OperandType == OperandType.ShortInlineBrTarget ||
              c.OperandType == OperandType.ShortInlineI ||
              c.OperandType == OperandType.ShortInlineVar
               ? 1
               : c.OperandType == OperandType.InlineVar
                  ? 2
                  : c.OperandType == OperandType.InlineI8 ||
                    c.OperandType == OperandType.InlineR
                     ? 8
                     : c.OperandType == OperandType.InlineSwitch
                        ? 4 * (BitConverter.ToInt32(_il, _pos) + 1)
                        : 4; // All others are 4 bytes

      if (_pos + operandLength > _il.Length)
      {
         throw new Exception("Unexpected end of IL");
      }

      var result = FormatOperand(c, operandLength);
      if (result == null)
      { // Write out operand bytes in hex
         result = "";
         for (var i = 0; i < operandLength; i++)
         {
            result += _il[_pos + i].ToString("X2") + " ";
         }
      }

      _pos += operandLength;
      return result;
   }

   private string FormatOperand(OpCode c, int operandLength)
   {
      if (operandLength == 0)
      {
         return "";
      }

      if (operandLength == 4)
      {
         return Get4ByteOperand(c);
      }

      if (c.OperandType == OperandType.ShortInlineBrTarget)
      {
         return GetShortRelativeTarget();
      }

      if (c.OperandType == OperandType.InlineSwitch)
      {
         return GetSwitchTarget(operandLength);
      }

      return null;
   }

   private string Get4ByteOperand(OpCode c)
   {
      var intOp = BitConverter.ToInt32(_il, _pos);

      switch (c.OperandType)
      {
         case OperandType.InlineTok:
         case OperandType.InlineMethod:
         case OperandType.InlineField:
         case OperandType.InlineType:
            MemberInfo mi;
            try
            {
               mi = _module.ResolveMember(intOp);
            }
            catch
            {
               return null;
            }

            if (mi == null)
            {
               return null;
            }

            if (mi.ReflectedType != null)
            {
               return mi.ReflectedType.FullName + "." + mi.Name;
            }

            if (mi is Type)
            {
               return ((Type)mi).FullName;
            }

            return mi.Name;

         case OperandType.InlineString:
            var s = _module.ResolveString(intOp);
            if (s != null)
            {
               s = "'" + s + "'";
            }

            return s;

         case OperandType.InlineBrTarget:
            return "IL_" + (_pos + intOp + 4).ToString("X4");

         default:
            return null;
      }
   }

   private string GetShortRelativeTarget()
   {
      var absoluteTarget = _pos + (sbyte)_il[_pos] + 1;
      return "IL_" + absoluteTarget.ToString("X4");
   }

   private string GetSwitchTarget(int operandLength)
   {
      var targetCount = BitConverter.ToInt32(_il, _pos);
      var targets = new string[targetCount];
      for (var i = 0; i < targetCount; i++)
      {
         var ilTarget = BitConverter.ToInt32(_il, _pos + (i + 1) * 4);
         targets[i] = "IL_" + (_pos + ilTarget + operandLength).ToString("X4");
      }

      return "(" + string.Join(", ", targets) + ")";
   }
}