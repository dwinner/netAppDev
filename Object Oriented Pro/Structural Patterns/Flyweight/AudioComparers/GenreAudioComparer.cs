using System;
using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class GenreAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return string.Compare(x.Genre, y.Genre, StringComparison.CurrentCulture);
      }
   }
}