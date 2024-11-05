using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace SIMDTest
{
    
    public class VectorScaleTest
    {
        private int[] sourceArray, destinationArray;
        private Vector<int> sourceVector, destinationVector;
        
        private int Length = 4;
        private int ScaleFactor = 13;

        [IterationSetup]
        public void GlobalSetup()
        {
            this.Length = Vector<int>.Count;
            this.sourceArray = new int[Length];
            this.destinationArray = new int[Length];
            Random rand = new Random();
            for (int i=0;i<Length;i++)
            {
                this.sourceArray[i] = rand.Next();
            }
            this.sourceVector = new Vector<int>(this.sourceArray);
        }

        [Benchmark]
        public int Min_NonSIMD()
        {
            int min = int.MaxValue;
            for (int i=0;i<sourceArray.Length;i++)
            {
                if (sourceArray[i] < min)
                {
                    min = sourceArray[i];
                }
            }
            return min;
        }

        [Benchmark]
        public int Min_SIMD()
        {            
            var minVector = new Vector<int>(int.MaxValue);
            int i = 0;
            // Process array in chunks of the vector's length
            for (i=0; i < Length - Vector<int>.Count; i += Vector<int>.Count)
            {
                Vector<int> subArray = new Vector<int>(this.sourceArray, i);
                minVector = Vector.Min(subArray, minVector);
            }

            // get min of the min vector and any leftover elements
            int min = Int32.MaxValue;
            for (int j = 0; j < Vector<int>.Count; j++)
            {
                min = Math.Min(min, minVector[j]);
            }

            for (i = 0; i < sourceArray.Length; i++)
            {
                min = Math.Min(min, sourceArray[i]);
            }
            return min;
        }

        [Benchmark]
        public void Scale_NonSIMD()
        {
            for(int i=0; i < this.sourceArray.Length; i++)
            {
                this.destinationArray[i] = this.sourceArray[i] * ScaleFactor;
            }
        }

        [Benchmark]
        public void Scale_SIMD()
        {
            this.destinationVector = this.sourceVector * ScaleFactor;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {            
            if (!Vector.IsHardwareAccelerated)
            {
                Console.WriteLine("No hardware acceleration for Vector. Exiting.");
                return;
            }

            var summary = BenchmarkRunner.Run<VectorScaleTest>();
        }
    }
}
