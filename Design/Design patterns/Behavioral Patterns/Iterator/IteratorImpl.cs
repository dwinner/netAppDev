namespace Iterator
{
   public class IteratorImpl<T> : IIterator<T>
   {
      private readonly IAggregator<T> _aggregator;
      private int _currentIndex;

      public IteratorImpl(IAggregator<T> aggregator)
      {
         _aggregator = aggregator;
      }

      public T First() => _aggregator[0];

      public T Next()
      {
         var returnValue = default(T);
         if (_currentIndex < _aggregator.Count - 1)
            returnValue = _aggregator[++_currentIndex];
         return returnValue;
      }

      public bool IsDone() => _currentIndex >= _aggregator.Count;

      public T CurrentItem() => _aggregator[_currentIndex];
   }
}