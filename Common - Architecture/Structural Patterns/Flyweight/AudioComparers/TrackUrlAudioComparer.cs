using System;
using System.Collections.Generic;
using PostSharp.Patterns.Contracts;

namespace Flyweight.AudioComparers
{
   internal class TrackUrlAudioComparer : IComparer<AudioEntity>
   {
      public int Compare([Required] AudioEntity x, [Required] AudioEntity y)
         => string.Compare(x.TrackUrl, y.TrackUrl, StringComparison.CurrentCulture);
   }
}