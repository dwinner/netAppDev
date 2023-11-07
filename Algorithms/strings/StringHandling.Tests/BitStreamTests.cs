using System.Text;
using BitStreams;
using StringHandling.Compression;

namespace StringHandling.Tests;

[TestFixture]
public class BitStreamTests
{
   [TestCase("one", "one00000")]
   [TestCase("five", "five0000")]
   [TestCase("another", "another0")]
   [TestCase("other_another", "other_another000")]
   public void AlignByLgRTest(string input, string expected)
   {
      var actual = input.AlignBy();
      var strLen = actual.Length;
      var (_, remainder) = Math.DivRem(strLen, BitLevelExtensions.LgR);
      Assert.Multiple(() =>
      {
         Assert.That(remainder, Is.EqualTo(0));
         Assert.That(expected, Is.EqualTo(actual));
      });
   }

   [Test]
   public void BitStream_ReadTest()
   {
      const string testStr = "abcd";
      const int bitCount = 8;
      var encoding = Encoding.Default;
      var strBytes = encoding.GetBytes(testStr.ToCharArray());
      using var memStream = new MemoryStream(strBytes);
      var perByteLen = memStream.Length;
      var bitStream = new BitStream(memStream, encoding, true);
      var binDump = new StringBuilder();
      for (var i = 0; i < perByteLen; i++)
      {
         var readBits = bitStream.ReadBits(bitCount);
         Array.ForEach(readBits, bit =>
         {
            var boolVal = bit.AsBool();
            binDump.Append(boolVal ? "1" : "0");
         });
      }

      const string expected = "01100001011000100110001101100100";
      var actual = binDump.ToString();
      Assert.That(actual, Is.EqualTo(expected));
   }

   [Test]
   public void BitReaderTest()
   {
      const string testStr = "abcd";
      var strBytes = Encoding.Default.GetBytes(testStr.ToCharArray());
      var memStream = new MemoryStream(strBytes);
      var bitReader = new BitReader(memStream, true);
      var binaryDump1 = new StringBuilder();
      bool? currentBit;
      while ((currentBit = bitReader.ReadBit()) != null)
      {
         binaryDump1.Append(currentBit.Value
            ? "1"
            : "0");
      }

      var actual1 = binaryDump1.Remove(0, 8).ToString();
      const string expected = "01100001011000100110001101100100";
      Assert.That(actual1, Is.EqualTo(expected));
   }
}

sealed file class BitReader
{
   private readonly bool _bigEndian;
   private readonly Stream _stream;
   private int _bit;
   private byte _currentByte;

   public BitReader(Stream stream, bool bigEndian = false)
   {
      _stream = stream;
      _bigEndian = bigEndian;
   }

   public bool? ReadBit()
   {
      const int byteThreshold = 8;
      const int eof = -1;

      if (_bit == byteThreshold)
      {
         var currentByte = _stream.ReadByte();
         if (currentByte == eof)
         {
            return null;
         }

         _bit = 0;
         _currentByte = (byte)currentByte;
      }

      var value = !_bigEndian
         ? (_currentByte & (1 << _bit)) > 0
         : (_currentByte & (1 << (7 - _bit))) > 0;

      _bit++;
      return value;
   }
}