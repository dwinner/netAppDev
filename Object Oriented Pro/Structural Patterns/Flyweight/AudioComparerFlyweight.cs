using System.Collections.Generic;
using static Flyweight.AudioComparerFactory;

namespace Flyweight
{
   public sealed class AudioComparerFlyweight
   {
      private static AudioComparerFlyweight _instance;

      private readonly IDictionary<AudioComparisonType, IComparer<AudioEntity>> _audioComparers;

      private AudioComparerFlyweight()
      {
         _audioComparers = new SortedDictionary<AudioComparisonType, IComparer<AudioEntity>>();
      }

      public static AudioComparerFlyweight Instance { get; }
         = _instance ?? (_instance = new AudioComparerFlyweight());

      public IComparer<AudioEntity> this[AudioComparisonType comparisonType]
      {
         get
         {
            IComparer<AudioEntity> audioComparer;
            if (_audioComparers.TryGetValue(comparisonType, out audioComparer))
               return audioComparer;
            _audioComparers[comparisonType] = GetTypeComparer(comparisonType);
            return _audioComparers[comparisonType];
         }
      }
   }
}