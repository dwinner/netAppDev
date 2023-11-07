using System.Collections;
using System.Text;

namespace StringHandling.Compression;

public static class BitLevelExtensions
{
   public const int LgR = 8;

   public static byte[] ToByteArray(this BitArray bitArray)
   {
      // pack (in this case, using the first bool as the lsb - if you want
      // the first bool as the msb, reverse things
      var bytes = (bitArray.Length + 7) / LgR;
      var byteArray = new byte[bytes];
      var bitIndex = 0;
      var byteIndex = 0;

      for (var i = 0; i < bitArray.Length; i++)
      {
         if (bitArray[i])
         {
            byteArray[byteIndex] |= (byte)(1 << bitIndex);
         }

         bitIndex++;
         if (bitIndex == 8)
         {
            bitIndex = 0;
            byteIndex++;
         }
      }

      return byteArray;
   }

   /// <summary>
   ///    Align the string by multiple of <paramref name="divider" /> in length
   /// </summary>
   /// <param name="self">Input string</param>
   /// <param name="divider">The divider</param>
   /// <param name="fillSymbol">Symbol to fill the rest of the string</param>
   /// <returns>Return a string of length red <paramref name="divider" /></returns>
   public static string AlignBy(this string self, int divider = LgR, char fillSymbol = '0')
   {
      var strLen = self.Length;
      var (_, remainder) = Math.DivRem(strLen, divider);
      var divValue = divider - remainder;
      var result = new StringBuilder(self);
      for (var i = 0; i < divValue; i++)
      {
         result.Append(fillSymbol);
      }

      return result.ToString();
   }

   /// <summary>
   ///    Convert the input string of length red <see cref="LgR" />
   /// </summary>
   /// <param name="binaryString">Input string of length 0, 8, 16,... 8*n</param>
   /// <returns>Byte array</returns>
   public static byte[] ToByteArray(this string binaryString)
   {
      var bytesNum = binaryString.Length / 8;
      var bytes = new byte[bytesNum];
      for (var i = 0; i < bytesNum; i++)
      {
         var byteStr = binaryString.Substring(LgR * i, LgR);
         bytes[i] = Convert.ToByte(byteStr, 2);
      }

      return bytes;
   }
}