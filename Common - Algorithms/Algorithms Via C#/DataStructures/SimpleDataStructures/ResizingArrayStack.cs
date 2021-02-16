using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimpleDataStructures
{
   /// <summary>
   ///    Стек переменной емкости
   /// </summary>
   public sealed class ResizingArrayStack<T> : IStack<T>
   {
      private T[] _elements;

      public ResizingArrayStack(int capacity)
      {
         _elements = new T[capacity];
      }

      public bool IsEmpty => Size == 0;

      public int Size { get; private set; }

      public void Push(T anItem) // Добавление элемента на верхушку стека
      {
         if (Size == _elements.Length)         
            Resize(2*_elements.Length);
         
         _elements[Size++] = anItem;
      }

      public T Pop() // Удаление элемента с верхушки стека
      {
         var item = _elements[--Size];
         _elements[Size] = default(T); // Гашение праздной ссылки
         if (Size > 0 && Size == _elements.Length / 4)         
            Resize(_elements.Length / 2);         

         return item;
      }

      private void Resize(int max)  // Перенос стека размером Size <= max в новый массив размером max
      {
         Debug.Assert(Size <= max, "Size <= max");         
         var temp = new T[max];         
         Array.Copy(_elements, temp, _elements.Length - 1);
         _elements = temp;
      }

      public IEnumerator<T> GetEnumerator() => new ReverseArrayIterator<T>(this);

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      private sealed class ReverseArrayIterator<TItem> : IEnumerator<TItem>
      {
         private readonly ResizingArrayStack<TItem> _resizingArrayStack;

         public ReverseArrayIterator(ResizingArrayStack<TItem> resizingArrayStack)
         {
            _resizingArrayStack = resizingArrayStack;            
         }

         public void Dispose()
         {
            // NOTE: Можно обнулить ссылки на элементы
         }

         public bool MoveNext() => _resizingArrayStack.Size > 0;

         public void Reset() => _resizingArrayStack.Size = _resizingArrayStack._elements.Length;

         public TItem Current => _resizingArrayStack._elements[--_resizingArrayStack.Size];

         object IEnumerator.Current => Current;
      }
   }
}