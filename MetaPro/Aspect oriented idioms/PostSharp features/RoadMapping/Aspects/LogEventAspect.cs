using System;
using PostSharp.Aspects;

namespace MethodBoundarySample.Aspects
{
   [Serializable]   
   public sealed class LogEventAspect : EventInterceptionAspect
   {
      public override void OnAddHandler(EventInterceptionArgs args)
      {
         Console.WriteLine("Event {0} added", args.Event.Name);
         args.ProceedAddHandler();
      }

      public override void OnRemoveHandler(EventInterceptionArgs args)
      {
         Console.WriteLine("Event {0} removed", args.Event.Name);
         args.ProceedRemoveHandler();
      }

      public override void OnInvokeHandler(EventInterceptionArgs args)
      {
         Console.WriteLine("Event {0} invoked", args.Event.Name);
         args.ProceedInvokeHandler();
      }
   }
}