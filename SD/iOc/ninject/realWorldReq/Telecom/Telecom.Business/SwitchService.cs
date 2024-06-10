using Telecom.Business.Model;

namespace Telecom.Business
{
   public class SwitchService
   {
      private readonly IStatusCollectorFactory _factory;

      public SwitchService(IStatusCollectorFactory factory) => _factory = factory;

      public string GetStatus(Switch @switch)
      {
         var collector = @switch.SupportsTcpIp
            ? _factory.GetTcpStatusCollector()
            : _factory.GetFileStatusCollector();

         return collector.GetStatus(@switch);
      }
   }
}