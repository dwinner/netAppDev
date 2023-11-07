// ReSharper disable UnusedMember.Local

namespace DataStructures.HashBased;

internal static class HashingStuff
{
   // Hashing function first reported by Dan Bernstein
   // http://www.cse.yorku.ca/~oz/hash.html
   private static int Djb2(string input)
   {
      var hash = 5381;
      foreach (int @char in input.ToCharArray())
      {
         unchecked
         {
            /* hash * 33 + c */
            hash = (hash << 5) + hash + @char;
         }
      }

      return hash;
   }

   // Treats each four characters as an integer so
   // "aaaabbbb" hashes differently than "bbbbaaaa"
   private static int FoldingHash(string input)
   {
      var hashValue = 0;
      var startIndex = 0;
      int currentFourBytes;
      do
      {
         currentFourBytes = GetNextBytes(startIndex, input);
         unchecked
         {
            hashValue += currentFourBytes;
         }

         startIndex += 4;
      } while (currentFourBytes != 0);

      return hashValue;
   }

   // Gets the next four bytes of the string converted to an
   // integer - If there are not enough characters, 0 is used.
   private static int GetNextBytes(int startIndex, string str)
   {
      var currentFourBytes = 0;
      currentFourBytes += GetByte(str, startIndex);
      currentFourBytes += GetByte(str, startIndex + 1) << 8;
      currentFourBytes += GetByte(str, startIndex + 2) << 16;
      currentFourBytes += GetByte(str, startIndex + 3) << 24;

      return currentFourBytes;
   }

   private static int GetByte(string str, int index) =>
      index < str.Length
         ? str[index]
         : 0;
}