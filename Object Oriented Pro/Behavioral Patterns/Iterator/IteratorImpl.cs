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

      public T First()
      {
         return _aggregator[0];
      }

      public T Next()
      {
         T returnValue = default(T);
         if (_currentIndex < _aggregator.Count - 1)
            returnValue = _aggregator[++_currentIndex];
         return returnValue;
      }

      public bool IsDone()
      {
         return _currentIndex >= _aggregator.Count;
      }

      public T CurrentItem()
      {
         return _aggregator[_currentIndex];
      }
   }
}