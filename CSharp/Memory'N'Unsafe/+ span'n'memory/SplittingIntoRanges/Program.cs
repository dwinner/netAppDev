namespace SplittingIntoRanges;

internal static class Program
{
   private static void Main()
   {
      ReadOnlySpan<char> source = "The quick brown fox";
      foreach (var range in Split(source))
      {
         var wordSpan = source[range];
         Console.WriteLine(wordSpan.ToString());
      }
   }

   private static IEnumerable<Range> Split(ReadOnlySpan<char> input)
   {
      var pos = 0;
      var list = new List<Range>();
      for (var i = 0; i <= input.Length; i++)
      {
         if (i == input.Length || char.IsWhiteSpace(input[i]))
         {
            list.Add(new Range(pos, i));
            pos = i + 1;
         }
      }

      return list.ToArray();
   }
}