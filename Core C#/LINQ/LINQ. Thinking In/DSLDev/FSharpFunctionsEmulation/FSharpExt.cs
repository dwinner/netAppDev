using System;
using System.Collections.Generic;
using System.Linq;

namespace FSharpFunctionsEmulation
{
   public static class FSharpExt
   {
      /// <summary>
      ///    This method generates a list of numbers from a given seed number
      ///    and a list of functions that are used to generate the next number
      ///    one step at a time.
      /// </summary>
      /// <typeparam name="T">The type of the seed and the function arguments</typeparam>
      /// <param name="x0">The seed value</param>
      /// <param name="projectors">The step descriptions in terms of Functions</param>
      /// <returns>A list of generated elements</returns>
      public static IEnumerable<T> Scan<T>(this T x0, IEnumerable<Func<T, T>> projectors)
         where T : IEquatable<T>
      {
         var values = new List<T> { x0 };
         foreach (var func in projectors)
         {
            values.Add(func(values[values.Count - 1]));            
         }

         return values.AsEnumerable();
      }

      /// <summary>
      ///    This is same as Scan just that the functions provided are used in reverse order
      ///    while generating the elements
      ///    unlike Scan where the sequence of the functions are used as is.
      /// </summary>
      /// <typeparam name="T">The type of the collection and the seed value</typeparam>
      /// <param name="x0">The seed value</param>
      /// <param name="projectors">The step descriptions</param>
      /// <returns>A list of generated elements</returns>
      public static IEnumerable<T> ScanBack<T>(this T x0, IEnumerable<Func<T, T>> projectors)
         where T : IEquatable<T>
      {
         var values = new List<T> {x0};
         foreach (var func in projectors.Reverse())
         {
            values.Add(func(values[values.Count - 1]));            
         }

         return values.AsEnumerable();
      }

      /// <summary>
      ///    This method partitions the given collection into two parts.
      ///    The first part contains elements for which the predicate returns true.
      ///    The other part contains elements for which the predicate returns false.
      /// </summary>
      /// <typeparam name="T">The type of the collection</typeparam>
      /// <param name="collection">The collection</param>
      /// <param name="predicate">The predicate.</param>
      /// <returns>
      ///    A tuple with two ranges. The first range has the elements for
      ///    which the predicate returns true and the second part returns elements
      ///    for which the predicate returns false.
      /// </returns>
      public static Tuple<IEnumerable<T>, IEnumerable<T>> OppositePartition<T>(
         this IEnumerable<T> collection, Func<T, bool> predicate)
      {
         var array = collection as T[] ?? collection.ToArray();
         return new Tuple<IEnumerable<T>, IEnumerable<T>>(array.Where(predicate), array.Where(arg => !predicate(arg)));
      }

      /// <summary>
      ///    Applies the given action for all elements of the given collection
      /// </summary>
      /// <typeparam name="T">The type of the collection</typeparam>
      /// <param name="collection">The collection</param>
      /// <param name="action">The action to be performed</param>
      public static void Iterate<T>(this IEnumerable<T> collection, Action<T> action)
      {
         foreach (var obj in collection)
            action(obj);
      }
      
      /// <summary>
      ///    This method wraps three collections into one.
      /// </summary>
      /// <typeparam name="T1">The type of the first collection</typeparam>
      /// <typeparam name="T2">The type of the second collection</typeparam>
      /// <typeparam name="T3">The type of the third collection</typeparam>
      /// <param name="first">The first collection</param>
      /// <param name="second">The second collection</param>
      /// <param name="third">The third/last collection</param>
      /// <returns>
      ///    A list of tuples where the items of the tuples are picked from the first,
      ///    respectively.
      /// </returns>
      public static IEnumerable<Tuple<T1, T2, T3>> Zip3<T1, T2, T3>(IEnumerable<T1> first,
         IEnumerable<T2> second,
         IEnumerable<T3> third)
      {
         var firstArray = first as T1[] ?? first.ToArray();
         var secondArray = second as T2[] ?? second.ToArray();
         var thirdArray = third as T3[] ?? third.ToArray();

         var smallest = new List<int> { firstArray.Length, secondArray.Length, thirdArray.Length }.Min();
         for (var i = 0; i < smallest; i++)
         {
            yield return new Tuple<T1, T2, T3>(firstArray[i], secondArray[i], thirdArray[i]);            
         }
      }

      /// <summary>
      ///    Returns the index of the given item in the given collection
      /// </summary>
      /// <typeparam name="T">The type of the collection</typeparam>
      /// <param name="collection">The collection</param>
      /// <param name="predicate">The predicate to be used to search the given item</param>
      /// <returns>
      ///    Returns the index of the given element in the collection, else returns -1
      ///    if not found.
      /// </returns>
      public static int FindIndex<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
      {
         try
         {
            return
               collection.Select((arg, index) => new KeyValuePair<int, bool>(index, predicate(arg)))
                  .First(c => c.Value)
                  .Key;
         }
         catch (InvalidOperationException)
         {
            return -1;
         }
      }

      /// <summary>
      ///    Returns a list of consecutive items as a list of key/value pairs
      /// </summary>
      /// <typeparam name="T">The type of the input collection</typeparam>
      /// <param name="collection">The collection</param>
      /// <returns>A list of key/alue pairs</returns>
      public static IEnumerable<KeyValuePair<T, T>> Pairwise<T>(
         this IEnumerable<T> collection)
      {
         var array = collection as T[] ?? collection.ToArray();
         return array.Zip(array.Skip(1), (a, b) => new KeyValuePair<T, T>(a, b));
      }

      /// <summary>
      ///    Checks whether there is a pair of consecutive entries that matches
      ///    the given condition
      /// </summary>
      /// <typeparam name="T">The type of the collection</typeparam>
      /// <param name="collection">The collection</param>
      /// <param name="predicate">The predicate to use</param>
      /// <returns>
      ///    True if such a pair exists that matches the given predicate pairwise
      ///    else returns false
      /// </returns>
      public static bool Exists2<T>(this IEnumerable<T> collection,
         Func<T, T, bool> predicate)
      {
         var array = collection as T[] ?? collection.ToArray();
         return array.Zip(array.Skip(1), predicate).Any(c => c);
      }

      /// <summary>
      ///    Checks whether all pairwise items (taken 2 at a time) from the given collection
      ///    matches the predicate or not
      /// </summary>
      /// <typeparam name="T">The type of the collection</typeparam>
      /// <param name="collection">The collection</param>
      /// <param name="predicate">
      ///    The predicate to run against all pairwise coupled
      ///    items.
      /// </param>
      /// <returns></returns>
      public static bool ForAll2<T>(this IEnumerable<T> collection,
         Func<T, T, bool> predicate)
      {
         var array = collection as T[] ?? collection.ToArray();
         return array.Zip(array.Skip(1), predicate).All(b => b);
      }

      /// <summary>
      ///    Finds intersection of several collections
      /// </summary>
      /// <typeparam name="T">type of these collections</typeparam>
      /// <param name="sets">all collections</param>
      /// <returns>
      ///    A list with all the elements that appear in the intersection of
      ///    all these collections
      /// </returns>
      public static IEnumerable<T> IntersectMany<T>(this IEnumerable<IEnumerable<T>> sets)
         where T : IComparable
      {
         var arraySet = sets as IEnumerable<T>[] ?? sets.ToArray();
         var temp = new HashSet<T>(arraySet.ElementAt(0));
         arraySet.ToList().ForEach(enm => temp = new HashSet<T>(enm.Intersect(temp)));
         return temp;
      }

      /// <summary>
      ///    Finds the union of several collections.
      /// </summary>
      /// <typeparam name="T">The type of these collections</typeparam>
      /// <param name="sets">All the collections, not just sets</param>
      /// <returns>A collection of elements with all the elements in the total union</returns>
      public static IEnumerable<T> UnionMany<T>(this IEnumerable<IEnumerable<T>> sets)
         where T : IComparable
      {
         var allValues = new HashSet<T>();
         sets.SelectMany(enmr => enmr.ToArray()).ToList().ForEach(item => allValues.Add(item));
         return allValues;
      }
   }
}