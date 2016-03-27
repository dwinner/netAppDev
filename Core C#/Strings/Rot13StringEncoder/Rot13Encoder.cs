using System;
using System.Collections.Generic;
using System.Linq;

namespace Rot13StringEncoder
{
   class Rot13Encoder : System.Text.Encoding
   {
      // Класс-кодировщик определяет много разных перегрузок
      // для преобразования, но все они вызывают эти методы,
      // которые достаточно реализовать
      public override int GetByteCount(char[] chars, int index, int count)
      {
         if (chars == null)
            throw new ArgumentNullException("chars");
         if (index < 0)
            throw new ArgumentOutOfRangeException("index");
         if (count < 0)
            throw new ArgumentOutOfRangeException("count");
         if (index + count > chars.Length)
            throw new ArgumentOutOfRangeException("index, count");
         // Мы будем использовать один байт для каждого символа
         // (вы можете делать, что хотите: если вам нужно 6 битов - пожалуйста)
         return count;
      }

      public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
      {
         if (chars == null)
            throw new ArgumentNullException("chars");
         if (bytes == null)
            throw new ArgumentNullException("bytes");
         if (charIndex < 0)
            throw new ArgumentOutOfRangeException("charIndex");
         if (charCount < 0)
            throw new ArgumentOutOfRangeException("charCount");
         if (charIndex + charCount > chars.Length)
            throw new ArgumentOutOfRangeException("charIndex, charCount");
         if (bytes.Length - byteIndex < charCount)
            throw new ArgumentException("Not enough bytes in destination");
         if (!AreValidChars(chars))
            throw new ArgumentException("Only lower-case alphabetic characters allowed");

         for (int i = charIndex, dest = byteIndex; i < charIndex + charCount; ++i, ++dest)
         {
            // Использовать в качестве базы 'a' = 0
            int value = chars[i] - 'a';
            value = (value + 13) % 26;
            value += 'a';
            bytes[dest] = (byte)value;
         }

         return charCount; // Вернуть количество записанных символов
      }

      private static bool AreValidChars(IEnumerable<char> chars)
      {
         return chars.All(c => c >= 'a' && c <= 'z');
      }

      public override int GetCharCount(byte[] bytes, int index, int count)
      {
         if (bytes == null)
            throw new ArgumentNullException("bytes");
         if (index < 0)
            throw new ArgumentOutOfRangeException("index");
         if (count < 0)
            throw new ArgumentOutOfRangeException("count");
         if (index + count > bytes.Length)
            throw new ArgumentOutOfRangeException("index, count");
         return count;
      }

      public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
      {
         if (bytes == null)
            throw new ArgumentNullException("bytes");
         if (chars == null)
            throw new ArgumentNullException("chars");
         if (charIndex < 0)
            throw new ArgumentOutOfRangeException("charIndex");
         if (charIndex >= chars.Length)
            throw new ArgumentOutOfRangeException("charIndex");
         if (byteIndex < 0)
            throw new ArgumentOutOfRangeException("byteIndex");
         if (byteCount < 0)
            throw new ArgumentOutOfRangeException("byteCount");
         if (byteIndex + byteCount > bytes.Length)
            throw new ArgumentOutOfRangeException("byteIndex, byteCount");

         for (int i = byteIndex, dest = charIndex; i < byteIndex + byteCount; ++i, ++dest)
         {
            // Использовать в качестве базы 'a' = 0
            int value = bytes[i] - 'a';
            value = (value + 13) % 26;
            value += 'a';
            chars[dest] = (char)value;
         }

         return byteCount; // Вернуть количество записанных символов
      }

      public override int GetMaxByteCount(int charCount)
      {
         // Поскольку на каждый символ отводится один байт, все очень просто
         return charCount;
      }

      public override int GetMaxCharCount(int byteCount)
      {
         return byteCount;
      }
   }
}
