using System.Diagnostics;

namespace StringHandling.Substr;

/// <summary>
///    The <see cref="RabinKarpSubstr" /> class finds the 1St occurrence of a pattern string
///    in a text string.
///    <p>This implementation uses the Rabin-Karp algorithm.</p>
/// </summary>
public sealed class RabinKarpSubstr
{
   /// <summary>
   ///    Approach variants for Agls design
   /// </summary>
   public enum AlgApproach
   {
      /// <summary>
      ///    Almost certainly right, fast always
      /// </summary>
      MonteCarlo,

      /// <summary>
      ///    Almost certainly fast, right always
      /// </summary>
      LasVegas
   }

   private const int DefaultRadix = 0x100;

   private static readonly int[] FirstPrimes =
   {
      2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
      31, 37, 41, 43, 47, 53, 59, 61, 67,
      71, 73, 79, 83, 89, 97, 101, 103,
      107, 109, 113, 127, 131, 137, 139,
      149, 151, 157, 163, 167, 173, 179,
      181, 191, 193, 197, 199, 211, 223,
      227, 229, 233, 239, 241, 251, 257,
      263, 269, 271, 277, 281, 283, 293,
      307, 311, 313, 317, 331, 337, 347, 349
   };

   private readonly AlgApproach _algApproach;
   private readonly string _pattern; // the pattern (needed only for Las Vegas)
   private readonly long _patternHash; // pattern hash value
   private readonly int _patternLen; // pattern length
   private readonly long _precomputedRadixModule; // R^(M-1) % Q
   private readonly long _primeThreshold; // a large prime, small enough to avoid long overflow
   private readonly int _radix; // radix

   public RabinKarpSubstr(string pattern, AlgApproach algApproach = AlgApproach.MonteCarlo)
   {
      _pattern = pattern; // save pattern (needed only for Las Vegas)
      _algApproach = algApproach;
      _radix = DefaultRadix;
      _patternLen = _pattern.Length;
      _primeThreshold = GetLongRandomPrime();

      // precompute R^(m-1) % q for use in removing leading digit
      _precomputedRadixModule = 1;
      for (var i = 1; i <= _patternLen - 1; i++)
      {
         _precomputedRadixModule = _radix * _precomputedRadixModule % _primeThreshold;
      }

      _patternHash = ComputeHash(pattern, _patternLen);
   }

   /// <summary>
   ///    Returns the index of the 1st occurrence of the pattern string in the text string.
   /// </summary>
   /// <param name="text">The text string</param>
   /// <returns>The index of the 1st occurrence of the pattern string; -1 if no such match</returns>
   public int Search(string text)
   {
      const int notFoundVal = -1;
      var textLen = text.Length;
      if (textLen < _patternLen)
      {
         return notFoundVal;
      }

      var textHash = ComputeHash(text, _patternLen);

      // check for match at offset 0
      if (_patternHash == textHash)
      {
#if DEBUG
         Debug.Assert(Check(text, 0));
#endif
         return 0;
      }

      // check for hash match; if hash match, check for exact match (for debugging)
      for (var i = _patternLen; i < textLen; i++)
      {
         // Remove leading digit, add trailing digit, check for match.
         textHash = (textHash + _primeThreshold - _precomputedRadixModule * text[i - _patternLen] % _primeThreshold)
                    % _primeThreshold;
         textHash = (textHash * _radix + text[i])
                    % _primeThreshold;

         // match
         var offset = i - _patternLen + 1;
         if (_patternHash == textHash)
         {
#if DEBUG
            Debug.Assert(Check(text, offset));
#endif
            return offset;
         }
      }

      // no match
      return notFoundVal;
   }

   // Compute hash for key[0..m-1].
   private long ComputeHash(string key, int patternLen)
   {
      long hash = 0;
      for (var j = 0; j < patternLen; j++)
      {
         hash = (_radix * hash + key[j]) % _primeThreshold;
      }

      return hash;
   }

   // NOTEME: in general n should depends on the length of the substring to search
   // i.e. if the string is 'needle', then the n = 2^6 = 64
   private static long GetLongRandomPrime()
   {
      const int primeFloor = 0x10000; // 2^16 means the average length of substring is 16
      return GetLowLevelPrime(primeFloor);
   }

   private static long GetLowLevelPrime(int primeFloor)
   {
      // Generate a prime candidate divisible by first primes
      // Repeat until a number satisfying the test isn't found
      while (true)
      {
         //  Obtain a random number
         var primeCandidate = GetNthBitRandom(primeFloor);
         if (FirstPrimes.TakeWhile(divisor => primeCandidate % divisor != 0
                                              || divisor * divisor > primeCandidate).Any())
         {
            return primeCandidate;
         }
      }
   }

   private static long GetNthBitRandom(int primeFloor)
   {
      var rand = new Random();

      // Returns a random number between 2**(n-1)+1 and 2**n-1'''
      var max = (long)(Math.Pow(2, primeFloor) - 1);
      var min = (long)(Math.Pow(2, primeFloor - 1) + 1);

      return rand.NextInt64(min, max + 1);
   }

   private bool Check(string text, int offsetIdx)
   {
      switch (_algApproach)
      {
         case AlgApproach.MonteCarlo:
            return true;

         case AlgApproach.LasVegas:
            // does pat[] match txt[i..i-m+1] ?
            for (var j = 0; j < _patternLen; j++)
            {
               if (_pattern[j] != text[offsetIdx + j])
               {
                  return false;
               }
            }

            return true;

         default:
            goto case AlgApproach.MonteCarlo;
      }
   }
}