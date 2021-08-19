/**
 * Корректная смена регистра
 */

using System;
using System.Globalization;
using System.Text;

namespace HowToCSharp.ch07.ChangeCase
{
   class Program
   {
      static void Main()
      {
         const string original = "file";
         Console.WriteLine("Original: {0}", original);

         string upperInvariant = original.ToUpperInvariant();
         string turkUpperCase = original.ToUpper(CultureInfo.CreateSpecificCulture("tr-TR"));

         Console.WriteLine("Uppercase (invariant): {0}", upperInvariant);
         Console.WriteLine("Uppercase (Turkish): {0}", turkUpperCase);

         byte[] bytesInvariant = Encoding.Unicode.GetBytes(upperInvariant);
         byte[] bytesTurkish = Encoding.Unicode.GetBytes(turkUpperCase);

         Console.WriteLine("Bytes (invariant): {0}", BitConverter.ToString(bytesInvariant));
         Console.WriteLine("Bytes (Turkish): {0}", BitConverter.ToString(bytesTurkish));

         Console.ReadKey();
      }
   }
}
