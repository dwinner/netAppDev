using System;
using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class TrackUrlAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return string.Compare(x.TrackUrl, y.TrackUrl, StringComparison.CurrentCulture);
      }
   }
}