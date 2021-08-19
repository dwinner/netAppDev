/**
 * Преобразование строки в байтовое представление (и обратно)
 */

using System;
using System.Text;

namespace HowToCSharp.ch07.StringsAndBytes
{
   class Program
   {
      static void Main(string[] args)
      {
         string myString = "C# Rocks!";

         Console.WriteLine("Original string: {0}", myString);
         //ASCII
         byte[] bytes = Encoding.ASCII.GetBytes(myString);
         Console.WriteLine("ASCII bytes: {0}", BitConverter.ToString(bytes));

         //Unicode
         bytes = Encoding.Unicode.GetBytes(myString);
         Console.WriteLine("Unicode bytes: {0}", BitConverter.ToString(bytes));

         //UTF32
         bytes = Encoding.UTF32.GetBytes(myString);
         Console.WriteLine("UTF32 bytes: {0}", BitConverter.ToString(bytes));

         //round-trip: string --> ASCII bytes --> string
         bytes = Encoding.ASCII.GetBytes(myString);
         string result = Encoding.ASCII.GetString(bytes);
         Console.WriteLine("Round trip: {0}->{1}->{2}", myString, BitConverter.ToString(bytes), result);

         myString = "C# Rocks!♫";
         Console.WriteLine("With Unicode-only characters: {0}", myString);

         //ASCII
         bytes = Encoding.ASCII.GetBytes(myString);
         Console.WriteLine("ASCII bytes: {0}", BitConverter.ToString(bytes));

         //Unicode
         bytes = Encoding.Unicode.GetBytes(myString);
         Console.WriteLine("Unicode bytes: {0}", BitConverter.ToString(bytes));

         //UTF32
         bytes = Encoding.UTF32.GetBytes(myString);
         Console.WriteLine("UTF32 bytes: {0}", BitConverter.ToString(bytes));

         //round-trip: string --> ASCII bytes --> string
         bytes = Encoding.ASCII.GetBytes(myString);
         result = Encoding.ASCII.GetString(bytes);
         Console.WriteLine("Round trip: {0}->{1}->{2}", myString, BitConverter.ToString(bytes), result);
         //round-trip: string --> Unicode bytes --> string
         bytes = Encoding.Unicode.GetBytes(myString);
         result = Encoding.Unicode.GetString(bytes);
         Console.WriteLine("Round trip: {0}->{1}->{2}", myString, BitConverter.ToString(bytes), result);


         Console.ReadKey();
      }
   }
}
