using System.Collections;

namespace AdventOfCode.Common;

public static class EnumerableExtensions
{
   public static int Product(this IEnumerable<int> @this) =>
      @this.Aggregate(1, (agg, x) => agg * x);

   public static uint Product(this IEnumerable<uint> @this) =>
      @this.Aggregate(1u, (agg, x) => agg * x);

   public static long Product(this IEnumerable<long> @this) =>
      @this.Aggregate(1l, (agg, x) => agg * x);

   public static IEnumerable<T2> Scan<T1, T2>(this IEnumerable<T1> @this, T2 seed, Func<T2, T1, T2> acc)
   {
      var curr = seed;
      yield return curr;

      foreach (var i in @this)
      {
         curr = acc(curr, i);
         yield return curr;
      }
   }

   public static int FindIndex<T>(this IEnumerable<T> @this, Func<T, bool> cond)
   {
      var result = @this.Select((x, i) => (x, i)).FirstOrDefault(x => cond(x.x));
      return result.i;
   }

   public static bool ConsecutiveAny<T>(this IEnumerable<T> @this, Func<T, T, bool> condition) =>
      @this.ToArray().Map(x => x.Zip(x.Skip(1)).Select(y => condition(y.First, y.Second)).FirstOrDefault(z => z));

   public static IEnumerable<T2> ConsecutiveSelect<T1, T2>(this IEnumerable<T1> @this, Func<T1, T1, T2> f)
   {
      var thisArr = @this.ToArray();
      var thisArrCompare = thisArr.Skip(1).Append(default);
      var newEnumerable = thisArr.Zip(thisArrCompare).Select(x => f(x.First, x.Second));
      return newEnumerable;
   }

   public static T IterateUntil<T>(this T @this, Func<T, bool> end, Func<T, T> iter)
   {
      var curr = @this;

      while (!end(curr))
      {
         curr = iter(curr);
      }

      return curr;
   }

   public static IEnumerable<T2> Pair<T1, T2>(this IEnumerable<T1> @this, Func<T1, T1, T2> f) =>
      @this.Select((x, i) => (x, i / 2))
         .GroupBy(x => x.Item2)
         .Select(x => f(x.First().x, x.Last().x));
}

public static class UEnumerable
{
   public static IEnumerable<uint> URange(uint start, uint count) =>
      count == 0 ? Enumerable.Empty<uint>() : new URangeIterator(start, count);

   private sealed class URangeIterator : IEnumerable<uint>
   {
      private readonly uint _count;
      private readonly uint _start;

      public URangeIterator(uint start, uint count)
      {
         _start = start;
         _count = count;
      }

      public IEnumerator<uint> GetEnumerator() =>
         new URangeEnumerator(_start, _count);

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
   }

   private sealed class URangeEnumerator : IEnumerator<uint>
   {
      private readonly uint _count;
      private uint _iterationNum;
      private readonly uint _start;
      private bool _started;

      public URangeEnumerator(uint start, uint count)
      {
         _start = start;
         _count = count;
      }

      public bool MoveNext()
      {
         if (_iterationNum >= _count - 1)
         {
            return false;
         }

         if (_started)
         {
            _iterationNum += 1;
         }
         else
         {
            _started = true;
         }

         return true;
      }

      public void Reset()
      {
         _iterationNum = 0;
         _started = false;
      }

      public uint Current => _start + _iterationNum;

      object IEnumerator.Current => Current;

      public void Dispose()
      {
      }
   }
}