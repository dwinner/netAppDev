namespace StringHandling.Substr;

/// <summary>
///    The <see cref="KnuthMorrisPrattSubstr" /> class finds the first occurrence of a pattern string
///    in a text string.
///    <p>
///       This implementation uses a version of the Knuth-Morris-Pratt substring search
///       algorithm. The version takes time proportional to <em>n</em> + <em>m R</em>
///       in the worst case, where <em>n</em> is the length of the text string,
///       <em>m</em> is the length of the pattern, and <em>R</em> is the alphabet size.
///       It uses extra space proportional to <em>m R</em>.
///    </p>
/// </summary>
public sealed class KnuthMorrisPrattSubstr
{
   private const int DefaultRadix = 0x100;
   private readonly int[][] _dfa; // // the KMP automaton
   private readonly int _patternLen; // length of pattern

   public KnuthMorrisPrattSubstr(string pattern, int radix = DefaultRadix)
      : this(pattern.ToCharArray(), radix)
   {
   }

   public KnuthMorrisPrattSubstr(IReadOnlyList<char> pattern, int radix = DefaultRadix)
   {
      _patternLen = pattern.Count;

      // build DFA from pattern
      _dfa = new int[radix][];
      for (var i = 0; i < radix; i++)
      {
         _dfa[i] = new int[_patternLen];
      }

      var firstChar = pattern[0];
      _dfa[firstChar][0] = 1;
      for (int idx = 0, j = 1; j < _patternLen; j++)
      {
         for (var chrIdx = 0; chrIdx < radix; chrIdx++)
         {
            _dfa[chrIdx][j] = _dfa[chrIdx][idx]; // Copy mismatch cases.
         }

         var charAt = pattern[j];
         _dfa[charAt][j] = j + 1; // Set match case.
         idx = _dfa[charAt][idx]; // Update restart state.
      }
   }

   /// <summary>
   ///    Returns the index of the first occurrence of the pattern string in the text string.
   /// </summary>
   /// <param name="text">The text string</param>
   /// <returns>
   ///    The index of the first occurrence of the pattern string in the text string;
   ///    N if no such match
   /// </returns>
   public int Search(string text) => Search(text.ToCharArray());

   /// <summary>
   ///    Returns the index of the first occurrence of the pattern string in the text string.
   /// </summary>
   /// <param name="text">The text string</param>
   /// <returns>
   ///    The index of the first occurrence of the pattern string in the text string;
   ///    N if no such match
   /// </returns>
   public int Search(char[] text)
   {
      const int notFoundVal = -1;

      // simulate operation of DFA on text
      var len = text.Length;
      int i, j;
      for (i = 0, j = 0; i < len && j < _patternLen; i++)
      {
         var charAt = text[i];
         j = _dfa[charAt][j];
      }

      return j == _patternLen
         ? i - _patternLen // found
         : notFoundVal; // not found
   }
}