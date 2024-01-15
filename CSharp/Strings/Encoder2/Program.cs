/*
 * Кодировщик строк, использующий класс System.Text.StringBuilder
 */

using System;
using System.Text;

namespace Encoder2
{
   static class Program
   {
      static void Main()
      {
         var greetingBuilder = new StringBuilder("Hello from all the guys at Wrox Press. ", 150);
         greetingBuilder.Append("We do hope you enjoy this book as much as we enjoyed writing it");

         for (var i = (int)'z'; i >= (int)'a'; i--)
         {
            var oldChar = (char)i;
            var newChar = (char)(i + 1);
            greetingBuilder = greetingBuilder.Replace(oldChar, newChar);
         }

         for (var i = (int)'Z'; i >= (int)'A'; i--)
         {
            var oldChar = (char)i;
            var newChar = (char)(i + 1);
            greetingBuilder = greetingBuilder.Replace(oldChar, newChar);
         }

         Console.WriteLine("Encoded:\n" + greetingBuilder);
         Console.ReadLine();
      }
   }
}
