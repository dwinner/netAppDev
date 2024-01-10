// From C# 12, you can use square brackets when initializing a array.
// This is called a collection expression:

using System.Collections.Immutable;

namespace CollectionExprSample
{
   internal class Program
   {
      static void Main(string[] args)
      {
         int[] array = [1, 2, 3];
         Console.WriteLine(array);

         // Collection expressions also work with other collection types:

         List<int> list = [1, 2, 3];
         Console.WriteLine(list);

         Span<int> span = [1, 2, 3];
         Console.WriteLine(span.ToString());

         HashSet<int> set = [1, 2, 3];
         Console.WriteLine(set);

         ImmutableArray<int> immutable = [1, 2, 3];
         Console.WriteLine(immutable);

         // A collection expression can include other collections if prefixed by the .. (spread) operator:

         string[] primaryLightColors = ["Red", "Green", "Blue"];
         List<string> primaryPigments = ["Magenta", "Cyan", "Yellow"];

         HashSet<string> allColors = [.. primaryLightColors, .. primaryPigments, "Black", "White"];
         Console.WriteLine(allColors);
      }
   }
}
