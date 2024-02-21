using System;
using System.Collections.Generic;
using System.Linq;

namespace WeakRefCache
{
   public class WeakCache<TKey, TValue>
      where TValue : class
   {
      // Имейте дело с объектами типа WeakReference, а не TValue,
      // чтобы они были доступны сборщику мусора

      private readonly IDictionary<TKey, WeakReference> _cache = new Dictionary<TKey, WeakReference>();

      public void Add(TKey key, TValue value)
      {
         _cache[key] = new WeakReference(value);
      }

      public void Clear()
      {
         _cache.Clear();
      }

      // Поскольку ненужные объекты WeakReferenece могут накапливаться,
      // время от времени у вас будет возникать желание уничтожить их

      public void ClearDeadReferences()
      {
         foreach (KeyValuePair<TKey, WeakReference> keyValuePair
            in _cache.Where(keyValuePair => !keyValuePair.Value.IsAlive))
         {
            _cache.Remove(keyValuePair.Key);
         }
      }

      /* Note:
             Не начинайте с проверки IsAlive, поскольку сборщик мусора
             все равно может запуститься сразу после нее.
             Просто прочитайте значение и приведите его к нужному типу.
             Если объект уже уничтожен сборщиком мусора, вы получите null.
             Если вы получили значение Target, можете создать сильную ссылку
             и не допустить уничтожение объекта
      */

      public TValue GetObject(TKey key)
      {
         WeakReference reference;
         return _cache.TryGetValue(key, out reference) ? reference.Target as TValue : null;
      }
   }
}