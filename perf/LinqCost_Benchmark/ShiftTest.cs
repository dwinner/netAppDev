using System.Linq;
using BenchmarkDotNet.Attributes;

namespace LinqCost_Benchmark;

[MemoryDiagnoser]
public class ShiftTest
{
   private const int ShiftAmount = 100;
   private int[] _vals;

   [IterationSetup]
   public void Setup()
   {
      _vals = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
   }

   [Benchmark]
   public int[] ShiftLinq()
   {
      var temp = from n in _vals select n + ShiftAmount;
      var results = temp.ToArray();
      return results;
   }

   [Benchmark]
   public void Shift()
   {
      for (var i = 0; i < _vals.Length; i++)
      {
         _vals[i] += ShiftAmount;
      }
   }
}