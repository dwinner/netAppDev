using System.Text;
using BitStreams;
using static StringHandling.Compression.BitLevelExtensions;

namespace StringHandling.Compression;

/// <summary>
///    The <see cref="GenomeImpl" /> class provides methods for compressing
///    and expanding a genomic sequence using a 2-bit code.
/// </summary>
public sealed class GenomeImpl : ICompressionAlg
{
   private static readonly Dictionary<char, string> GenomeMap = new()
   {
      ['A'] = "00",
      ['C'] = "01",
      ['G'] = "10",
      ['T'] = "11"
   };

   private static readonly Dictionary<string, char> BinMap = GenomeMap.Inverse();

   private readonly Encoding _encoding;
   private readonly bool _msb;

   public GenomeImpl(Encoding encoding, bool msb)
   {
      _encoding = encoding;
      _msb = msb;
   }

   /// <summary>
   ///    Reads a sequence of 8-bit extended ASCII characters over the alphabet
   ///    { A, C, T, G } from input; compresses them using two bits per
   ///    character; and writes the results to standard output.
   /// </summary>
   /// <param name="srcFilename">File to compress</param>
   /// <param name="dstFilename">Compressed file</param>
   public void Compress(string srcFilename, string dstFilename)
   {
      var genomeStr = File.ReadAllText(srcFilename, _encoding);
      var strLen = genomeStr.Length;
      var dstFileInf = new FileInfo(dstFilename);
      using var outWriter = new BinaryWriter(dstFileInf.OpenWrite(), _encoding);

      // Write the genome length at the beginning
      outWriter.Write(strLen);

      // Align the string by 8 and make the binary form of it
      var alignedDna = genomeStr.AlignBy(fillSymbol: 'A');
      var binBuilder = new StringBuilder(alignedDna.Length * 2);
      foreach (var binGenome in alignedDna.Select(dnaChar => GenomeMap[dnaChar]))
      {
         binBuilder.Append(binGenome);
      }

      // Transform the binary string into the byte array
      var encodedBytes = binBuilder.ToString().ToByteArray();

      // Save the encoded byte array
      outWriter.Write(encodedBytes);
   }

   /// <summary>
   ///    Reads a binary sequence from input; converts each two bits
   ///    to an 8-bit extended ASCII character over the alphabet { A, C, T, G };
   ///    and writes the results to standard output.
   /// </summary>
   /// <param name="srcFilename">File to decompress</param>
   /// <param name="dstFilename">Decompressed file</param>
   public void Decompress(string srcFilename, string dstFilename)
   {
      var srcFileInf = new FileInfo(srcFilename);
      using var inBinReader = new BinaryReader(srcFileInf.OpenRead(), _encoding);

      // Read the genome length
      var genomeLen = inBinReader.ReadInt32();
      var ceilingNum = (int)Math.Ceiling((decimal)genomeLen / 4);

      // Read the encoded byte array
      var encodedBytes = inBinReader.ReadBytes(ceilingNum);
      var bitStream = new BitStream(encodedBytes, _encoding, _msb);

      // Decode bytes to genome symbols
      var decoded = new StringBuilder(genomeLen + 1);
      for (var i = 0; i < ceilingNum; i++)
      {
         var bits = bitStream.ReadBits(LgR);
         for (var j = 0; j < LgR; j += 2)
         {
            var bit1St = bits[j].AsBool();
            var bit2Nd = bits[j + 1].AsBool();
            var bitDna = $"{(bit1St ? "1" : "0")}{(bit2Nd ? "1" : "0")}";
            var decodedChar = BinMap[bitDna];
            decoded.Append(decodedChar);
         }
      }

      // Truncate and safe decoded genome
      var decodedGenome = decoded.ToString();
      var resultGenome = decodedGenome[..genomeLen];
      File.WriteAllText(dstFilename, resultGenome, _encoding);
   }
}