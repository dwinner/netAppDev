using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace AddingMethodsToEnums
{
   internal static class FileAttributesExtensionMethods
   {
      public static bool IsSet(this FileAttributes flags, FileAttributes flagToTest)
      {
         Contract.Requires(flagToTest != 0);
         return (flags & flagToTest) == flagToTest;
      }

      public static bool IsClear(this FileAttributes flags, FileAttributes flagToTest)
      {
         Contract.Requires(flagToTest != 0);
         return !IsSet(flags, flagToTest);
      }

      public static bool AnyFlagsSet(this FileAttributes flags, FileAttributes testFlags) => (flags & testFlags) != 0;

      public static FileAttributes Set(this FileAttributes flags, FileAttributes setFlags) => flags | setFlags;

      public static FileAttributes Clear(this FileAttributes flags, FileAttributes clearFlags) => flags & ~clearFlags;

      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      public static void ForEach(this FileAttributes flags, Action<FileAttributes> processFlag)
      {
         Contract.Requires(processFlag != null);
         for (var bit = 1u; bit != 0; bit <<= 1)
         {
            var temp = (uint)flags & bit;
            if (temp != 0)
            {
               processFlag((FileAttributes)temp);
            }
         }
      }
   }
}