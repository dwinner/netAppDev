namespace CharSpanSplitterSample;

public readonly ref struct CharSpanSplitter
{
   private readonly ReadOnlySpan<char> _input;
   public CharSpanSplitter(ReadOnlySpan<char> input) => _input = input;
   public Enumerator GetEnumerator() => new(_input);

   // Forward-only enumerator
   public ref struct Enumerator
   {
      private readonly ReadOnlySpan<char> _input;
      private int _wordPos;
      public ReadOnlySpan<char> Current { get; private set; }

      public Enumerator(ReadOnlySpan<char> input)
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