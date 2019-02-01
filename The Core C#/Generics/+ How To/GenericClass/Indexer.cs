using System.Collections.Generic;
using System.Linq;

namespace HowToCSharp.ch09.GenericClass
{
   class Indexer<T> where T : class
   {
      internal struct ItemStruct
      {
         public readonly string Key;
         public readonly T Value;

         public ItemStruct(string key, T value)
         {
            Key = key;
            Value = value;
         }
      };

      private readonly List<ItemStruct> _items = new List<ItemStruct>();

      internal List<ItemStruct> Items
      {
         get { return _items; }
      }

      public T Find(string key)
      {
         return (from itemStruct in Items where itemStruct.Key == key select itemStruct.Value).FirstOrDefault();
      }

      public void Add(string key, T value = default (T))
      {
         Items.Add(new ItemStruct(key, value));
      }
   }
}