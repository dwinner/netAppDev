namespace SpanAndUnmanagedMemory;

public static class CharSpanExtensions
{
   public static CharSpanSplitter Split(this ReadOnlySpan<char> input) => new(input);

   public static CharSpanSplitter Split(this Span<char> input) => new(input);
}