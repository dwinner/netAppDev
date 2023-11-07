using System;
using Mono.Cecil.Cil;

namespace ILCodeWeaving
{
   public static class IlProcessorExtensions
   {
      public static Instruction CreateLoadInstruction(this ILProcessor self, object obj)
      {
         switch (obj)
         {
            case string value:
               return self.Create(OpCodes.Ldstr, value);
            case int value:
               return self.Create(OpCodes.Ldc_I4, value);
            default:
               throw new NotSupportedException();
         }
      }
   }
}