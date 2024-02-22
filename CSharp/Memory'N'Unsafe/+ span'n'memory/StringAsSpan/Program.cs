namespace StringAsSpan;

internal static class Program
{
   private static void Main()
   {
      var wsCount = CountWhitespace("Word1 Word2");
      Console.WriteLine(wsCount);
      wsCount = CountWhitespace("1 2 3 4 5".AsSpan(3, 3));
      Console.WriteLine(wsCount);

      var span = "This ".AsSpan(); // ReadOnlySpan<char>
      Console.WriteLine(span.StartsWith("This")); // True
      Console.WriteLine(span.Trim().Length); // 4
   }

   private static int CountWhitespace(ReadOnlySpan<char> str)
   {
      var count = 0;
      foreach (var chr in str)
      {
         if (char.IsWhiteSpace(chr))
         {
            count++;
         }
      }

      return count;
   }
}