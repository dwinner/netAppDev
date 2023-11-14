using System;

namespace DelegatesAndLambdas
{
   internal static class LazyLoadExample
   {
      private class HeavyClass
      {
         //this is a heavy class that take long time to create
      }

      private class ThinClass
      {
         private HeavyClass _heavy;

         public HeavyClass TheHeavy => _heavy ?? (_heavy = new HeavyClass());

         public void SomeMethod()
         {
            var myHeavy = TheHeavy;

            //Rest of code the use myHeavy
         }
      }

      private class ClassWithLazy
      {
         private readonly Lazy<HeavyClass> _lazyHeavyClass = new Lazy<HeavyClass>(() =>
         {
            var heavy = new HeavyClass();

            //code that initialize the heavy object

            return heavy;
         });

         public void SomeMethod()
         {
            var myHeavy = _lazyHeavyClass.Value;

            //Rest of code the use myHeavy
         }
      }
   }
}