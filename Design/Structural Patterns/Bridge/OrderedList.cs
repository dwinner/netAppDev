using System.Collections.Generic;

namespace Bridge
{
   public sealed class OrderedList : IBaseList
   {
      private readonly IList<string> _items = new List<string>();

      public void AddItem(string item)
      {
         if (!_items.Contains(item))
         {
            _items.Add(item);
         }
      }

      public void RemoveItem(string item)
      {
         if (_items.Contains(item))
         {
            _items.Remove(item);
         }
      }

      public int NumberOfItems => _items.Count;

      public string this[int index]
      {
         get { return index >= 0 && index < _items.Count ? _items[index] : null; }
         set
         {
            if (index >= 0 && index < _items.Count)
            {
               _items[index] = value;
            }
         }
      }

      public bool SupportsOrdering => true;
   }
}