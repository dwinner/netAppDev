using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class DurationAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return x.Duration - y.Duration;
      }
   }
}