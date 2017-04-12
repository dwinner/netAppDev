using System;
using System.Collections.Generic;
using PostSharp.Patterns.Contracts;

namespace Flyweight.AudioComparers
{
   internal class RecordFormatAudioComparer : IComparer<AudioEntity>
   {
      public int Compare([Required] AudioEntity x, [Required] AudioEntity y) => string.Compare(x.RecordFormat,
         y.RecordFormat, StringComparison.CurrentCulture);
   }
}