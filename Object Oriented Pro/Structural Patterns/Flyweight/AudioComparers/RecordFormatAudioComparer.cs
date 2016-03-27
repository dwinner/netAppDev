using System;
using System.Collections.Generic;

namespace Flyweight.AudioComparers
{
   class RecordFormatAudioComparer : IComparer<AudioEntity>
   {
      public int Compare(AudioEntity x, AudioEntity y)
      {
         return string.Compare(x.RecordFormat, y.RecordFormat, StringComparison.CurrentCulture);
      }
   }
}