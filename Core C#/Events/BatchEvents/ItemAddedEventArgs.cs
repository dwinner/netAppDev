using System;
using System.Collections.Generic;

namespace BatchEvents
{
   /// <summary>
   /// Аргументы события, объединяющие элементы в коллекцию
   /// </summary>
   /// <typeparam name="T">Тип добавляемых элементов</typeparam>
   public class ItemAddedEventArgs<T> : EventArgs
   {
      private readonly IList<T> _items = new List<T>();

      public IList<T> Items { get { return _items; } }

      public ItemAddedEventArgs()
      {
      }

      public ItemAddedEventArgs(T item)
      {
         Add(item);
      }

      public void Add(T item)
      {
         _items.Add(item);
      }
   }
}
