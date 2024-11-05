using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace WeakReferenceCache
{
    public class HybridCache<TKey, TValue>  where TValue : class
    {
        class ValueContainer<T>
        {
            public T value;
            public long additionTime;
            public long demoteTime;
        }

        private readonly TimeSpan maxAgeBeforeDemotion;

        // Values live here until they hit their maximum age
        private readonly ConcurrentDictionary<TKey, ValueContainer<TValue>> strongReferences =
            new ConcurrentDictionary<TKey, ValueContainer<TValue>>();

        // Values are moved here after they hit their maximum age
        private readonly ConcurrentDictionary<TKey, WeakReference<ValueContainer<TValue>>> weakReferences =
            new ConcurrentDictionary<TKey, WeakReference<ValueContainer<TValue>>>();

        public int Count
        {
            get { return this.strongReferences.Count; }
        }

        public int WeakCount
        {
            get
            {
                return this.weakReferences.Count;
            }
        }

        public HybridCache(TimeSpan maxAgeBeforeDemotion)
        {
            this.maxAgeBeforeDemotion = maxAgeBeforeDemotion;
        }

        public void Add(TKey key, TValue value)
        {
            RemoveFromWeak(key);
            var container = new ValueContainer<TValue>();
            container.value = value;
            container.additionTime = Stopwatch.GetTimestamp();
            container.demoteTime = 0;
            this.strongReferences.AddOrUpdate(key, container, (k, existingValue) => container);
        }

        private void RemoveFromWeak(TKey key)
        {
            WeakReference<ValueContainer<TValue>> oldValue;
            weakReferences.TryRemove(key, out oldValue);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = null;
            ValueContainer<TValue> container;
            if (this.strongReferences.TryGetValue(key, out container))
            {
                AttemptDemotion(key, container);
                value = container.value;
                return true;
            }

            WeakReference<ValueContainer<TValue>> weakRef;
            if (this.weakReferences.TryGetValue(key, out weakRef))
            {
                if (weakRef.TryGetTarget(out container))
                {
                    value = container.value;
                    return true;
                }
                else
                {
                    RemoveFromWeak(key);
                }
            }
            return false;
        }

        /// <summary>
        /// Call this method periodically from another thread.
        /// </summary>
        public void DemoteOldObjects()
        {
            var demotionList = new List<KeyValuePair<TKey, ValueContainer<TValue>>>();
            long now = Stopwatch.GetTimestamp();

            foreach (var kvp in this.strongReferences)
            {
                var age = CalculateTimeSpan(kvp.Value.additionTime, now);
                if (age > this.maxAgeBeforeDemotion)
                {
                    demotionList.Add(kvp);
                }
            }

            foreach (var kvp in demotionList)
            {
                Demote(kvp.Key, kvp.Value);
            }
        }

        private void AttemptDemotion(TKey key, ValueContainer<TValue> container)
        {
            long now = Stopwatch.GetTimestamp();
            var age = CalculateTimeSpan(container.additionTime, now);
            if (age > this.maxAgeBeforeDemotion)
            {
                Demote(key, container);
            }
        }

        private void Demote(TKey key, ValueContainer<TValue> container)
        {
            ValueContainer<TValue> oldContainer;
            this.strongReferences.TryRemove(key, out oldContainer);
            container.demoteTime = Stopwatch.GetTimestamp();
            var weakRef = new WeakReference<ValueContainer<TValue>>(container);
            this.weakReferences.AddOrUpdate(key, weakRef, (k, oldRef) => weakRef);
        }

        private static TimeSpan CalculateTimeSpan(long offsetA, long offsetB)
        {
            long diff = offsetB - offsetA;
            double seconds = (double)diff / Stopwatch.Frequency;
            return TimeSpan.FromSeconds(seconds);
        }
    }
}