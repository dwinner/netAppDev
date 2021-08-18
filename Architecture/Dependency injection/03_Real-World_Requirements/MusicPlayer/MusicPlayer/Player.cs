using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Player.Core
{
   public class Player
   {
      private readonly IEnumerable<ICodec> _codecs;

      public Player(IEnumerable<ICodec> codecs) => _codecs = codecs;

      public void Play(FileInfo fileInfo)
      {
         var supportingCodec = FindCodec(fileInfo.Extension);

         using (var rawStream = fileInfo.OpenRead())
         {
            var decodedStream = supportingCodec.Decode(rawStream);
            PlayStream(decodedStream);
         }
      }

      private ICodec FindCodec(string extension)
      {
         var foundCodec = _codecs.FirstOrDefault(codec => codec.CanDecode(extension));
         if (foundCodec != null)
         {
            return foundCodec;
         }

         throw new CodecNotFoundException("Codec not found");
      }

      private void PlayStream(Stream stream)
      {
         // Playing decoded stream
      }
   }
}