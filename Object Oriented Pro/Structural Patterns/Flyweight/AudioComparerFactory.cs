using Flyweight.AudioComparers;
using System;
using System.Collections.Generic;

namespace Flyweight
{
   public static class AudioComparerFactory
   {
      public static IComparer<AudioEntity> GetTypeComparer(AudioComparisonType comparisonType)
      {
         switch (comparisonType)
         {
            case AudioComparisonType.TrackName:
               return new TrackNameAudioComparer();

            case AudioComparisonType.Duration:
               return new DurationAudioComparer();

            case AudioComparisonType.Bitrate:
               return new BitrateAudioComparer();

            case AudioComparisonType.TrackUrl:
               return new TrackUrlAudioComparer();

            case AudioComparisonType.Group:
               return new GroupAudioComparer();

            case AudioComparisonType.Year:
               return new YearAudioComparer();

            case AudioComparisonType.RecordFormat:
               return new RecordFormatAudioComparer();

            case AudioComparisonType.Album:
               return new SizeAudioComparer();

            case AudioComparisonType.Size:
               return new GenreAudioComparer();

            case AudioComparisonType.Genre:
               return new GenreAudioComparer();

            default:
               throw new InvalidOperationException("Unknown comparison type");
         }
      }
   }
}