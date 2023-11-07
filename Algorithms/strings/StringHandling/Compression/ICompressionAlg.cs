namespace StringHandling.Compression;

/// <summary>
///    Common compression behavior
/// </summary>
public interface ICompressionAlg
{
   /// <summary>
   ///    Compress to some kind of <see cref="Stream" />
   /// </summary>
   /// <param name="srcFilename">File to compress</param>
   /// <param name="dstFilename">Compressed file</param>
   void Compress(string srcFilename, string dstFilename);

   /// <summary>
   ///    Decompress from some kind of <see cref="Stream" />
   /// </summary>
   /// <param name="srcFilename">File to decompress</param>
   /// <param name="dstFilename">Decompressed file</param>
   void Decompress(string srcFilename, string dstFilename);
}