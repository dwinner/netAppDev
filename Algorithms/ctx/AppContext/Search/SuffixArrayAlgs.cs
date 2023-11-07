namespace AppContext.Search;

public static class SuffixArrayAlgs
{
   /// <summary>
   ///    The KWIK provides a <see cref="SuffixArray" /> client for computing
   ///    all occurrences of a keyword in a given string, with surrounding context.
   ///    This is known as <em>keyword-in-context search</em>.
   /// </summary>
   /// <param name="text">Text to search</param>
   /// <param name="query">Keyword</param>
   /// <param name="contextLen">The length of context occurrence</param>
   /// <returns>The KWIK</returns>
   public static IEnumerable<string> GetKeywordInCtx(string text, string query, int contextLen)
   {
      // build suffix array
      var sfxArray = new SuffixArray(text);

      // find all occurrences of queries and give context
      var textLen = text.Length;
      for (var i = sfxArray.GetRank(query); i < textLen; i++)
      {
         var from1 = sfxArray[i];
         var to1 = Math.Min(textLen, from1 + query.Length);
         var currentFnd = text[from1..to1];
         if (query != currentFnd)
         {
            break;
         }

         var from2 = Math.Max(0, sfxArray[i] - contextLen);
         var to2 = Math.Min(textLen, sfxArray[i] + contextLen + query.Length);
         var currentKwik = text[from2..to2];

         yield return currentKwik;
      }
   }

   /// <summary>
   ///    Get the longest repeated substring
   /// </summary>
   /// <param name="text">Text</param>
   /// <returns>The longest repeated substring</returns>
   public static string GetLongestRepeatedSubstring(in string text)
   {
      var textLen = text.Length;
      var sfxArray = new SuffixArray(text);
      var lrs = string.Empty;
      for (var i = 1; i < textLen; i++)
      {
         var longestCommonPfx = sfxArray.GetLongestCommonPrefix(i);
         if (longestCommonPfx > lrs.Length)
         {
            lrs = text.Substring(sfxArray[i], longestCommonPfx);
         }
      }

      return lrs;
   }

   /// <summary>
   ///    Returns the longest common string of the two specified strings.
   /// </summary>
   /// <param name="str1">One string</param>
   /// <param name="str2">The other string</param>
   /// <returns>
   ///    the longest common string that appears as a substring in both {@code s} and {@code t}; the empty string if no
   ///    such string
   /// </returns>
   public static string GetLongestCommonString(in string str1, in string str2)
   {
      var suffix1 = new SuffixArray(str1);
      var suffix2 = new SuffixArray(str2);

      // find longest common substring by "merging" sorted suffixes
      var lcs = string.Empty;
      int i = 0, j = 0;
      while (i < str1.Length && j < str2.Length)
      {
         var idx1 = suffix1[i];
         var idx2 = suffix2[j];
         var lcPfx = LongestCommonPfx(str1, idx1, str2, idx2);
         if (lcPfx.Length > lcs.Length)
         {
            lcs = lcPfx;
         }

         if (LocalCompare(str1, idx1, str2, idx2) < 0)
         {
            i++;
         }
         else
         {
            j++;
         }
      }

      return lcs;

      // return the longest common prefix of suffix s[p..] and suffix t[q..]
      static string LongestCommonPfx(in string str1, int idx1, in string str2, int idx2)
      {
         var minLen = Math.Min(str1.Length - idx1, str2.Length - idx2);
         for (var i = 0; i < minLen; i++)
         {
            if (str1[idx1 + i] != str2[idx2 + i])
            {
               return str1.Substring(idx1, i);
            }
         }

         return str1.Substring(idx1, minLen);
      }

      // compare suffix s[p..] and suffix t[q..]
      static int LocalCompare(in string str1, int idx1, in string str2, int idx2)
      {
         var minLen = Math.Min(str1.Length - idx1, str2.Length - idx2);
         for (var i = 0; i < minLen; i++)
         {
            if (str1[idx1 + i] != str2[idx2 + i])
            {
               return str1[idx1 + i] - str2[idx2 + i];
            }
         }

         if (str1.Length - idx1 < str2.Length - idx2)
         {
            return -1;
         }

         if (str1.Length - idx1 > str2.Length - idx2)
         {
            return +1;
         }

         return 0;
      }
   }
}