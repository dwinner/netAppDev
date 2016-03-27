using System;
using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class TrackNameAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return string.Compare(x.TrackName, y.TrackName, StringComparison.CurrentCulture);
      }
   }
}