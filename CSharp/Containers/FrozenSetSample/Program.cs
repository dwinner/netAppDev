using System.Collections.Frozen;

// FrozenSet<T> and FrozenDictionary<K,V> are similar to ImmutableDictionary<K,V> and ImmutableHashSet<K,V>
// but lack methods for nondestructive mutation (such as Add or Remove), allowing for highly optimized read performance.

int[] numbers = { 10, 20, 30 };
var frozen = numbers.ToFrozenSet();
if (frozen.Contains(10))
{
   Console.WriteLine("Ok");
}