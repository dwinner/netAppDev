using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace WeakReferenceCache
{
   public class HybridCache<TKey, TVal> where TVal : class
   {
      private readonly TimeSpan _maxAgeBeforeDemotion;

      // Values live here until they hit their maximum age
      private readonly ConcurrentDictionary<TKey, ValueContainer<TVal>> _strongReferences =
         new ConcurrentDictionary<TKey, ValueContainer<TVal>>();

      // Values are moved here after they hit their maximum age
      private readonly ConcurrentDictionary<TKey, WeakReference<ValueContainer<TVal>>> _weakReferences =
         new ConcurrentDictionary<TKey, WeakReference<ValueContainer<TVal>>>();

      public HybridCache(TimeSpan maxAgeBeforeDemotion) => _maxAgeBeforeDemotion = maxAgeBeforeDemotion;

      public int Count => _strongReferences.Count;

      public int WeakCount => _weakReferences.Count;

      public void Add(TKey key, TVal value)
      {
         RemoveFromWeak(key);
         var container = new ValueContainer<TVal>
         {
            Value = value,
            AdditionTime = Stopwatch.GetTimestamp(),
            DemoteTime = 0
         };
         _strongReferences.AddOrUpdate(key, container, (k, existingValue) => container);
      }

      private void RemoveFromWeak(TKey key)
      {
         _weakReferences.TryRemove(key, out _);
      }

      public bool TryGetValue(TKey key, out TVal value)
      {
         value = null;
         if (_strongReferences.TryGetValue(key, out var container))
         {
            AttemptDemotion(key, container);
            value = container.Value;
            return true;
         }

         if (_weakReferences.TryGetValue(key, out var weakRef))
         {
            if (weakRef.TryGetTarget(out container))
            {
               value = container.Value;
               return true;
            }

            RemoveFromWeak(key);
         }

         return false;
      }

      /// <summary>
      ///    Call this method periodically from another thread.
      /// </summary>
      public void DemoteOldObjects()
      {
         var demotionList = new List<KeyValuePair<TKey, ValueContainer<TVal>>>();
         var now = Stopwatch.GetTimestamp();

         foreach (var kvp in _strongReferences)
         {
            var age = CalculateTimeSpan(kvp.Value.AdditionTime, now);
            if (age > _maxAgeBeforeDemotion)
            {
               demotionList.Add(kvp);
            }
         }

         foreach (var kvp in demotionList)
         {
            Demote(kvp.Key, kvp.Value);
         }
      }

      private void AttemptDemotion(TKey key, ValueContainer<TVal> container)
      {
         var now = Stopwatch.GetTimestamp();
         var age = CalculateTimeSpan(container.AdditionTime, now);
         if (age > _maxAgeBeforeDemotion)
         {
            Demote(key, container);
         }
      }

      private void Demote(TKey key, ValueContainer<TVal> container)
      {
         _strongReferences.TryRemove(key, out _);
         container.DemoteTime = Stopwatch.GetTimestamp();
         var weakRef = new WeakReference<ValueContainer<TVal>>(container);
         _weakReferences.AddOrUpdate(key, weakRef, (k, oldRef) => weakRef);
      }

      private static TimeSpan CalculateTimeSpan(long offsetA, long offsetB)
      {
         var diff = offsetB - offsetA;
         var seconds = (double)diff / Stopwatch.Frequency;
         return TimeSpan.FromSeconds(seconds);
      }

      private class ValueContainer<T>
      {
         public long AdditionTime;
         public long DemoteTime;
         public T Value;
      }
   }
}