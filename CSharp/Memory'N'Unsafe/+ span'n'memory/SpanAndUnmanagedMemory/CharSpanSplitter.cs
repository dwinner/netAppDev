namespace SpanAndUnmanagedMemory;

public readonly ref struct CharSpanSplitter
{
   private readonly ReadOnlySpan<char> _input;
   public CharSpanSplitter(ReadOnlySpan<char> input) => _input = input;
   public Rator GetEnumerator() => new(_input);

   public ref struct Rator // Forward-only enumerator
   {
      private readonly ReadOnlySpan<char> _input;
      private int _wordPos;
      public ReadOnlySpan<char> Current { get; private set; }

      public Rator(ReadOnlySpan<char> input)
      {
         _input = input;
         _wordPos = 0;
         Current = default;
      }

      public bool MoveNext()
      {
         for (var i = _wordPos; i <= _input.Length; i++)
         {
            if (i == _input.Length || char.IsWhiteSpace(_input[i]))
            {
               Current = _input[_wordPos..i];
               _wordPos = i + 1;
               return true;
            }
         }

         return false;
      }
   }
}