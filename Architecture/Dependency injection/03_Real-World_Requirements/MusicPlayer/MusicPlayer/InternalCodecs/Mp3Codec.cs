using System;
using System.IO;

namespace Player.Core.InternalCodecs
{
   public class Mp3Codec : ICodec
   {
      public string Name => "MP3 Audio";

      public bool CanDecode(string extension) => extension == ".mp3";

      public Stream Decode(Stream inStream) => throw new NotImplementedException();
   }
}