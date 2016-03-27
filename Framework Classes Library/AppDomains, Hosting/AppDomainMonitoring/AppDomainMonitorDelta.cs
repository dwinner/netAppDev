using System;
using static System.AppDomain;
using static System.Console;
using static System.GC;

namespace AppDomainMonitoring
{
   public sealed class AppDomainMonitorDelta : IDisposable
   {
      private readonly TimeSpan _adCpu;
      private readonly long _adMemoryAllocated;
      private readonly long _adMemoryInUse;
      private readonly AppDomain _appDomain;

      static AppDomainMonitorDelta()
      {
         // Проверяем, что включен режим мониторинга домена
         MonitoringIsEnabled = true;
      }

      public AppDomainMonitorDelta()
         : this(null)
      {
      }

      public AppDomainMonitorDelta(AppDomain domain)
      {
         _appDomain = domain ?? CurrentDomain;
         _adCpu = _appDomain.MonitoringTotalProcessorTime;
         _adMemoryInUse = _appDomain.MonitoringSurvivedMemorySize;
         _adMemoryAllocated = _appDomain.MonitoringTotalAllocatedMemorySize;
      }

      public void Dispose()
      {
         Collect();
         WriteLine("Friendly name = {0}, CPU = {1}ms", _appDomain.FriendlyName,
            (_appDomain.MonitoringTotalProcessorTime - _adCpu));
         WriteLine("Allocated {0:N0} bytes of which {1:N0} survived GCs",
            _appDomain.MonitoringTotalAllocatedMemorySize - _adMemoryAllocated,
            _appDomain.MonitoringSurvivedMemorySize - _adMemoryInUse);
      }
   }
}