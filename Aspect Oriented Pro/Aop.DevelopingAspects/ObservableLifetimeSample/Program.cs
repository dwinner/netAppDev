// Прозрачное введение шаблонов проектирования

using System;
using PostSharp;

namespace ObservableLifetimeSample
{
   internal static class Program
   {
      private static void Main()
      {
         using (var f1 = new DomainObject("f1"))
         {
            var fe1 = Post.Cast<DomainObject, IObservableLifetime>(f1);
            fe1.Finalized += OnFinalized;
            f1.Dispose();
         }

         var f2 = new DomainObject("f2");
         var fe2 = Post.Cast<DomainObject, IObservableLifetime>(f2);
         fe2.Finalized += OnFinalized;
         f2 = null;
         fe2 = null;
         GC.Collect();
         GC.WaitForPendingFinalizers();
      }

      private static void OnFinalized(object sender, EventArgs e) => Console.WriteLine("OnFinalized: {0}", sender);
   }
}