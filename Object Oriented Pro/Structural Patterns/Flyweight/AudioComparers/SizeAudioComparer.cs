using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class SizeAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return x.Size - y.Size;
      }
   }
}