/**
 * Аспектное логирование
 */

namespace Entry
{
   internal static class Program
   {
      private static void Main()
      {
         ILogging logging = new LoggingImpl();
         Tracer tracer = new TracerImpl(logging);
         tracer.TraceDelay();
      }
   }
}