namespace CharSpanSplitterSample;

internal static class Program
{
   private static void Main()
   {
      ReadOnlySpan<char> source = "The quick brown fox";
      foreach (var word in source.Split())
      {
         Console.WriteLine(word.ToString());
      }
   }
}