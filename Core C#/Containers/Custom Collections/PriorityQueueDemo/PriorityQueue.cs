using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PriorityQueueDemo
{
   /*
    * Обычно очереди с приоритетами реализуются в виде куч,
    * но при этом возникает проблема с перебором элементов структуры,
    * поскольку куча не поддерживает строгое упорядочивание.
    * Здесь приводится альтернативная реализация с использованием стандартных структур данных.
    */
   class PriorityQueue<TPriority, TObject> : ICollection, IEnumerable<TObject>
   {
      private readonly SortedDictionary<TPriority, Queue<TObject>> _elements;

      // Те же конструкторы, что и у класса Queue<T>

      public PriorityQueue()
      {
         _elements = new SortedDictionary<TPriority, Queue<TObject>>();
      }

      public PriorityQueue(IComparer<TPriority> comparer)
      {
         _elements = new SortedDictionary<TPriority, Queue<TObject>>(comparer);
      }

      public PriorityQueue(PriorityQueue<TPriority, TObject> queue)
         : this()
      {
         foreach (var pair in queue._elements)
         {
            _elements.Add(pair.Key, new Queue<TObject>(pair.Value));
         }
      }

      public PriorityQueue(PriorityQueue<TPriority, TObject> queue, IComparer<TPriority> comparer)
         : this(comparer)
      {
         foreach (var pair in queue._elements)
         {
            _elements.Add(pair.Key, new Queue<TObject>(pair.Value));
         }
      }

      public void Enqueue(TPriority priority, TObject item)
      {
         Queue<TObject> queue;
         if (!_elements.TryGetValue(priority, out queue))
         {
            queue = new Queue<TObject>();
            _elements[priority] = queue;
         }
         queue.Enqueue(item);
      }

      public TObject Dequeue()
      {
         if (_elements.Count == 0)         
            throw new InvalidOperationException("The priority queue is empty");
         
         SortedDictionary<TPriority, Queue<TObject>>.Enumerator enumerator = _elements.GetEnumerator();
         enumerator.MoveNext(); // Должно работать, т.к. мы уже выяснили, что имеется хотя бы один элемент

         Queue<TObject> queue = enumerator.Current.Value;
         TObject obj = queue.Dequeue();
         if (queue.Count == 0)   // Нужно обязательно удалять пустые очереди
         {
            _elements.Remove(enumerator.Current.Key);
         }
         return obj;
      }

      public IEnumerator<TObject> GetEnumerator()
      {         
         //foreach (var pair in _elements)
         //{
         //   foreach (TObject obj in pair.Value)
         //   {
         //      yield return obj;
         //   }
         //}         
         return _elements.SelectMany(pair => pair.Value).GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return (this as IEnumerable<TObject>).GetEnumerator();
      }

      public void CopyTo(Array array, int index)
      {
         if (array == null)         
            throw new ArgumentNullException("array");         
         if (index < 0)         
            throw new ArgumentOutOfRangeException("index");         
         if (array.Rank != 1)         
            throw new ArgumentException("Array needs to be of rank 1", "array");         
         if (Count + index > array.Length)         
            throw new ArgumentException("There is not enough space in the array", "array");         

         int currentIndex = index;
         //foreach (var pair in _elements)
         //{
         //   foreach (TObject obj in pair.Value)
         //   {
         //      array.SetValue(obj, currentIndex++);
         //   }
         //}
         foreach (TObject obj in _elements.SelectMany(pair => pair.Value))
         {
            array.SetValue(obj, currentIndex++);
         }
      }

      public int Count
      {
         get
         {
            return _elements.Values.Sum(queue => queue.Count);
         }
      }

      public bool IsSynchronized
      {
         get { return (_elements as ICollection).IsSynchronized; }
      }

      public object SyncRoot
      {
         get { return (_elements as ICollection).SyncRoot; }
      }
   }
}
