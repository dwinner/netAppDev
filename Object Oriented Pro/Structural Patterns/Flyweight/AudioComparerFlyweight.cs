using System.Collections.Generic;

namespace Flyweight
{
   public sealed class AudioComparerFlyweight
   {
      private static AudioComparerFlyweight _instance;

      public static AudioComparerFlyweight Instance
      {
         get { return _instance ?? (_instance = new AudioComparerFlyweight()); }
      }

      private AudioComparerFlyweight() { }

      private readonly IDictionary<AudioComparisonType, IComparer<AudioEntity>> _audioComparers =
         new SortedDictionary<AudioComparisonType, IComparer<AudioEntity>>();

      public IComparer<AudioEntity> this[AudioComparisonType comparisonType]
      {
         get
         {
            IComparer<AudioEntity> audioComparer;
            if (_audioComparers.TryGetValue(comparisonType, out audioComparer))
            {
               return audioComparer;
            }
            _audioComparers[comparisonType] = AudioComparerFactory.GetTypeComparer(comparisonType);
            return _audioComparers[comparisonType];
         }
      }
   }
}