using System;
using System.Threading;

namespace PsLazyLoading
{
   public class SlowConstructor
   {
      private readonly IMyService _myService;

      public SlowConstructor(IMyService myService)
      {
         _myService = myService;
         Console.WriteLine("Doing something slow");
         Thread.Sleep(TimeSpan.FromSeconds(5));
      }

      public void DoSomething() => _myService.DoSomething();
   }
}