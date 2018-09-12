using System;
using System.Collections.Generic;

namespace BatchEvents
{
   /// <summary>
   /// Контейнер, способный генерировать события "пакетного" характера
   /// </summary>
   /// <typeparam name="T">Тип хранящихся элементов</typeparam>
   public class BatchCollection<T>
   {
      private readonly List<T> _data = new List<T>();
      private readonly List<T> _updatedItems = new List<T>();
      private int _updateCount;

      public event EventHandler<ItemAddedEventArgs<T>> ItemsAdded;

      private void OnItemsAdded(T item)
      {
         if (!IsUpdating)
         {
            if (ItemsAdded != null)
            {
               ItemsAdded(this, new ItemAddedEventArgs<T>(item));
            }
         }
         else
         {
            _updatedItems.Add(item);
         }
      }

      private void FireQueuedEvents()
      {
         if (IsUpdating || _updatedItems.Count <= 0)
         {
            return;
         }

         // Аргументы события могут содержать несколько элементов
         var args = new ItemAddedEventArgs<T>();
         foreach (var item in _updatedItems)
         {
            args.Add(item);
         }
         _updatedItems.Clear();
         if (ItemsAdded != null)
         {
            ItemsAdded(this, args);
         }
      }

      private bool IsUpdating
      {
         get { return _updateCount > 0; }
      }

      public void BeginUpdate()
      {
         /* NOTE: Будем вести подсчет на тот случай, если несколько клиентов
          * вызовут метод BeginUpdate, или он будет вызван рекурсивно.
          * Заметьте, однако, что этот класс не является потокобезопасным. */
         ++_updateCount;
      }

      public void EndUpdate()
      {
         if (--_updateCount == 0)
         {
            // Генерируется только по окончании всех обновлений
            FireQueuedEvents();
         }
      }

      public void Add(T item)
      {
         _data.Add(item);
         OnItemsAdded(item);
      }
   }
}
