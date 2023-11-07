namespace AppContext.Search;

/// <summary>
///    The <see cref="SuffixArrayX" /> class represents a suffix array of a string of
///    length <em>n</em>.
///    It supports the <em>selecting</em> the <em>i</em>th smallest suffix,
///    getting the <em>index</em> of the <em>i</em>th smallest suffix,
///    computing the length of the <em>longest common prefix</em> between the
///    <em>i</em>th smallest suffix and the <em>i</em>-1st smallest suffix,
///    and determining the <em>rank</em> of a query string (which is the number
///    of suffixes strictly less than the query string).
///    <p>
///       This implementation uses 3-way radix quicksort to sort the array of suffixes.
///       For a simpler (but less efficient) implementations of the same API, see
///       <seealso cref="SuffixArray" />.
///       The <em>index</em> and <em>length</em> operations takes constant time
///       in the worst case. The <em>lcp</em> operation takes time proportional to the
///       length of the longest common prefix.
///       The <em>select</em> operation takes time proportional
///       to the length of the suffix and should be used primarily for debugging.
///    </p>
///    <p>
///       This implementation uses '\0' as a sentinel and assumes that the character
///       '\0' does not appear in the text.
///    </p>
///    <p>
///       In practice, this algorithm runs very fast. However, in the worst-case
///       it can be very poor (e.g., a string consisting of N copies of the same
///       character). We do not shuffle the array of suffixes before sorting because
///       shuffling is relatively expensive and a pathological input for which
///       the suffixes start out in a bad order (e.g., sorted) is likely to be
///       a bad input for this algorithm with or without the shuffle.
///    </p>
/// </summary>
public sealed class SuffixArrayX
{
   private const int CutOff = 5; // cutoff to insertion sort (any value between 0 and 12)
   private readonly int[] _index; // index[i] = j means text.substring(j) is ith largest suffix
   private readonly char[] _text;

   /// <summary>
   ///    Initializes a suffix array for the given <paramref name="text" /> string.
   /// </summary>
   /// <param name="text">The input string</param>
   public SuffixArrayX(in string text)
   {
      Len = text.Length;
      _text = (text + '\0').ToCharArray();
      _index = new int[Len];
      for (var i = 0; i < Len; i++)
      {
         _index[i] = i;
      }

      Sort(0, Len - 1, 0);
   }

   /// <summary>
   ///    The length of the input string.
   /// </summary>
   public int Len { get; }

   /// <summary>
   ///    Returns the index into the original string of the <em>i</em>th smallest suffix.
   ///    That is, text.substring(sa.index(i)) is the <em>i</em> smallest suffix.
   /// </summary>
   /// <param name="idx">An integer between 0 and <em>n</em>-1</param>
   /// <returns>The index into the original string of the <em>i</em>th smallest suffix</returns>
   /// <exception cref="ArgumentOutOfRangeException"><paramref name="idx" /> is out of range</exception>
   public int this[int idx]
   {
      get
      {
         if (idx < 0 || idx >= Len)
         {
            throw new ArgumentOutOfRangeException(nameof(idx));
         }

         return _index[idx];
      }
   }

   /// <summary>
   ///    Returns the number of suffixes strictly less than the <paramref name="query" /> string.
   ///    We note that rank(select(i)) equals i for each i
   ///    between 0 and <em>n</em>-1.
   /// </summary>
   /// <param name="query">The query string</param>
   /// <returns>The number of suffixes strictly less than <paramref name="query" /></returns>
   public int GetRank(string query)
   {
      int lo = 0, hi = Len - 1;
      while (lo <= hi)
      {
         var mid = lo + (hi - lo) / 2;
         var cmp = Compare(query, _index[mid]);
         switch (cmp)
         {
            case < 0:
               hi = mid - 1;
               break;
            case > 0:
               lo = mid + 1;
               break;
            default:
               return mid;
         }
      }

      return lo;
   }

   /// <summary>
   ///    Returns the <em>i</em>th smallest suffix as a string.
   /// </summary>
   /// <param name="idx">The index</param>
   /// <returns>The <em>i</em> smallest suffix as a string</returns>
   /// <exception cref="ArgumentOutOfRangeException"><paramref name="idx" /> is out of range</exception>
   public string Select(int idx)
   {
      if (idx < 0 || idx >= Len)
      {
         throw new ArgumentOutOfRangeException(nameof(idx));
      }

      return new string(_text, _index[idx], Len - _index[idx]);
   }

   /// <summary>
   ///    Returns the length of the longest common prefix of the <em>i</em>th smallest suffix and the <em>i</em>-1st smallest
   ///    suffix.
   /// </summary>
   /// <param name="idx1">An integer between 1 and <em>n</em>-1</param>
   /// <returns>
   ///    The length of the longest common prefix of the <em>i</em>th smallest suffix and the <em>i</em>-1st smallest
   ///    suffix.
   /// </returns>
   /// <exception cref="ArgumentOutOfRangeException"><paramref name="idx1" /> is out of range</exception>
   public int GetLongestCommonPrefix(int idx1)
   {
      if (idx1 < 1 || idx1 >= Len)
      {
         throw new ArgumentOutOfRangeException(nameof(idx1));
      }

      return GetLongestCommonPrefix(_index[idx1], _index[idx1 - 1]);
   }

   // exchange index[i] and index[j]
   private void Exchange(int i, int j) =>
      (_index[i], _index[j]) = (_index[j], _index[i]);

   // is text[i+d..n) < text[j+d..n) ?
   private bool Less(int idx1, int idx2, int idx)
   {
      if (idx1 == idx2)
      {
         return false;
      }

      idx1 += idx;
      idx2 += idx;
      while (idx1 < Len && idx2 < Len)
      {
         if (_text[idx1] < _text[idx2])
         {
            return true;
         }

         if (_text[idx1] > _text[idx2])
         {
            return false;
         }

         idx1++;
         idx2++;
      }

      return idx1 > idx2;
   }

   // sort from a[lo] to a[hi], starting at the dth character
   private void Insertion(int loIdx, int hiIdx, int idx)
   {
      for (var i = loIdx; i <= hiIdx; i++)
      {
         for (var j = i;
              j > loIdx && Less(_index[j], _index[j - 1], idx);
              j--)
         {
            Exchange(j, j - 1);
         }
      }
   }

   // longest common prefix of text[i..n) and text[j..n)
   private int GetLongestCommonPrefix(int idx1, int idx2)
   {
      var length = 0;
      while (idx1 < Len && idx2 < Len)
      {
         if (_text[idx1] != _text[idx2])
         {
            return length;
         }

         idx1++;
         idx2++;
         length++;
      }

      return length;
   }

   // is query < text[i..n) ?
   private int Compare(string query, int idx)
   {
      var qLen = query.Length;
      var j = 0;
      while (idx < Len && j < qLen)
      {
         if (query[j] != _text[idx])
         {
            return query[j] - _text[idx];
         }

         idx++;
         j++;
      }

      if (idx < Len)
      {
         return -1;
      }

      if (j < qLen)
      {
         return +1;
      }

      return 0;
   }

   // 3-way string quicksort lo..hi starting at dth character
   private void Sort(int loIdx, int hiIdx, int idx)
   {
      // cutoff to insertion sort for small subarrays
      if (hiIdx <= loIdx + CutOff)
      {
         Insertion(loIdx, hiIdx, idx);
         return;
      }

      int lt = loIdx, gt = hiIdx;
      var charAt = _text[_index[loIdx] + idx];
      var currentIdx = loIdx + 1;
      while (currentIdx <= gt)
      {
         var currentChar = _text[_index[currentIdx] + idx];
         if (currentChar < charAt)
         {
            Exchange(lt++, currentIdx++);
         }
         else if (currentChar > charAt)
         {
            Exchange(currentIdx, gt--);
         }
         else
         {
            currentIdx++;
         }
      }

      // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi].
      Sort(loIdx, lt - 1, idx);
      if (charAt > 0)
      {
         Sort(lt, gt, idx + 1);
      }

      Sort(gt + 1, hiIdx, idx);
   }
}