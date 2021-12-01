using System;
using System.Linq;

namespace ChainViaEnums
{
   public static class EnumUtils
   {
      public static TEnum GetRandomEnumValue<TEnum>()
      {
         if (!typeof(TEnum).IsEnum)
         {
            throw new ArgumentException($"{typeof(TEnum)} is not a enum", nameof(TEnum));
         }

         var random = new Random();
         var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
         var rndIdx = random.Next(enumValues.Length);

         return enumValues[rndIdx];
      }
   }
}