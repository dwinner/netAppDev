using System;

namespace MethodOverride
{
   class Base
   {
      public virtual void DoSomethingVirtual()
      {
         Console.WriteLine("Base.DoSomethingVirtual   ");
      }

      public void DoSomethingNonVirtual()
      {
         Console.WriteLine("Base.DoSomethingNonVirtual   ");
      }
   }

   class Derived : Base
   {
      public override void DoSomethingVirtual()
      {
         Console.WriteLine("Derived.DoSomethingVirtual   ");
      }

      public new void DoSomethingNonVirtual()
      {
         Console.WriteLine("Derived.DoSomethingNonVirtual");
      }
   }

   class Program
   {
      static void Main()
      {
         Console.WriteLine("Derived via Base reference:");

         Base baseRef = new Derived();
         baseRef.DoSomethingVirtual(); // Derived.DoSomethingVirtual
         baseRef.DoSomethingNonVirtual(); // Base.DoSomethingNonVirtual

         Console.WriteLine();
         Console.WriteLine("Derived via Derived reference:");

         var derivedRef = new Derived();
         derivedRef.DoSomethingVirtual(); // Derived.DoSomethingVirtual
         derivedRef.DoSomethingNonVirtual(); // Derived.DoSomethingNonVirtual

         Console.ReadKey();
      }
   }
}
