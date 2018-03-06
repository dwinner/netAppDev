using System;
using System.Threading;
using PostSharp.Aspects;

namespace ThreadingInterception
{
   [Serializable]
   public class WorkerThread : MethodInterceptionAspect
   {
      public override void OnInvoke(MethodInterceptionArgs args)
      {
         var aThread = new Thread(args.Proceed);
         aThread.Start();
      }
   }
}