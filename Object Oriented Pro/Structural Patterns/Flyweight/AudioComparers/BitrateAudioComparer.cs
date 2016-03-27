using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class BitrateAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return x.Bitrate - y.Bitrate;
      }
   }
}