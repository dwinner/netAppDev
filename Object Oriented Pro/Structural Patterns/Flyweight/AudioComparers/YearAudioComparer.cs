using System.Collections.Generic;
using PostSharp.Patterns.Contracts;

namespace Flyweight.AudioComparers
{
   internal class YearAudioComparer : IComparer<AudioEntity>
   {
      public int Compare([Required] AudioEntity x, [Required] AudioEntity y)
         => x.Year - y.Year;
   }
}