using System;
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

      public AggregatorImpl(IList<T> listItems)
      {
         _listItems = listItems;
      }

      public IIterator<T> Create()
      {
         return new IteratorImpl<T>(this);
      }

      public int Count
      {
         get { return _listItems.Count; }
         set { throw new NotImplementedException(); }
      }

      public T this[int index]
      {
         get { return _listItems[index]; }
         set { _listItems.Insert(index, value); }
      }
   }
}