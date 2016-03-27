using System;
using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class GroupAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return string.Compare(x.Group, y.Group, StringComparison.CurrentCulture);
      }
   }
}