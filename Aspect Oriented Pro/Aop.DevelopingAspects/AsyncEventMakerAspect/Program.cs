// Асинхронные события

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace AsyncEventMakerAspect
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   [Serializable]
   public sealed class AsyncEventAttribute : EventInterceptionAspect
   {
      public override void OnInvokeHandler(EventInterceptionArgs args) // Вызываем событие асинхронно
      {
         Task.Factory.StartNew(() => Invoke(args)).Start();
      }

      private static void Invoke(EventInterceptionArgs args)
      {
         try
         {
            args.ProceedInvokeHandler();
         }
         catch (Exception e)
         {
            Trace.TraceError(e.ToString());
            args.ProceedRemoveHandler();
         }
      }
   }
}