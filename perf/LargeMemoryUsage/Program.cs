using System;

namespace LargeMemoryUsage
{
   internal class Program
   {
      private const int ArraySize = 1000;
      private static readonly object[] staticArray = new object[ArraySize];

      private static void Main(string[] args)
      {
         var localArray = new object[ArraySize];

         var rand = new Random();
         for (var i = 0; i < ArraySize; i++)
         {
            staticArray[i] = GetNewObject(rand.Next(0, 4));
            localArray[i] = GetNewObject(rand.Next(0, 4));
         }

         Console.WriteLine("Examine heap now. Press any key to exit...");
         Console.ReadKey();

         // This will prevent localArray from being garbage collected before you take the snapshot
         Console.WriteLine(staticArray.Length);
         Console.WriteLine(localArray.Length);
      }

      private static Base GetNewObject(int type)
      {
         Base obj = null;
         switch (type)
         {
            case 0:
               obj = new A();
               break;
            case 1:
               obj = new B();
               break;
            case 2:
               obj = new C();
               break;
            case 3:
               obj = new D();
               break;
         }

         return obj;
      }
   }

   internal class Base
   {
      private byte[] memory;
      protected Base(int size) => memory = new byte[size];
   }

   internal class A : Base
   {
      public A() : base(1000)
      {
      }
   }

   internal class B : Base
   {
      public B() : base(10000)
      {
      }
   }

   internal class C : Base
   {
      public C() : base(100000)
      {
      }
   }

   internal class D : Base
   {
      public D() : base(1000000)
      {
      }
   }
}