using System.Collections.Generic;

namespace Bridge
{
   /// <summary>
   /// Базовый интерфейс иерархии реализации
   /// </summary>
   public interface IBaseListImpl
   {
      void AddItem(string item);

      void RemoveItem(string item);

      int NumberOfItems { get; }

      string this[int index] { get; set; }

      bool SupportsOrdering { get; }
   }

   public class OrderedListImpl : IBaseListImpl
   {
      private readonly IList<string> _items = new List<string>();

      public void AddItem(string item)
      {
         if (!_items.Contains(item))
            _items.Add(item);
      }

      public void RemoveItem(string item)
      {
         if (_items.Contains(item))
            _items.Remove(item);
      }

      public int NumberOfItems { get { return _items.Count; } }

      public string this[int index]
      {
         get { return (index >= 0 && index < _items.Count) ? _items[index] : null; }
         set
         {
            if (index >= 0 && index < _items.Count)
               _items[index] = value;
         }
      }

      public bool SupportsOrdering { get { return true; } }
   }
}
