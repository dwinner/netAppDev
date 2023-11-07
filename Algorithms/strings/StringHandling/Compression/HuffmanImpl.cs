using System.Diagnostics;
using System.Text;
using Ayx.BitIO;
using Sorting.Algs.PrioQ;

namespace StringHandling.Compression;

/// <summary>
///    The <see cref="HuffmanImpl" /> class provides static methods for compressing
///    and expanding a binary input using Huffman codes over the 8-bit extended
///    ASCII alphabet.
/// </summary>
public sealed class HuffmanImpl : ICompressionAlg
{
   private const int Radix = 256; // alphabet size of extended ASCII
   private const int InitialLenCapacity = 32;
   private const int InitialCharLen = 8;
   private const char TerminalChar = '\0';
   private const int DegenerateFrequency = -1;
   private readonly Encoding _encoding;

   public HuffmanImpl(Encoding encoding) => _encoding = encoding;

   /// <summary>
   ///    Reads a sequence of 8-bit bytes from standard input; compresses them
   ///    using Huffman codes with an 8-bit alphabet; and writes the results
   ///    to output.
   /// </summary>
   /// <param name="srcFilename">File to compress</param>
   /// <param name="dstFilename">Compressed file</param>
   public void Compress(string srcFilename, string dstFilename)
   {
      var srcFInf = new FileInfo(srcFilename);
      using var srcReader = new StreamReader(srcFInf.OpenRead(), _encoding);

      // read the input
      var strToCompress = srcReader.ReadToEnd();
      var inputChars = strToCompress.ToCharArray();

      // tabulate frequency counts
      var frequencies = new int[Radix];
      foreach (var charAt in inputChars)
      {
         frequencies[charAt]++;
      }

      // build Huffman trie
      var root = BuildTrie(frequencies);

      // build code table
      var symbolTable = new string[Radix];
      BuildCode(symbolTable, root, string.Empty);

      // print trie for decoder
      var bitWriter = new BitWriter();
      WriteTrie(root, bitWriter);

      // print number of bytes in original uncompressed message
      bitWriter.WriteInt(inputChars.Length, InitialLenCapacity);

      // use Huffman code to encode input
      foreach (var codeChar in inputChars.SelectMany(@char => symbolTable[@char]))
      {
         switch (codeChar)
         {
            case '0':
               bitWriter.WriteBool(false);
               break;
            case '1':
               bitWriter.WriteBool(true);
               break;
            default:
               throw new InvalidOperationException($"Illegal state: '{codeChar}'");
         }
      }

      var encodedBytes = bitWriter.GetBytes();

      // Store compressed bytes
      var dstFInf = new FileInfo(dstFilename);
      using var binWriter = new BinaryWriter(dstFInf.OpenWrite(), _encoding);
      binWriter.Write(encodedBytes);
   }

   /// <summary>
   ///    Reads a sequence of bits that represents a Huffman-compressed message from
   ///    standard input; expands them; and writes the results to standard output.
   /// </summary>
   /// <param name="srcFilename">File to decompress</param>
   /// <param name="dstFilename">Decompressed file</param>
   public void Decompress(string srcFilename, string dstFilename)
   {
      var srcFInf = new FileInfo(srcFilename);
      var fileLen = (int)srcFInf.Length;
      using var binReader = new BinaryReader(srcFInf.OpenRead(), _encoding);
      var encodedBytes = binReader.ReadBytes(fileLen);
      var bitReader = new BitReader(encodedBytes);

      // read in Huffman trie from bit stream
      var root = ReadTrie(bitReader);

      // Get the number of bytes to write
      var len = bitReader.ReadInt(InitialLenCapacity);

      // decode using the Huffman trie
      var dstFInf = new FileInfo(dstFilename);
      using var writer = new StreamWriter(dstFInf.OpenWrite(), _encoding);
      for (var i = 0; i < len; i++)
      {
         var currentNode = root;
         while (!currentNode!.IsLeaf)
         {
            var readBit = bitReader.ReadBool();
            currentNode = readBit
               ? currentNode.Right
               : currentNode.Left;
         }

         writer.Write(currentNode.Char);
      }
   }

   // write bitstring-encoded trie to bit output
   private static void WriteTrie(Node? node, BitWriter bitWriter)
   {
      if (node == null)
      {
         return;
      }

      if (node.IsLeaf)
      {
         bitWriter.WriteBool(true);
         bitWriter.WriteChar(node.Char, InitialCharLen);
         return;
      }

      bitWriter.WriteBool(false);
      WriteTrie(node.Left, bitWriter);
      WriteTrie(node.Right, bitWriter);
   }

   // make a lookup table from symbols and their encodings
   private static void BuildCode(IList<string> symbolTable, Node? node, string prefix)
   {
      if (node == null)
      {
         return;
      }

      if (!node.IsLeaf)
      {
         BuildCode(symbolTable, node.Left, $"{prefix}0");
         BuildCode(symbolTable, node.Right, $"{prefix}1");
      }
      else
      {
         var @char = node.Char;
         symbolTable[@char] = prefix;
      }
   }

   // build the Huffman trie given frequencies
   private static Node BuildTrie(IReadOnlyList<int> frequencies)
   {
      // initialize priority queue with singleton trees
      var priorityQueue = new MinPrioQueue<Node>();
      for (var chr = (char)0; chr < Radix; chr++)
      {
         var frequency = frequencies[chr];
         if (frequency > 0)
         {
            priorityQueue.Insert(new Node(chr, frequency, null, null));
         }
      }

      // merge two smallest trees
      while (priorityQueue.Count > 1)
      {
         var left = priorityQueue.DelMin();
         var right = priorityQueue.DelMin();
         var parent = new Node(TerminalChar, left.Frequency + right.Frequency, left, right);
         priorityQueue.Insert(parent);
      }

      return priorityQueue.DelMin();
   }

   private static Node ReadTrie(BitReader bitReader)
   {
      var isLeaf = bitReader.ReadBool();
      if (isLeaf)
      {
         var readChar = bitReader.ReadChar(InitialCharLen);
         return new Node(readChar, DegenerateFrequency, null, null);
      }

      var leftTrie = ReadTrie(bitReader);
      var rightTrie = ReadTrie(bitReader);

      return new Node(TerminalChar, DegenerateFrequency, leftTrie, rightTrie);
   }

   /// <summary>
   ///    Huffman trie node
   /// </summary>
   private sealed class Node : IComparable<Node>
   {
      public Node(char aChar, int frequency, Node? left, Node? right)
      {
         Char = aChar;
         Frequency = frequency;
         Left = left;
         Right = right;
      }

      public char Char { get; }

      public int Frequency { get; private set; }

      public Node? Left { get; }

      public Node? Right { get; }

      /// <summary>
      ///    Is the node a leaf node?
      /// </summary>
      public bool IsLeaf
      {
         get
         {
            Debug.Assert((Left == null && Right == null) || (Left != null && Right != null));
            return Left == null && Right == null;
         }
      }

      public int CompareTo(Node? other)
      {
         Debug.Assert(other != null, $"{nameof(other)} != null");
         return Frequency = other.Frequency;
      }
   }
}