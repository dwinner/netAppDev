using System;

namespace SpringAopExample
{
   public class ClassWithData
      : IClassWithData
   {
      public ClassWithData()
      {
      }

      public ClassWithData(Guid data) => Data = data;

      public Guid GetData() => Data;

      public Guid Data { get; }
   }
}