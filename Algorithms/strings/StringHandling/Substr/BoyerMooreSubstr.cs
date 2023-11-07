namespace StringHandling.Substr;

/// <summary>
///    The <see cref="BoyerMooreSubstr" /> class finds the 1st occurrence of a pattern string
///    in a text string.
///    <p>
///       This implementation uses the Boyer-Moore algorithm (with the bad-character
///       rule, but not the strong good suffix rule).
///    </p>
/// </summary>
public sealed class BoyerMooreSubstr
{
   private const int DefaultRadix = 0x100;
   private readonly char[] _pattern; // store the pattern as a character array
   private readonly int[] _skipRightArray; // the bad-character skip array

   public BoyerMooreSubstr(char[] pattern, int radix = DefaultRadix)
   {
      if (pattern == null || pattern.Length == 0)
      {
         throw new ArgumentException($"Array '{nameof(pattern)}' is null or empty", nameof(pattern));
      }

      _pattern = new char[pattern.Length];
      Array.Copy(pattern, _pattern, pattern.Length);

      // position of rightmost occurrence of c in the pattern
      _skipRightArray = new int[radix];
      Array.Fill(_skipRightArray, -1);
      for (var i = 0; i < _pattern.Length; i++)
      {
         var charAt = _pattern[i];
         _skipRightArray[charAt] = i;
      }
   }

   public BoyerMooreSubstr(string pattern, int radix = DefaultRadix)
      : this(pattern.ToCharArray(), radix)
   {
   }

   public int Search(string text) => Search(text.ToCharArray());

   /// <summary>
   ///    Returns the index of the first occurrence of the pattern string in the text string.
   /// </summary>
   /// <param name="text">The text string</param>
   /// <returns>
   ///    The index of the first occurrence of the pattern string in the text string;
   ///    length of the whole text if no such match
   /// </returns>
   public int Search(char[] text)
   {
      const int notFoundVal = -1;
      var patternLen = _pattern.Length;
      var textLen = text.Length;
      int skip;
      for (var i = 0; i <= textLen - patternLen; i += skip)
      {
         skip = 0;
         for (var j = patternLen - 1; j >= 0; j--)
         {
            var charAt = text[i + j];
            if (_pattern[j] != charAt)
            {
               var skipValue = _skipRightArray[charAt];
               skip = Math.Max(1, j - skipValue);
               break;
            }
         }

         if (skip == 0)
         {
            return i; // found
         }
      }

      return notFoundVal; // not found
   }
}