/**
 * Извлечение фрагментов текста.
 */

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExtractGroups
{
   class Program
   {
      static void Main()
      {
         string file =
                     "1234 Cherry Lane, Smalltown, USA" + Environment.NewLine +
                     "1235 Apple Tree Drive, Smalltown, USA" + Environment.NewLine +
                     "3456 Cherry Orchard Street, Smalltown, USA" + Environment.NewLine;

         var regex =
            new Regex("^(?<HouseNumber>\\d+)\\s*(?<Street>[\\w\\s]*), (?<City>[\\w]+), (?<Country>[\\w\\s]+)$",
               RegexOptions.Multiline);
         MatchCollection matchCollection = regex.Matches(file);
         foreach (var street in
            from Match match in matchCollection select match.Groups["Street"].Value)  // Извлечь именованную группу Street
         {
            Console.WriteLine("Street: {0}", street);
         }

         Console.ReadKey();

      }
   }
}
