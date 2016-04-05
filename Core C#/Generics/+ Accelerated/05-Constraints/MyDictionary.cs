/**
 * Более сложный пример с ограничениями
 */

using System;
using System.Collections.Generic;

namespace _05_Constraints
{
   class MyDictionary<TKey, TValue>
      where TKey : struct, IComparable<TKey>
      where TValue : IValue, new()
   {
      private readonly IDictionary<TKey, TValue> _imp = new Dictionary<TKey, TValue>();

      public void Add(TKey key, TValue value)
      {
         _imp.Add(key, value);
      }
   }

   interface IValue
   {
      // Методы IValue
   }
}
