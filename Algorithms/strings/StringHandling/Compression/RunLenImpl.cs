using System.Collections;
using BitStreams;

namespace StringHandling.Compression;

public sealed class RunLenImpl : ICompressionAlg
{
   private const int Radix = 0x100;

   public void Decompress(string srcFilename, string dstFilename)
   {
      var compressed = new FileInfo(srcFilename);
      var fileLen = compressed.Length;
      using var binReader = new BinaryReader(compressed.OpenRead());
      var outBool = false;
      var bitValues = new List<bool>((int)fileLen);
      for (var i = 0; i < fileLen; i++)
      {
         var runLen = binReader.ReadByte();
         for (var j = 0; j < runLen; j++)
         {
            bitValues.Add(outBool);
         }

         outBool = !outBool;
      }

      var bitArray = new BitArray(bitValues.ToArray());
      var decodedBytes = bitArray.ToByteArray();

      var uncompressedFile = new FileInfo(dstFilename);
      using var binaryWriter = new BinaryWriter(uncompressedFile.OpenWrite());
      binaryWriter.Write(decodedBytes);
   }

   public void Compress(string srcFilename, string dstFilename)
   {
      var fileToCompress = new FileInfo(srcFilename);
      var fileLen = fileToCompress.Length;
      var inputBytes = new byte[fileLen];
      using var binReader = new BinaryReader(fileToCompress.OpenRead());
      for (var i = 0; i < inputBytes.Length; i++)
      {
         inputBytes[i] = binReader.ReadByte();
      }

      var compressedFileInfo = new FileInfo(dstFilename);
      using var binWriter = new BinaryWriter(compressedFileInfo.OpenWrite());

      byte runLen = 0;
      var old = false;
      var bitStream = new BitStream(inputBytes);
      for (var i = 0; i < fileLen; i++)
      {
         var readBits = bitStream.ReadBits(BitLevelExtensions.LgR);
         Array.ForEach(readBits, bit =>
         {
            var boolVal = bit.AsBool();
            if (boolVal != old)
            {
               binWriter.Write(runLen);
               runLen = 1;
               old = !old;
            }
            else
            {
               if (runLen == Radix - 1)
               {
                  binWriter.Write(runLen);
                  runLen = 0;
                  binWriter.Write(runLen);
               }

               runLen++;
            }
         });
      }

      binWriter.Write(runLen);
   }
}