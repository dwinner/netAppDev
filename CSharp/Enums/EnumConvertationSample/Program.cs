using System;
using System.ComponentModel;
using System.Linq;

namespace EnumConvertationSample
{
   internal static class Program
   {
      private static void Main()
      {
         const ClickMode clickMode = ClickMode.Hover;
         var comClickMode = EnumConvert<ClickMode, ComClickMode>(clickMode);
         Console.WriteLine(comClickMode);
      }

      private static TOutEnum EnumConvert<TInEnum, TOutEnum>(TInEnum inEnum)
         where TInEnum : struct
         where TOutEnum : struct
      {
         if (!typeof(TInEnum).IsEnum)
         {
            throw new ArgumentException($"{nameof(TInEnum)} isn't enum type", nameof(TInEnum));
         }

         if (!typeof(TOutEnum).IsEnum)
         {
            throw new ArgumentException($"{nameof(TOutEnum)} isn't enum type", nameof(TOutEnum));
         }

         var inEnumValues = Enum.GetValues(typeof(TInEnum)).Cast<TInEnum>().Cast<int>().ToArray();
         var inEnumNames = Enum.GetNames(typeof(TInEnum));
         var foundIndex = -1;
         for (var i = 0; i < inEnumNames.Length; i++)
            if (string.Equals(inEnum.ToString(), inEnumNames[i]))
            {
               foundIndex = i;
               break;
            }

         if (foundIndex != -1 && Enum.IsDefined(typeof(TOutEnum), inEnumValues[foundIndex]))
         {
            return (TOutEnum) Enum.ToObject(typeof(TOutEnum), inEnumValues[foundIndex]);
         }

         throw new InvalidEnumArgumentException(nameof(inEnum));
      }
   }
}