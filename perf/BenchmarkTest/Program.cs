using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;

namespace BenchmarkTest
{
    public class LoopBenchmarks
    {
        static int[] arr = new int[100];

        public LoopBenchmarks()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }
        }

        [Benchmark]
        public int ForEachOnArray()
        {
            int sum = 0;
            foreach (int val in arr)
            {
                sum += val;
            }
            return sum;
        }

        [Benchmark]
        public int ForEachOnIEnumerable()
        {
            int sum = 0;
            IEnumerable<int> arrEnum = arr;
            foreach (int val in arrEnum)
            {
                sum += val;
            }
            return sum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LoopBenchmarks>();            
        }
    }
}
