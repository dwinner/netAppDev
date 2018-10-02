using System.Collections.Generic;

namespace Iterator
{
   public class AggregatorImpl<T> : IAggregator<T>
   {
      private readonly IList<T> _listItems;

      public AggregatorImpl()
      {
         _listItems = new List<T>();
      }

      public IIterator<T> Create() => new IteratorImpl<T>(this);

      public int Count => _listItems.Count;

      public T this[int index]
      {
         get { return _listItems[index]; }
         set { _listItems.Insert(index, value); }
      }
   }
}