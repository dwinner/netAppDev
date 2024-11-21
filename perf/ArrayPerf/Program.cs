using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayPerf
{
    public class ArrayPerfTest
    {
        private const int Size = 50;
        private int[][][] jaggedArray;
        private int[,,] multiArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.jaggedArray = new int[Size][][];
            for (int i = 0; i < Size; i++)
            {
                this.jaggedArray[i] = new int[Size][];
                for (int j=0;j<Size;j++)
                {
                    this.jaggedArray[i][j] = new int[Size];
                }
            }

            this.multiArray = new int[Size, Size, Size];
        }

        [IterationSetup]
        public void IterationSetup()
        {
            for (int i=0;i < Size;i++)
            {
                for (int j=0;j < Size;j++)
                {
                    for (int k = 0; k < Size; k++)
                    {
                        this.multiArray[i, j, k] = this.jaggedArray[i][j][k] = i * j * k;
                    }
                }
            }
        }

        [Benchmark]
        public void Negate_JaggedArray()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int k = 0; k < Size; k++)
                    {
                        this.jaggedArray[i][j][k] *= -1;
                    }
                }
            }
        }

        [Benchmark]
        public void Negate_MultiArray()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int k = 0; k < Size; k++)
                    {
                        this.multiArray[i, j, k] *= -1;
                    }
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ArrayPerfTest>();
        }
    }
}
