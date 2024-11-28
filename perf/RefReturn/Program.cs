using System;
using System.Diagnostics;

namespace RefReturn
{
   internal static class Program
   {
      private const int ArraySize = 5;
      private const int NumIterations = 10_000_000;

      private static readonly Random _Rand = new Random();

      private static void Main()
      {
         var arr = GenerateRandomArray(ArraySize);
         ZeroMiddleValue(arr);

         RefZeroMiddleValue(arr);

         // We can also now do things like assigning values to methods
         GetRefToMiddle(arr) = 0;

         var vector = new Vector { Location = new Point3d { X = 1, Y = 2, Z = 3 }, Magnitude = 2 };

         SetVectorToOrigin(vector);
         RefSetVectorToOrigin(vector);

         Console.WriteLine("Benchmarks:");

         BenchmarkSetVectorToOrigin();

         BenchmarkRefSetVectorToOrigin();
      }

      #region Ref Array Access

      private static void ZeroMiddleValue(int[] arr)
      {
         var midIndex = GetMidIndex(arr);
         arr[midIndex] = 0;
      }

      private static int GetMidIndex(int[] arr) => arr.Length / 2;

      private static void RefZeroMiddleValue(int[] arr)
      {
         ref var middle = ref GetRefToMiddle(arr);
         middle = 0;
      }

      private static ref int GetRefToMiddle(int[] arr) => ref arr[arr.Length / 2];

      private static int[] GenerateRandomArray(int arraySize)
      {
         var arr = new int[arraySize];
         for (var i = 0; i < arraySize; i++)
         {
            arr[i] = _Rand.Next();
         }

         return arr;
      }

      #endregion

      #region Ref Struct Access

      private static void SetVectorToOrigin(Vector vector)
      {
         var pt = vector.Location;
         pt.X = 0;
         pt.Y = 0;
         pt.Z = 0;
         vector.Location = pt;
      }

      private static void RefSetVectorToOrigin(Vector vector)
      {
         ref var location = ref vector.RefLocation;
         location.X = 0;
         location.Y = 0;
         location.Z = 0;
      }

      private static void BenchmarkSetVectorToOrigin()
      {
         var vector = new Vector { Location = new Point3d { X = 1, Y = 2, Z = 3 }, Magnitude = 2 };

         var watch = Stopwatch.StartNew();
         for (var i = 0; i < NumIterations; i++)
         {
            SetVectorToOrigin(vector);
         }

         watch.Stop();
         Console.WriteLine($"SetVectorsToOrigin: {watch.ElapsedMilliseconds}ms");
      }

      private static void BenchmarkRefSetVectorToOrigin()
      {
         var vector = new Vector { Location = new Point3d { X = 1, Y = 2, Z = 3 }, Magnitude = 2 };

         var watch = Stopwatch.StartNew();
         for (var i = 0; i < NumIterations; i++)
         {
            RefSetVectorToOrigin(vector);
         }

         watch.Stop();

         Console.WriteLine($"RefSetVectorsToOrigin: {watch.ElapsedMilliseconds}ms");
      }

      #endregion
   }
}