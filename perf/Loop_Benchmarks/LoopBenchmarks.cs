using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Loop_Benchmarks;

public class LoopBenchmarks
{
   private static readonly int[] _Arr = new int[100];

   public LoopBenchmarks()
   {
      for (var i = 0; i < _Arr.Length; i++)
      {
         _Arr[i] = i;
      }
   }

   [Benchmark]
   public int ForEachOnArray()
   {
      var sum = 0;
      foreach (var val in _Arr)
      {
         sum += val;
      }

      return sum;
   }

   [Benchmark]
   public int ForEachOnIEnumerable()
   {
      var sum = 0;
      IEnumerable<int> arrEnum = _Arr;
      foreach (var val in arrEnum)
      {
         sum += val;
      }

      return sum;
   }
}