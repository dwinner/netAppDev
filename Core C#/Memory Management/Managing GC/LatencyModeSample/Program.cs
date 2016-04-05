// Режим срочных операций (который "обходит" сборку мусора для поколения 2)

using System.Runtime;
using System.Runtime.CompilerServices;

namespace _13_LatencyModeSample
{
   internal static class Program
   {
      private static void Main()
      {
      }

      private static void LowLatencyDemo()
      {
         var oldMode = GCSettings.LatencyMode;
         RuntimeHelpers.PrepareConstrainedRegions();
         try
         {
            GCSettings.LatencyMode = GCLatencyMode.LowLatency; // или GCLatencyMode.SustainedLowLatency
            // Здесь выполняется "срочный" код
         }
         finally
         {
            GCSettings.LatencyMode = oldMode;
         }
      }
   }
}