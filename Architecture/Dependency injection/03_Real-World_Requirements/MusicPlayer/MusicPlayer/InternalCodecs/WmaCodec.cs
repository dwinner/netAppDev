using System;
using System.IO;

namespace Player.Core.InternalCodecs
{
   public class WmaCodec : ICodec
   {
      public string Name => "Windows Media Auido";

      public bool CanDecode(string extension) => extension == ".wma";

      public Stream Decode(Stream inStream) => throw new NotImplementedException();
   }
}