using System.Text;

namespace StringHandling.Adt;

/// <summary>
///    A data type for alphabets, for use with string-processing code
///    that must convert between an alphabet of size R and the integers
///    0 through R-1.
///    Warning: supports only the basic multilingual plane (BMP), i.e,
///    Unicode characters between U+0000 and U+FFFF.
/// </summary>
public sealed class Alphabet
{
   private const int DefaultRadix = 0x100;

   private static Alphabet? _binary;
   private static Alphabet? _octal;
   private static Alphabet? _decimal;
   private static Alphabet? _hexadecimal;
   private static Alphabet? _dna;
   private static Alphabet? _lowercase;
   private static Alphabet? _uppercase;
   private static Alphabet? _protein;
   private static Alphabet? _base64;
   private static Alphabet? _ascii;
   private static Alphabet? _extendedAscii;
   private static Alphabet? _unicode16;

   private readonly char[] _alphabetChars; // the characters in the alphabet
   private readonly int[] _inverseIndices; // indices

   /// <summary>
   ///    Initializes a new alphabet from the given set of characters.
   /// </summary>
   /// <param name="alpha">The set of characters</param>
   /// <exception cref="ArgumentException">If <paramref name="alpha" /> doesn't contain unique chars</exception>
   public Alphabet(string alpha)
   {
      // check that alphabet contains no duplicate chars
      var unicode = new bool[char.MaxValue];
      foreach (var @char in alpha)
      {
         if (unicode[@char])
         {
            throw new ArgumentException($"Illegal alphabet: repeated character = '{@char}'", nameof(alpha));
         }

         unicode[@char] = true;
      }

      _alphabetChars = alpha.ToCharArray();
      Radix = alpha.Length;
      _inverseIndices = new int[char.MaxValue];
      Array.Fill(_inverseIndices, -1);

      // can't use char since R can be as big as 65,536
      for (var charIndex = 0; charIndex < Radix; charIndex++)
      {
         var @char = _alphabetChars[charIndex];
         _inverseIndices[@char] = charIndex;
      }
   }

   /// <summary>
   ///    Initializes a new alphabet using characters 0 through R-1.
   /// </summary>
   /// <param name="radix">The number of characters in the alphabet (the radix R)</param>
   public Alphabet(int radix = DefaultRadix)
   {
      Radix = radix;
      _alphabetChars = new char[Radix];
      _inverseIndices = new int[Radix];

      // can't use char since R can be as big as 65,536
      for (var i = 0; i < Radix; i++)
      {
         _alphabetChars[i] = (char)(_inverseIndices[i] = i);
      }
   }

   public int Radix { get; }

   /// <summary>
   ///    The binary alphabet { 0, 1 }.
   /// </summary>
   public static Alphabet Binary => _binary ??= new Alphabet("01");

   /// <summary>
   ///    The octal alphabet { 0, 1, 2, 3, 4, 5, 6, 7 }.
   /// </summary>
   public static Alphabet Octal => _octal ??= new Alphabet("01234567");

   /// <summary>
   ///    The decimal alphabet { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.
   /// </summary>
   public static Alphabet Decimal => _decimal ??= new Alphabet("0123456789");

   /// <summary>
   ///    The hexadecimal alphabet { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A, B, C, D, E, F }.
   /// </summary>
   public static Alphabet Hexadecimal => _hexadecimal ??= new Alphabet("0123456789ABCDEF");

   /// <summary>
   ///    The DNA alphabet { A, C, T, G }.
   /// </summary>
   public static Alphabet Dna => _dna ??= new Alphabet("ACGT");

   /// <summary>
   ///    The lowercase alphabet { a, b, c, ..., z }.
   /// </summary>
   public static Alphabet Lowercase => _lowercase ??= new Alphabet("abcdefghijklmnopqrstuvwxyz");

   /// <summary>
   ///    The uppercase alphabet { A, B, C, ..., Z }.
   /// </summary>
   public static Alphabet Uppercase => _uppercase ??= new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

   /// <summary>
   ///    The protein alphabet { A, C, D, E, F, G, H, I, K, L, M, N, P, Q, R, S, T, V, W, Y }.
   /// </summary>
   public static Alphabet Protein => _protein ??= new Alphabet("ACDEFGHIKLMNPQRSTVWY");

   /// <summary>
   ///    The base-64 alphabet (64 characters).
   /// </summary>
   public static Alphabet Base64 =>
      _base64 ??= new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");

   /// <summary>
   ///    The ASCII alphabet (0-127).
   /// </summary>
   public static Alphabet Ascii => _ascii ??= new Alphabet(0x80);

   /// <summary>
   ///    The extended ASCII alphabet (0-255).
   /// </summary>
   public static Alphabet ExtendedAscii => _extendedAscii ??= new Alphabet();

   /// <summary>
   ///    The Unicode 16 alphabet (0-65,535).
   /// </summary>
   public static Alphabet Unicode16 => _unicode16 ??= new Alphabet(0x10000);

   /// <summary>
   ///    Returns true if the argument is a character in this alphabet.
   /// </summary>
   /// <param name="aChar">The character</param>
   /// <returns>true if <paramref name="aChar" /> is a character in this alphabet; false otherwise</returns>
   public bool Contains(char aChar) => _inverseIndices[aChar] != -1;

   /// <summary>
   ///    Returns the binary logarithm of the number of characters in this alphabet.
   /// </summary>
   /// <returns>The binary logarithm (rounded up) of the number of characters in this alphabet</returns>
   public int GetLgRadix()
   {
      var lgR = 0;
      for (var t = Radix - 1; t >= 1; t /= 2)
      {
         lgR++;
      }

      return lgR;
   }

   /// <summary>
   ///    Returns the index corresponding to the argument character.
   /// </summary>
   /// <param name="aChar">The character</param>
   /// <returns>The index corresponding to the character <paramref name="aChar" /></returns>
   /// <exception cref="ArgumentOutOfRangeException">Unless <paramref name="aChar" /> is a character in this alphabet</exception>
   public int ToIndex(char aChar)
   {
      if (aChar >= _inverseIndices.Length || _inverseIndices[aChar] == -1)
      {
         throw new ArgumentOutOfRangeException(nameof(aChar), $"Character {aChar} not in alphabet");
      }

      return _inverseIndices[aChar];
   }

   /// <summary>
   ///    Returns the indices corresponding to the argument characters.
   /// </summary>
   /// <param name="aString">The characters</param>
   /// <returns>The indices corresponding to the characters <paramref name="aString" /></returns>
   public int[] ToIndices(string aString)
   {
      var source = aString.ToCharArray();
      var target = new int[aString.Length];
      for (var i = 0; i < source.Length; i++)
      {
         var srcChar = source[i];
         target[i] = ToIndex(srcChar);
      }

      return target;
   }

   /// <summary>
   ///    Returns the character corresponding to the argument index.
   /// </summary>
   /// <param name="anIndex">The index</param>
   /// <returns>The character corresponding to the index <paramref name="anIndex" /></returns>
   /// <exception cref="ArgumentOutOfRangeException">unless 0 &#8804; <paramref name="anIndex" /> &lt; R</exception>
   public char ToChar(int anIndex)
   {
      if (anIndex < 0 || anIndex >= Radix)
      {
         throw new ArgumentOutOfRangeException(nameof(anIndex), $"Index must be between 0 and {Radix}: {anIndex}");
      }

      return _alphabetChars[anIndex];
   }

   /// <summary>
   ///    Returns the characters corresponding to the argument indices.
   /// </summary>
   /// <param name="indices">The indices</param>
   /// <returns>The characters corresponding to the indices <paramref name="indices" /></returns>
   public string ToChars(int[] indices)
   {
      var charsBuilder = new StringBuilder(indices.Length);
      foreach (var charIndex in indices)
      {
         charsBuilder.Append(ToChar(charIndex));
      }

      return charsBuilder.ToString();
   }
}