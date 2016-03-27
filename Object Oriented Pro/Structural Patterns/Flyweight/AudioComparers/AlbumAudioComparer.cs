using System;
using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class AlbumAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return string.Compare(x.Album, y.Album, StringComparison.CurrentCulture);
      }
   }
}