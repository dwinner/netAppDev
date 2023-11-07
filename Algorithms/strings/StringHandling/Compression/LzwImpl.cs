using System.Text;
using Ayx.BitIO;
using StringHandling.Prefix;

namespace StringHandling.Compression;

/// <summary>
///    The <see cref="LzwImpl" /> {@code LZW} class provides static methods for compressing
///    and expanding a binary input using LZW compression over the 8-bit extended
///    ASCII alphabet with 12-bit codewords.
/// </summary>
public sealed class LzwImpl : ICompressionAlg
{
   private const int InputCharsCount = 256; // number of input chars
   private const int CodeWordsCount = 4096; // number of codewords = 2^W
   private const int CodeWordWidth = 12; // codeword width
   private readonly Encoding _encoding;

   public LzwImpl(Encoding encoding) => _encoding = encoding;

   /// <summary>
   ///    Reads a sequence of 8-bit bytes from input; compresses
   ///    them using LZW compression with 12-bit codewords; and writes the results
   ///    to output.
   /// </summary>
   /// <param name="srcFilename">File to compress</param>
   /// <param name="dstFilename">Compressed file</param>
   public void Compress(string srcFilename, string dstFilename)
   {
      var stringToCompress = File.ReadAllText(srcFilename, _encoding);
      var trnTrie = new TernarySearchTrie<int>();

      // since TST is not balanced, it would be better to insert in a different order
      for (var idxAt = 0; idxAt < InputCharsCount; idxAt++)
      {
         var @char = (char)idxAt;
         trnTrie[$"{@char}"] = idxAt;
      }

      var code = InputCharsCount + 1; // R is codeword for EOF
      var bitWriter = new BitWriter();
      while (stringToCompress.Length > 0)
      {
         var prefix = trnTrie.GetLongestPrefixOf(stringToCompress); // Find max prefix match s.
         var trieValue = trnTrie[prefix];
         bitWriter.WriteInt(trieValue, CodeWordWidth); // Print s's encoding.
         var prefixLen = prefix.Length;

         // Add s to symbol table.
         if (prefixLen < stringToCompress.Length && code < CodeWordsCount)
         {
            var trieStr = stringToCompress[..(prefixLen + 1)];
            trnTrie[trieStr] = code++;
         }

         // Scan past s in input.
         stringToCompress = stringToCompress[prefixLen..];
      }

      bitWriter.WriteInt(InputCharsCount, CodeWordWidth);
      var encodedBytes = bitWriter.GetBytes();

      // Store compressed bytes
      var dstFileInfo = new FileInfo(dstFilename);
      using var binaryWriter = new BinaryWriter(dstFileInfo.OpenWrite(), _encoding);
      binaryWriter.Write(encodedBytes);
   }

   /// <summary>
   ///    Reads a sequence of bit encoded using LZW compression with
   ///    12-bit codewords from input; expands them; and writes
   ///    the results to output.
   /// </summary>
   /// <param name="srcFilename">File to decompress</param>
   /// <param name="dstFilename">Decompressed file</param>
   public void Decompress(string srcFilename, string dstFilename)
   {
      var srcFileInfo = new FileInfo(srcFilename);
      var fileLen = (int)srcFileInfo.Length;
      using var binaryReader = new BinaryReader(srcFileInfo.OpenRead(), _encoding);
      var encodedBytes = binaryReader.ReadBytes(fileLen);
      var bitReader = new BitReader(encodedBytes);

      var symbolTable = new string[CodeWordsCount];
      int i; // next available codeword value

      // initialize symbol table with all 1-character strings
      for (i = 0; i < InputCharsCount; i++)
      {
         var @char = (char)i;
         symbolTable[i] = $"{@char}";
      }

      symbolTable[i++] = string.Empty; // (unused) lookahead for EOF
      var codeWord = bitReader.ReadInt(CodeWordWidth);
      if (codeWord == InputCharsCount)
      {
         // expanded message is empty string
         return;
      }

      var dstFileInfo = new FileInfo(dstFilename);
      using var writer = new StreamWriter(dstFileInfo.OpenWrite(), _encoding);
      var val = symbolTable[codeWord];
      while (true)
      {
         writer.Write(val);
         codeWord = bitReader.ReadInt(CodeWordWidth);
         if (codeWord == InputCharsCount)
         {
            break;
         }

         var decodedVal = symbolTable[codeWord];
         if (i == codeWord)
         {
            // special case hack
            decodedVal = $"{val}{val[0]}";
         }

         if (i < CodeWordsCount)
         {
            symbolTable[i++] = $"{val}{decodedVal[0]}";
         }

         val = decodedVal;
      }
   }
}