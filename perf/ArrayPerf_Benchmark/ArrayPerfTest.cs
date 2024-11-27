using BenchmarkDotNet.Attributes;

/*
 * PERF: Keep in mind, that the fastest way is to have
 * a flat structure with locical offset, i.e. when array[row][col]
 * is just a flat of [i*row + col], where col is an offset
 */

namespace ArrayPerf_Benchmark;

public class ArrayPerfTest
{
   private const int Size = 50;
   private int[][][] _jaggedArray;
   private int[,,] _multiArray;

   [GlobalSetup]
   public void GlobalSetUp()
   {
      _jaggedArray = new int[Size][][];
      for (var i = 0; i < Size; i++)
      {
         _jaggedArray[i] = new int[Size][];
         for (var j = 0; j < Size; j++)
         {
            _jaggedArray[i][j] = new int[Size];
         }
      }

      _multiArray = new int[Size, Size, Size];
   }

   [IterationSetup]
   public void IterationSetUp()
   {
      for (var i = 0; i < Size; i++)
      {
         for (var j = 0; j < Size; j++)
         {
            for (var k = 0; k < Size; k++)
            {
               _multiArray[i, j, k] = _jaggedArray[i][j][k] = i * j * k;
            }
         }
      }
   }

   [Benchmark]
   public void Negate_JaggedArray()
   {
      for (var i = 0; i < Size; i++)
      {
         for (var j = 0; j < Size; j++)
         {
            for (var k = 0; k < Size; k++)
            {
               _jaggedArray[i][j][k] *= -1;
            }
         }
      }
   }

   [Benchmark]
   public void Negate_MultiArray()
   {
      for (var i = 0; i < Size; i++)
      {
         for (var j = 0; j < Size; j++)
         {
            for (var k = 0; k < Size; k++)
            {
               _multiArray[i, j, k] *= -1;
            }
         }
      }
   }
}