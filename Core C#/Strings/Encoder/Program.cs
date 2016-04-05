/**
 * Кодировщик строк, использующий класс System.String
 */

using System;

namespace Encoder
{
   static class Program
   {
      static void Main()
      {
         string greetingText = "Hello from all the guys at Wrox Press. ";
         greetingText += "We do hope you enjoy this book as much as we enjoyed writing it.";

         for (var i = (int)'z'; i >= (int)'a'; i--)
         {
            var oldChar = (char)i;
            var newChar = (char)(i + 1);
            greetingText = greetingText.Replace(oldChar, newChar);
         }

         for (var i = (int)'Z'; i >= (int)'A'; i--)
         {
            var oldChar = (char)i;
            var newChar = (char)(i + 1);
            greetingText = greetingText.Replace(oldChar, newChar);
         }

         Console.WriteLine("Encoded:\n{0}", greetingText);
         Console.ReadLine();
      }
   }
}
