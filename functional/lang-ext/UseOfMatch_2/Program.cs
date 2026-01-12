using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace UseOfMatch_2;

// Shows the basics of transforming a list of Eithers using match to understand
// what in them (both left and right values) and
// then transform them based on their values into a single type that represents
// either in one way (a string)
// Along with Map() and Bind() this extracts the value from the Either and
// provides transformation functions for both Left and Right sides of the Either
internal static class Program
{
   private static void Main()
   {
      Either<int, string> intOrString1 = "Stuart";
      Either<int, string> intOrString2 = "Jenny";
      Either<int, string> intOrString3 = "Bruce";
      Either<int, string> intOrString4 = "zxcmbasdjkfkejrfg";
      Either<int, string> intOrString5 = 66;
      Either<int, string> intOrString6 = 234;

      IEnumerable<Either<int, string>> listOfEithers =
      [
         intOrString1, intOrString2, intOrString3, intOrString4, intOrString5, intOrString6
      ];

      // Go through each of the Eithers and depending on their types and their content, transform them.
      // We can represent values from either type, string or int as one uniform type,
      // in this case we can represent a left and a right as a string and
      // thus return the set of all these representation as a list of strings
      // Note that in order to assign the result of a match, both sides' transformation function needs
      // to return the same type, particularly the type of the receiving variable
      var result = listOfEithers.Match(MakeGenderAwareString, MakeOneHundredAwareString);
      foreach (var transform in result)
      {
         Console.WriteLine(transform);
      }
   }

   private static string MakeOneHundredAwareString(int leftInteger) =>
      leftInteger > 100
         ? $"{leftInteger} is greater than 100"
         : $"{leftInteger} is less tan 100";

   private static string MakeGenderAwareString(string rightString)
   {
      var boysNames = new[] { "Stuart", "Bruce" };
      var girlsNames = new[] { "Jenny" };
      return boysNames.Contains(rightString)
         ? $"{rightString} is a Boys name"
         : girlsNames.Contains(rightString)
            ? $"{rightString} is a Girls name"
            : $"i dont know if {rightString} not registered with me as a boys name or a girls name";
   }
}