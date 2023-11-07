/**
 * Невиртуальный интерфейс
 */

using System;

namespace NonVirtualInterface
{
   class NviDemo
   {
      static void Main()
      {
         Base refBase = new Derived();
         refBase.DoWork();

         Console.ReadKey();
      }
   }

   public class Base
   {
      public void DoWork()
      {
         CoreDoWork();
      }

      protected virtual void CoreDoWork()
      {
         Console.WriteLine("Base.DoWork()");
      }
   }

   public class Derived : Base
   {
      protected override void CoreDoWork()
      {
         Console.WriteLine("Derived.DoWork()");
      }
   }
}
