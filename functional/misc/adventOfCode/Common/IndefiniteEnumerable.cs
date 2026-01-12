using System.Collections;

namespace AdventOfCode.Common;

public class IndefiniteEnumerator<T> : IEnumerator<T>
{
   private readonly Func<int, T> _iteration;
   private int _pos = -1;

   public IndefiniteEnumerator(Func<int, T> iteration) => _iteration = iteration;


   public bool MoveNext()
   {
      _pos += 1;
      Current = _iteration(_pos);
      return true;
   }

   public void Reset()
   {
      Current = default;
      _pos = 0;
   }

   public T Current { get; private set; }

   object IEnumerator.Current => Current;

   public void Dispose()
   {
   }
}

public class IndefiniteEnumerable<T> : IEnumerable<T>
{
   private readonly Func<int, T> _iteration;

   public IndefiniteEnumerable(Func<int, T> iteration) => _iteration = iteration;

   public IEnumerator<T> GetEnumerator() => new IndefiniteEnumerator<T>(_iteration);

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}