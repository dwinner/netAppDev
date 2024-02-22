namespace CharSpanSplitterSample;

public static class CharSpanExtensions
{
   public static CharSpanSplitter Split(this ReadOnlySpan<char> input) => new(input);
}