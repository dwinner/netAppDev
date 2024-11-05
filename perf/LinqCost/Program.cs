using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCost
{
    class Program
    {
        static void Main(string[] args)
        {            
            var summary = BenchmarkRunner.Run<ShiftTest>();

        }

        // These aren't used, just included for IL analysis purposes.

        public static int[] ShiftLinq(int[] vals, int shiftAmount)
        {
            var temp = from n in vals select n + shiftAmount;
            return temp.ToArray();
        }

        public static void Shift(int[] vals, int shiftAmount)
        {
            for (int i = 0; i < vals.Length; i++)
            {                
                vals[i] += shiftAmount;                
            }
        }
    }

    [MemoryDiagnoser]
    
    public class ShiftTest
    {
        int[] vals;
        const int ShiftAmount = 100;

        [IterationSetup]
        public void Setup()
        {
            this.vals = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        [Benchmark]
        public int[] ShiftLinq()
        {
            var temp = from n in vals select n + ShiftAmount;
            int[] results = temp.ToArray();
            return results;
        }

        [Benchmark]
        public void Shift()
        {
            for (int i=0;i<vals.Length;i++)
            {                
                 vals[i] += ShiftAmount;
            }
        }        
    }
}
