using System;

namespace PsLazyLoading
{
   public class MyService : IMyService
   {
      public void DoSomething() => Console.WriteLine("Hello, it's {0}", DateTime.Now);
   }
}