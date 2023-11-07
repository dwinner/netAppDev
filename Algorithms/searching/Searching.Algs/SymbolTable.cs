/*
 * Sorted symbol table implementation using a Dictionary.
 * Does not allow duplicates.
 */

using System.Collections;
using System.Diagnostics;

namespace Searching.Algs;

/// <summary>
///    This class represents an ordered symbol table. It assumes that
///    the keys are <tt>Comparable</tt>.
///    It supports the usual <em>put</em>, <em>get</em>, <em>contains</em>,
///    and <em>remove</em> methods.
///    It also provides ordered methods for finding the <em>minimum</em>,
///    <em>maximum</em>, <em>floor</em>, and <em>ceiling</em>.
///    <p>
///       The class implements the <em>associative array</em> abstraction: when associating
///       a value with a key that is already in the table, the convention is to replace
///       the old value with the new value.
///       The class also uses the convention that values cannot be null. Setting the
///       value associated with a key to null is equivalent to removing the key.
///    </p>
///    <p>
///       This class implements the Iterable interface for compatiblity with
///       the version from
///       <em>
///          Introduction to Programming in Java: An Interdisciplinary
///          Approach
///       </em>
///       .
///    </p>
///    <p>
///       This implementation uses a balanced binary search tree.
///       The <em>put</em>, <em>contains</em>, <em>remove</em>, <em>minimum</em>,
///       <em>maximum</em>, <em>ceiling</em>, and <em>floor</em> methods take
///       logarithmic time.
///    </p>
/// </summary>
public sealed class SymbolTable<TKey, TValue> : IEnumerable<TKey>
   where TKey : IComparable<TKey>
{
   private readonly SortedDictionary<TKey, TValue> _symbolTable;

   /// <summary>
   ///    Create an empty symbol table.
   /// </summary>
   public SymbolTable() => _symbolTable = new SortedDictionary<TKey, TValue>(Comparer<TKey>.Default);

   /// <summary>
   ///    How many keys are in the table?
   /// </summary>
   public int Size => _symbolTable.Count;

   /// <summary>
   ///    Keys
   /// </summary>
   public IEnumerable<TKey> Keys => _symbolTable.Keys;

   /// <summary>
   ///    The smallest key in the table
   /// </summary>
   /// <exception cref="InvalidOperationException">If value is null</exception>
   public TKey Min => _symbolTable.Keys.FirstOrDefault()
                      ?? throw new InvalidOperationException("value is null");

   /// <summary>
   ///    The largest key in the table
   /// </summary>
   /// <exception cref="InvalidOperationException">If value is null</exception>
   public TKey Max => _symbolTable.Keys.LastOrDefault()
                      ?? throw new InvalidOperationException("value is null");

   /// <summary>
   ///    Puts key-value pair into the symbol table
   /// </summary>
   /// <param name="key">Key</param>
   /// <param name="value">Value</param>
   public TValue this[TKey key]
   {
      set
      {
         Debug.Assert(key.CompareTo(default) != 0);
         if (!_symbolTable.ContainsKey(key))
         {
            _symbolTable[key] = value;
         }
      }

      get => _symbolTable[key];
   }

   public IEnumerator<TKey> GetEnumerator() => _symbolTable.Keys.GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   /// <summary>
   ///    Delete the key (and paired value) from table
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>Value</returns>
   /// <exception cref="InvalidOperationException">Value isn't found by key</exception>
   public TValue Delete(TKey key)
   {
      _symbolTable.Remove(key, out var value);
      return value ?? throw new InvalidOperationException("value is null");
   }

   /// <summary>
   ///    Is the key in the table?
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>true if the value is in the table, false otherwise</returns>
   public bool Contains(TKey key) => _symbolTable.ContainsKey(key);

   /// <summary>
   ///    Return the smallest key in the table >= <paramref name="key" />
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>the smallest key in the table >= <paramref name="key" /></returns>
   public TKey GetCeil(TKey key)
   {
      var (ceilKey, _) = _symbolTable.MinBy(tblItem => tblItem.Key.CompareTo(key) >= 0);
      return ceilKey;
   }

   /// <summary>
   ///    Returns the largest key in the table less or equal <paramref name="key" />
   /// </summary>
   /// <param name="key">Key</param>
   /// <returns>The largest key in the table less or equal <paramref name="key" /></returns>
   public TKey GetTail(TKey key)
   {
      var (tailKey, _) = _symbolTable.MaxBy(tblItem => tblItem.Key.CompareTo(key) <= 0);
      return tailKey;
   }
}