using System;
using System.Numerics;
using BenchmarkDotNet.Running;

namespace Simd_Benchmark;

public class Program
{
   public static void Main(string[] args)
   {
      if (!Vector.IsHardwareAccelerated)
      {
         Console.WriteLine("No hardware acceleration for Vector. Exiting.");
         return;
      }

      // If arguments are available use BenchmarkSwitcher to run benchmarks
      if (args.Length > 0)
      {
         _ = BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args, BenchmarkConfig.Get());
         return;
      }

      // Else, use BenchmarkRunner
      _ = BenchmarkRunner.Run<VectorScaleTest>(BenchmarkConfig.Get());
   }
}