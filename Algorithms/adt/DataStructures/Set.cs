using System.Collections;

namespace DataStructures;

public sealed class Set<T> : IEnumerable<T> where T : IComparable<T>
{
   private readonly List<T> _items = new();

   public Set()
   {
   }

   public Set(IEnumerable<T> items)
   {
      AddRange(items);
   }

   public int Count => _items.Count;

   public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

   public void Add(T item)
   {
      if (Contains(item))
      {
         throw new InvalidOperationException("Item already exists in Set");
      }

      _items.Add(item);
   }

   public void AddRange(IEnumerable<T> items)
   {
      foreach (var item in items)
      {
         Add(item);
      }
   }

   public bool Remove(T item) => _items.Remove(item);

   public bool Contains(T item) => _items.Contains(item);

   public Set<T> Union(Set<T> other)
   {
      var result = new Set<T>(_items);
      foreach (var item in other._items.Where(item => !Contains(item)))
      {
         result.Add(item);
      }

      return result;
   }

   public Set<T> Intersection(Set<T> other)
   {
      var result = new Set<T>();
      foreach (var item in _items.Where(item => other._items.Contains(item)))
      {
         result.Add(item);
      }

      return result;
   }

   public Set<T> Difference(Set<T> other)
   {
      var result = new Set<T>(_items);
      foreach (var item in other._items)
      {
         result.Remove(item);
      }

      return result;
   }

   public Set<T> SymmetricDifference(Set<T> other)
   {
      var union = Union(other);
      var intersection = Intersection(other);

      return union.Difference(intersection);
   }
}