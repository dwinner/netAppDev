using System;
using System.Collections.Generic;

namespace LanguageHost.LSP.Helpers;

public sealed class WeakEvictionCache<TKey, TValue>
    where TValue : class
{
    private readonly Dictionary<TKey, StrongToWeakReference<TValue>> _items;
    private readonly TimeSpan _weakEvictionThreshold;

    public WeakEvictionCache(TimeSpan weakEvictionThreshold)
    {
        _weakEvictionThreshold = weakEvictionThreshold;
        _items = new Dictionary<TKey, StrongToWeakReference<TValue>>();
    }

    public void AddOrUpdate(TKey key, TValue value)
    {
        if (_items.ContainsKey(key))
        {
            _items[key].MakeWeak();
            _items.Remove(key);
        }

        _items.Add(key, new StrongToWeakReference<TValue>(value));
    }

    public bool TryGet(TKey key, out TValue result)
    {
        result = null;
        if (!_items.TryGetValue(key, out var value))
        {
            return false;
        }

        result = value.Target;
        if (result == null)
        {
            return false;
        }

        // Item is used, make it strong again
        value.MakeStrong();
        return true;
    }

    public void ApplyWeakEviction()
    {
        var toRemove = new List<TKey>();
        foreach (var strongToWeakRef in _items)
        {
            var reference = strongToWeakRef.Value;
            var target = reference.Target;
            if (target != null)
            {
                if (DateTime.Now.Subtract(reference.StrongTime) >= _weakEvictionThreshold)
                {
                    reference.MakeWeak();
                }
            }
            else
            {
                // Remove already zeroed weak references
                toRemove.Add(strongToWeakRef.Key);
            }
        }

        foreach (var key in toRemove)
        {
            _items.Remove(key);
        }
    }

    private sealed class StrongToWeakReference<T>
        : WeakReference where T : class
    {
        private T _strongRef;

        public StrongToWeakReference(T obj) : base(obj)
        {
            StrongTime = DateTime.Now;
            _strongRef = obj;
        }

        public DateTime StrongTime { get; private set; }

        public new T Target => _strongRef ?? WeakTarget;

        private T WeakTarget => base.Target as T;

        public void MakeWeak() => _strongRef = null;

        public void MakeStrong()
        {
            StrongTime = DateTime.Now;
            _strongRef = WeakTarget;
        }
    }
}