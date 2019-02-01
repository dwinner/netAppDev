using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Entry
{
   public abstract class Tracer
   {
      private readonly ILogging _logging;

      protected Tracer(ILogging logging)
      {
         _logging = logging;
      }

      public virtual void TraceDelay()
      {
         InternalTrace();
      }

      private void InternalTrace([CallerMemberName] string aCallerMemberName = null)
      {
         if (aCallerMemberName == null) return;
         var callerMethod = GetType().GetMethod(aCallerMemberName);
         var logMessages =
            callerMethod.GetCustomAttributes(typeof (LogAttribute), true)
               .Cast<LogAttribute>()
               .Select(attribute => attribute.Message);
         foreach (var logMessage in logMessages)
         {
            _logging.Log(string.Format("{0}: {1}", DateTime.Now, logMessage));
         }
      }
   }

   public class TracerImpl : Tracer
   {
      public TracerImpl(ILogging logging)
         : base(logging)
      {
      }

      [Log(Message = "Tracing method")]
      public override void TraceDelay()
      {
         base.TraceDelay();
         Thread.Sleep(TimeSpan.FromSeconds(2));
      }
   }
}