using System;
using System.Diagnostics;

namespace CastingPerf
{
   internal static class Program
   {
      private const int NumIterations = 1_000_000;

      private static void Main()
      {
         var watch = new Stopwatch();

         // Ensure everything is jitted before we record times
         new A().GetValue();
         new B().GetValue();
         new C().GetValue();
         new D().GetValue();
         new NotAnA();
         TimeTest("JIT (ignore)", () => { }, 0);

         var aNoCast = new A();
         var baseline = TimeTest("No cast", () =>
         {
            var aNo = aNoCast;
            aNo.GetValue();
         }, 0);

         // UP CASTING
         var b = new B();
         TimeTest("Up cast (1 gen)", () =>
         {
            A a = b;
            a.GetValue();
         }, baseline);

         var c = new C();
         TimeTest("Up cast (2 gens)", () =>
         {
            A a = c;
            a.GetValue();
         }, baseline);

         var d = new D();
         TimeTest("Up cast (3 gens)", () =>
         {
            A a = d;
            a.GetValue();
         }, baseline);

         // DOWN CASTING
         A ab = new B();
         TimeTest("Down cast (1 gen)", () =>
         {
            var down = (B)ab;
            down.GetValue();
         }, baseline);

         A ac = new C();
         TimeTest("Down cast (2 gens)", () =>
         {
            var down = (C)ac;
            down.GetValue();
         }, baseline);

         A da = new D();
         TimeTest("Down cast (3 gens)", () =>
         {
            var down = (D)da;
            down.GetValue();
         }, baseline);

         // INTERFACE
         A aid = new D();
         TimeTest("Interface", () =>
         {
            var id = (I)aid;
            id.GetValue();
         }, baseline);

         // BAD CAST

         object notA = new NotAnA();
         TimeTest("Invalid Cast", () =>
         {
            try
            {
               var oops = (A)notA;
               oops.GetValue();
            }
            catch (InvalidCastException)
            {
            }
         }, baseline);

         TimeTest("as (success)", () =>
         {
            var down = da as D;
            d.GetValue();
         }, baseline);
         TimeTest("as (failure)", () =>
         {
            var oops = notA as D;
            if (oops != null)
            {
               oops.GetValue();
            }
         }, baseline);
         TimeTest("is (success)", () =>
         {
            var success = da is D;
            success.GetHashCode();
         }, baseline);
         TimeTest("is (failure)", () =>
         {
            var success = da is D;
            success.GetHashCode();
         }, baseline);
      }

      private static long TimeTest(string title, Action action, long baseline)
      {
         var watch = new Stopwatch();
         watch.Start();
         for (var i = 0; i < NumIterations; i++)
         {
            action();
         }

         watch.Stop();
         var ticks = watch.ElapsedTicks;
         Console.WriteLine("{0}: {1:F2}x", title, (double)ticks / (baseline == 0 ? ticks : baseline));
         return ticks;
      }

      private class A
      {
         public char GetValue() => 'A';
      }

      private interface I
      {
         char GetValue();
      }

      private class B : A
      {
      }

      private class C : B
      {
      }

      private class D : C, I
      {
      }

      private class NotAnA
      {
      }
   }
}