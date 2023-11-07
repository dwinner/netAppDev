namespace AppContext.Search;

/// <summary>
///    The <see cref="SuffixArray" /> class represents a suffix array of a string of
///    length <em>n</em>.
///    It supports the <em>selecting</em> the <em>i</em>th smallest suffix,
///    getting the <em>index</em> of the <em>i</em>th smallest suffix,
///    computing the length of the <em>longest common prefix</em> between the
///    <em>i</em>th smallest suffix and the <em>i</em>-1st smallest suffix,
///    and determining the <em>rank</em> of a query string (which is the number
///    of suffixes strictly less than the query string).
///    <p>
///       This implementation uses a nested class Suffix to represent
///       a suffix of a string (using constant time and space) and
///       Arrays.sort() to sort the array of suffixes.
///       The <em>index</em> and <em>length</em> operations takes constant time
///       in the worst case. The <em>lcp</em> operation takes time proportional to the
///       length of the longest common prefix.
///       The <em>select</em> operation takes time proportional
///       to the length of the suffix and should be used primarily for debugging.
///    </p>
///    <p>
///       For alternate implementations of the same API, see
///       <seealso cref="SuffixArrayX" />, which is faster in practice (uses 3-way radix quicksort)
///       and uses less memory (does not create Suffix objects)
///    </p>
/// </summary>
public sealed class SuffixArray
{
   private readonly Suffix[] _suffixes;

   /// <summary>
   ///    Initializes a suffix array for the given <paramref name="text" /> string.
   /// </summary>
   /// <param name="text">The input string</param>
   public SuffixArray(in string text)
   {
      var textLen = text.Length;
      _suffixes = new Suffix[textLen];
      for (var i = 0; i < textLen; i++)
      {
         _suffixes[i] = new Suffix(text, i);
      }

      Array.Sort(_suffixes);
   }

   /// <summary>
   ///    The length of the input string
   /// </summary>
   public int Len => _suffixes.Length;

   /// <summary>
   ///    Returns the index into the original string of the <em>i</em>th smallest suffix.
   ///    That is, text.substring(sa.index(i)) is the <em>i</em>th smallest suffix.
   /// </summary>
   /// <param name="idx">An integer between 0 and <em>n</em>-1</param>
   /// <exception cref="ArgumentOutOfRangeException">Index is out of range</exception>
   public int this[int idx]
   {
      get
      {
         if (idx < 0 || idx >= _suffixes.Length)
         {
            throw new ArgumentOutOfRangeException(nameof(idx));
         }

         return _suffixes[idx].Index;
      }
   }

   /// <summary>
   ///    Returns the length of the longest common prefix of the <em>i</em>the
   ///    smallest suffix and the <em>i</em>-1st smallest suffix.
   /// </summary>
   /// <param name="idx">An integer between 1 and <em>n</em>-1</param>
   /// <returns>
   ///    The length of the longest common prefix of the <em>i</em>th smallest suffix and the <em>i</em>-1st smallest
   ///    suffix.
   /// </returns>
   /// <exception cref="ArgumentOutOfRangeException">Index is out of range</exception>
   public int GetLongestCommonPrefix(int idx)
   {
      if (idx < 1 || idx >= _suffixes.Length)
      {
         throw new ArgumentOutOfRangeException(nameof(idx));
      }

      return GetLcpSuffix(_suffixes[idx], _suffixes[idx - 1]);
   }

   /// <summary>
   ///    Returns the <em>i</em>th smallest suffix as a string.
   /// </summary>
   /// <param name="idx">The index</param>
   /// <returns>The <em>i</em> smallest suffix as a string</returns>
   /// <exception cref="ArgumentOutOfRangeException">Index is out of range</exception>
   public string Select(int idx)
   {
      if (idx < 0 || idx >= _suffixes.Length)
      {
         throw new ArgumentOutOfRangeException(nameof(idx));
      }

      return _suffixes[idx].ToString();
   }

   /// <summary>
   ///    Returns the number of suffixes strictly less than the <paramref name="query" /> string.
   /// </summary>
   /// <param name="query">The query string</param>
   /// <returns>The number of suffixes strictly less than <paramref name="query" /></returns>
   public int GetRank(string query)
   {
      int loIdx = 0, hiIdx = _suffixes.Length - 1;
      while (loIdx <= hiIdx)
      {
         var midIdx = loIdx + (hiIdx - loIdx) / 2;
         var cmp = Compare(query, _suffixes[midIdx]);
         switch (cmp)
         {
            case < 0:
               hiIdx = midIdx - 1;
               break;
            case > 0:
               loIdx = midIdx + 1;
               break;
            default:
               return midIdx;
         }
      }

      return loIdx;
   }

   // compare query string to suffix
   private static int Compare(string query, Suffix suffix)
   {
      var minLen = Math.Min(query.Length, suffix.Len);
      for (var i = 0; i < minLen; i++)
      {
         if (query[i] < suffix[i])
         {
            return -1;
         }

         if (query[i] > suffix[i])
         {
            return +1;
         }
      }

      return query.Length - suffix.Len;
   }

   // longest common prefix of 1St and 2Nd
   private static int GetLcpSuffix(Suffix suffix1, Suffix suffix2)
   {
      var minLen = Math.Min(suffix1.Len, suffix2.Len);
      for (var i = 0; i < minLen; i++)
      {
         if (suffix1[i] != suffix2[i])
         {
            return i;
         }
      }

      return minLen;
   }

   private sealed class Suffix : IComparable<Suffix>, IComparable
   {
      private static readonly Comparer<Suffix> DefaultComparer = Comparer<Suffix>.Default;
      private readonly string _text;

      public Suffix(string text, int index)
      {
         _text = text;
         Index = index;
      }

      public int Index { get; }

      public int Len => _text.Length - Index;

      public char this[int idx] => _text[Index + idx];

      public int CompareTo(object? obj)
      {
         if (ReferenceEquals(null, obj))
         {
            return 1;
         }

         if (ReferenceEquals(this, obj))
         {
            return 0;
         }

         return obj is Suffix other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(Suffix)}");
      }

      public int CompareTo(Suffix? other)
      {
         if (ReferenceEquals(this, other))
         {
            return 0;
         }

         if (ReferenceEquals(null, other))
         {
            return 1;
         }

         var minLen = Math.Min(Len, other.Len);
         for (var i = 0; i < minLen; i++)
         {
            if (this[i] < other[i])
            {
               return -1;
            }

            if (this[i] > other[i])
            {
               return +1;
            }
         }

         return Len - other.Len;
      }

      public override string ToString() => _text[Index..];

      public static bool operator <(Suffix? left, Suffix? right) =>
         DefaultComparer.Compare(left, right) < 0;

      public static bool operator >(Suffix? left, Suffix? right) =>
         DefaultComparer.Compare(left, right) > 0;

      public static bool operator <=(Suffix? left, Suffix? right) =>
         DefaultComparer.Compare(left, right) <= 0;

      public static bool operator >=(Suffix? left, Suffix? right) =>
         DefaultComparer.Compare(left, right) >= 0;
   }
}