using System;
using System.ServiceModel;
using System.ServiceProcess;
using AutolotWcfServiceLibrary;

namespace AutolotWindowsServiceHost
{
   public partial class AutolotWinService : ServiceBase
   {
      private ServiceHost _autolotServiceHost;

      public AutolotWinService()
      {
         InitializeComponent();         
      }

      protected override void OnStart(string[] args)
      {
         if (_autolotServiceHost != null)
         {
            _autolotServiceHost.Close();
            _autolotServiceHost = null;
         }
         _autolotServiceHost = new ServiceHost(typeof(AutoLotService));
         Uri address = new Uri("http://localhost:8733/Design_Time_Addresses/AutolotWcfServiceLibrary/AutoLotService/");
         WSHttpBinding binding = new WSHttpBinding();
         Type contract = typeof (IAutoLotService);
         _autolotServiceHost.AddServiceEndpoint(contract, binding, address);
      }

      protected override void OnStop()
      {
         if (_autolotServiceHost != null)
         {
            _autolotServiceHost.Close();
         }
      }
   }
}
