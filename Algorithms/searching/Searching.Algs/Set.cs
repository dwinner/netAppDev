using System.Collections;

namespace Searching.Algs;

/// <summary>
///    The <see cref="Set{TKey}" /> class represents an ordered set of comparable keys.
///    It supports the usual <em>add</em>, <em>contains</em>, and <em>delete</em>
///    methods. It also provides ordered methods for finding the <em>minimum</em>,
///    <em>maximum</em>, <em>floor</em>, and <em>ceiling</em> and set methods
///    for <em>union</em>, <em>intersection</em>, and <em>equality</em>.
///    <p>
///       Even though this implementation include the method <code>equals()</code>, it
///       does not support the method <code>hashCode()</code> because sets are mutable.
///    </p>
///    <p>
///       This implementation uses a balanced binary search tree. It requires that
///       the key type implements the <code>IComparable</code> interface and calls the
///       <code>compareTo()</code> and method to compare two keys. It does not call either
///       <code>equals()</code> or <code>hashCode()</code>.
///       The <em>add</em>, <em>contains</em>, <em>delete</em>, <em>minimum</em>,
///       <em>maximum</em>, <em>ceiling</em>, and <em>floor</em> methods take
///       logarithmic time in the worst case.
///       The <em>size</em>, and <em>is-empty</em> operations take constant time.
///       Construction takes constant time.
///    </p>
/// </summary>
/// <typeparam name="TKey">Container type</typeparam>
public sealed class Set<TKey> : IEnumerable<TKey>, IEquatable<Set<TKey>> where TKey : IComparable<TKey>
{
   private static readonly EqualityComparer<TKey> KeyComparer = EqualityComparer<TKey>.Default;
   private readonly SortedSet<TKey> _set;

   /// <summary>
   ///    Initializes an empty set.
   /// </summary>
   public Set() => _set = new SortedSet<TKey>();

   /// <summary>
   ///    Initializes a new set that is an independent copy of the specified set.
   /// </summary>
   /// <param name="set">The set to copy</param>
   public Set(Set<TKey> set) => _set = new SortedSet<TKey>(set._set);

   /// <summary>
   ///    The number of keys in this set
   /// </summary>
   public int Size => _set.Count;

   /// <summary>
   ///    true if this set is empty, false otherwise
   /// </summary>
   public bool IsEmpty => Size == 0;

   /// <summary>
   ///    The largest key in this set.
   /// </summary>
   /// <exception cref="InvalidOperationException">If set is empty</exception>
   public TKey? Max
   {
      get
      {
         EmptyPrecondition();
         return _set.Max;
      }
   }

   /// <summary>
   ///    Returns the smallest key in this set.
   /// </summary>
   /// <exception cref="InvalidOperationException">If set is empty</exception>
   public TKey? Min
   {
      get
      {
         EmptyPrecondition();
         return _set.Min;
      }
   }

   public IEnumerator<TKey> GetEnumerator() => _set.GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   public bool Equals(Set<TKey>? other) =>
      !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || _set.Equals(other._set));

   /// <summary>
   ///    Adds the key to this set (if it is not already present).
   /// </summary>
   /// <param name="key">The key to add</param>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public void Add(TKey key)
   {
      NullPrecondition(key);
      _set.Add(key);
   }

   private static void NullPrecondition<T>(T? value)
   {
      if (value == null)
      {
         throw new ArgumentNullException(nameof(value));
      }
   }

   /// <summary>
   ///    Returns true if this set contains the given key.
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>true if this set contains the given key.</returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public bool Contains(TKey key)
   {
      NullPrecondition(key);
      return _set.Contains(key);
   }

   /// <summary>
   ///    Removes the specified key from this set (if the set contains the specified key).
   /// </summary>
   /// <param name="key">The key</param>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   public void Delete(TKey key)
   {
      NullPrecondition(key);
      _set.Remove(key);
   }

   private void EmptyPrecondition()
   {
      if (IsEmpty)
      {
         throw new InvalidOperationException("Empty set");
      }
   }

   /// <summary>
   ///    Returns the smallest key in this set greater than or equal to <paramref name="key" />
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The smallest key in this set greater than or equal to <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   /// <exception cref="InvalidOperationException">All keys are less than <paramref name="key" /></exception>
   public TKey? GetCeiling(TKey key)
   {
      NullPrecondition(key);
      var ceilingKey = _set.Where(lKey => lKey.CompareTo(key) >= 0).Min();
      if (KeyComparer.Equals(ceilingKey, default))
      {
         throw new InvalidOperationException($"All keys are less than {key}");
      }

      return ceilingKey;
   }

   /// <summary>
   ///    Returns the largest key in this set less than or equal to <paramref name="key" />
   /// </summary>
   /// <param name="key">The key</param>
   /// <returns>The largest key in this set less than or equal to <paramref name="key" /></returns>
   /// <exception cref="ArgumentNullException">If <paramref name="key" /> is null</exception>
   /// <exception cref="InvalidOperationException">All keys are greater than <paramref name="key" /></exception>
   public TKey? GetFloor(TKey key)
   {
      NullPrecondition(key);
      var floorKey = _set.Where(lKey => lKey.CompareTo(key) <= 0).Max();
      if (KeyComparer.Equals(floorKey, default))
      {
         throw new InvalidOperationException($"All keys are greater than {key}");
      }

      return floorKey;
   }

   /// <summary>
   ///    Returns the union of this set and that set.
   /// </summary>
   /// <param name="thatSet">The other set</param>
   /// <returns>The union of this set and that set.</returns>
   /// <exception cref="ArgumentNullException">If <paramref name="thatSet" /> is null</exception>
   public Set<TKey> Union(Set<TKey> thatSet)
   {
      NullPrecondition(thatSet);

      var unionSet = new Set<TKey>();
      foreach (var key in this)
      {
         unionSet.Add(key);
      }

      foreach (var key in thatSet)
      {
         unionSet.Add(key);
      }

      return unionSet;
   }

   /// <summary>
   ///    Returns the intersection of this set and that set.
   /// </summary>
   /// <param name="thatSet">The other set</param>
   /// <returns>The intersection of this set and that set.</returns>
   /// <exception cref="ArgumentNullException">If <paramref name="thatSet" /> is null</exception>
   public Set<TKey> Intersects(Set<TKey> thatSet)
   {
      NullPrecondition(thatSet);

      var intersectSet = new Set<TKey>();
      if (Size < thatSet.Size)
      {
         foreach (var key in this)
         {
            if (thatSet.Contains(key))
            {
               intersectSet.Add(key);
            }
         }
      }
      else
      {
         foreach (var key in thatSet)
         {
            if (Contains(key))
            {
               intersectSet.Add(key);
            }
         }
      }

      return intersectSet;
   }

   public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is Set<TKey> other && Equals(other));

   public override int GetHashCode() => _set.GetHashCode();

   public static bool operator ==(Set<TKey>? left, Set<TKey>? right) => Equals(left, right);

   public static bool operator !=(Set<TKey>? left, Set<TKey>? right) => !Equals(left, right);

   public override string ToString()
   {
      var dump = _set.ToString();
      return dump != null
         ? string.Concat("{ ", dump.AsSpan(1, dump.Length - 1), " }")
         : string.Empty;
   }
}