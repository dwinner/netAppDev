using System;
using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace Simd_Benchmark;

public class VectorScaleTest
{
   private const int ScaleFactor = 13;

   private int _length = 4;
   private int[] _sourceArray, _destinationArray;
   private Vector<int> _sourceVector, _destinationVector;

   [IterationSetup]
   public void GlobalSetup()
   {
      _length = Vector<int>.Count;
      _sourceArray = new int[_length];
      _destinationArray = new int[_length];
      var rand = new Random();
      for (var i = 0; i < _length; i++)
      {
         _sourceArray[i] = rand.Next();
      }

      _sourceVector = new Vector<int>(_sourceArray);
   }

   [Benchmark]
   public int Min_NonSIMD()
   {
      var min = int.MaxValue;
      for (var i = 0; i < _sourceArray.Length; i++)
      {
         if (_sourceArray[i] < min)
         {
            min = _sourceArray[i];
         }
      }

      return min;
   }

   [Benchmark]
   public int Min_SIMD()
   {
      var minVector = new Vector<int>(int.MaxValue);
      var i = 0;

      // Process array in chunks of the vector's length
      for (i = 0; i < _length - Vector<int>.Count; i += Vector<int>.Count)
      {
         var subArray = new Vector<int>(_sourceArray, i);
         minVector = Vector.Min(subArray, minVector);
      }

      // get min of the min vector and any leftover elements
      var min = int.MaxValue;
      for (var j = 0; j < Vector<int>.Count; j++)
      {
         min = Math.Min(min, minVector[j]);
      }

      for (i = 0; i < _sourceArray.Length; i++)
      {
         min = Math.Min(min, _sourceArray[i]);
      }

      return min;
   }

   [Benchmark]
   public void Scale_NonSIMD()
   {
      for (var i = 0; i < _sourceArray.Length; i++)
      {
         _destinationArray[i] = _sourceArray[i] * ScaleFactor;
      }
   }

   [Benchmark]
   public void Scale_SIMD()
   {
      _destinationVector = _sourceVector * ScaleFactor;
   }
}