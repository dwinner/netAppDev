using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class YearAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return x.Year - y.Year;
      }
   }
}