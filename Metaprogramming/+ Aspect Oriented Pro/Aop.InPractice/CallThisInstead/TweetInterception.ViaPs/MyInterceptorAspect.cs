using System;
using PostSharp.Aspects;

namespace TweetInterception.ViaPs
{
   [Serializable]
   public class MyInterceptorAspect : MethodInterceptionAspect
   {
      public override void OnInvoke(MethodInterceptionArgs args)
      {
         Console.WriteLine("Interceptor 1");
         args.Proceed();
         Console.WriteLine("Interceptor 2");
      }
   }
}