using System;

namespace InParameterSample
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("Hello World!");
         var sampleValueType = new SampleValueType { data = 13 };
         CantChangeIt(sampleValueType);
      }

      private static void CantChangeIt(in SampleValueType sampleValueType)
      {
         // var valueType = sampleValueType;
         // valueType.Data = 42;
         Console.WriteLine(sampleValueType.data);
      }
   }

   internal struct SampleValueType
   {
      public int data;
   }
}