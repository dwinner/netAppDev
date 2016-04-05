/**
 * Массив, размещенный в стеке
 */

using System;

namespace ArrayInStack
{
   internal static class Program
   {
      private static void Main()
      {
         StackAllocDemo();
         InlineArrayDemo();
      }

      private static unsafe void StackAllocDemo()
      {
         const int width = 20;
         char* pc = stackalloc char[width]; // В стеке выделяется память под массив
         const string s = "Jeffrey Richter";
         for (var index = 0; index < width; index++)
         {
            pc[width - index - 1] = index < s.Length ? s[index] : '.';
         }

         Console.WriteLine(new string(pc, 0, width));
      }

      private static unsafe void InlineArrayDemo()
      {
         CharArray ca; // В стеке выделяется память под массив
         var widthInBytes = sizeof(CharArray);
         var width = widthInBytes / 2;
         const string s = "Jeffrey Richter";
         for (var index = 0; index < width; index++)
         {
            ca.Characters[width - index - 1] = index < s.Length ? s[index] : '.';
         }

         Console.WriteLine(new string(ca.Characters, 0, width));
      }
   }

   internal unsafe struct CharArray
   {
#pragma warning disable 649
      public fixed char Characters[20]; // Этот массив встраивается в структуру
#pragma warning restore 649
   }
}