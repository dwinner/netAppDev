namespace MemoryOfT;

internal static class Program
{
   private static void Main()
   {
      var splitted = Split("Word1 Word2 Word3".AsMemory());
      foreach (var chunk in splitted)
      {
         Console.WriteLine(chunk);
      }
   }

   private static IEnumerable<ReadOnlyMemory<char>> Split(ReadOnlyMemory<char> input)
   {
      var wordStart = 0;
      for (var i = 0; i <= input.Length; i++)
      {
         if (i == input.Length || char.IsWhiteSpace(input.Span[i]))
         {
            yield return input[wordStart..i]; // Slice with C# range operator
            wordStart = i + 1;
         }
      }
   }
}