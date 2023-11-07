using System.Text;
using StringHandling.Compression;

namespace StringHandling.Tests;

[TestFixture]
public class CompressionTests : StringTestsBase
{
   [TearDown]
   public void TearDown()
   {
      File.Delete(Original);
      File.Delete(Zipped);
      File.Delete(UnZipped);
   }

   private const string Original = $"{nameof(Original)}.txt";
   private const string Zipped = $"{nameof(Zipped)}.bin";
   private const string UnZipped = $"{nameof(UnZipped)}.bin";

   [TestCase("binFiles/4runs.bin", Zipped, UnZipped, 6L)]
   [TestCase("binFiles/q32x48.bin", Zipped, UnZipped, 209L)]
   [TestCase("binFiles/q64x96.bin", Zipped, UnZipped, 603L)]
   public void RunLenTest(string originalFilename, string srcFilename, string dstFilename, long expectedCompressedSize)
   {
      ICompressionAlg runLen = new RunLenImpl();
      runLen.Compress(originalFilename, srcFilename);
      runLen.Decompress(srcFilename, dstFilename);
      var compFile = new FileInfo(srcFilename);
      var origFile = new FileInfo(originalFilename);
      var uncompFile = new FileInfo(dstFilename);
      Assert.Multiple(() =>
      {
         Assert.That(expectedCompressedSize, Is.EqualTo(compFile.Length));
         Assert.That(origFile.Length, Is.EqualTo(uncompFile.Length));
      });

      using var expectedReader = new BinaryReader(origFile.OpenRead());
      using var actualReader = new BinaryReader(uncompFile.OpenRead());
      for (var i = 0; i < origFile.Length; i++)
      {
         var expectedByte = expectedReader.ReadByte();
         var actualByte = actualReader.ReadByte();
         Assert.That(expectedByte, Is.EqualTo(actualByte), "Failed at offset: {0}", i);
      }
   }

   [TestCase("ATAGATGCATAGCGCATAGCTAGATGTGCTAGC")]
   public void GenomeTest(string genome)
   {
      var encoding = Encoding.Default;
      File.WriteAllText(Original, genome, encoding);
      ICompressionAlg genomeImpl = new GenomeImpl(encoding, true);
      genomeImpl.Compress(Original, Zipped);
      genomeImpl.Decompress(Zipped, UnZipped);
      var actualGenome = File.ReadAllText(UnZipped, encoding);
      Assert.That(actualGenome, Is.EqualTo(genome));
   }

   [Test]
   public void LzwTest()
   {
      const string expectedToDecode = "ABRACADABRABRABRA";
      var encoding = Encoding.Default;
      File.WriteAllText(Original, expectedToDecode, encoding);
      ICompressionAlg lzwImpl = new LzwImpl(encoding);
      lzwImpl.Compress(Original, Zipped);
      lzwImpl.Decompress(Zipped, UnZipped);
      var actualDecoded = File.ReadAllText(UnZipped, encoding);
      Assert.That(actualDecoded, Is.EqualTo(expectedToDecode));
   }

   [TestCase("https://algs4.cs.princeton.edu/55compression/abra.txt")]
   [TestCase("https://algs4.cs.princeton.edu/55compression/tinytinyTale.txt")]
   [TestCase("https://algs4.cs.princeton.edu/55compression/medTale.txt")]
   [TestCase("https://algs4.cs.princeton.edu/55compression/tale.txt")]
   public async Task HuffmanTest(string url)
   {
      var content = await GetContentAsync(url);
      var encoding = Encoding.Default;
      await File.WriteAllTextAsync(Original, content, encoding);
      ICompressionAlg huffman = new HuffmanImpl(encoding);
      huffman.Compress(Original, Zipped);
      huffman.Decompress(Zipped, UnZipped);
      var actualDecoded = await File.ReadAllTextAsync(UnZipped, encoding);
      Assert.That(actualDecoded, Is.EqualTo(content));
   }
}