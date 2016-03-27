using System;
using System.ServiceModel;
using System.ServiceProcess;
using MathServiceLibrary;

namespace MathWindowsServiceHost
{
   public partial class MathService : ServiceBase
   {
      private ServiceHost _serviceHost;   // Переменная-член типа ServiceHost

      public MathService()
      {
         InitializeComponent();
      }

      protected override void OnStart(string[] args)
      {
         // SetupEndPoint();
         if (_serviceHost != null)
         {
            _serviceHost.Close();
         }
         // Создать хост и указать URL для привязки HTTP
         _serviceHost = new ServiceHost(typeof(MathService), new Uri("http://localhost:8733/Design_Time_Addresses/MathServiceLibrary/BasicMath"));
         _serviceHost.AddDefaultEndpoints(); // Выбрать конечные точки по умолчанию
         _serviceHost.Open(); // Открыть хост
      }      

      protected override void OnStop()
      {
         if (_serviceHost != null)
         {
            _serviceHost.Close();
         }
      }

      private void SetupEndPoint()  // Мануальная установка конечной точки
      {
         if (_serviceHost != null) // Для подстраховки
         {
            _serviceHost.Close();
            _serviceHost = null;
         }
         _serviceHost = new ServiceHost(typeof(MathService)); // Создать хост и указать ABC в коде
         Uri address = new Uri("http://localhost:8733/Design_Time_Addresses/MathServiceLibrary/BasicMath");
         WSHttpBinding binding = new WSHttpBinding();
         Type contract = typeof(IBasicMath);
         _serviceHost.AddServiceEndpoint(contract, binding, address); // Добавить эту конечную точку
         _serviceHost.Open(); // Открыть хост
      }
   }
}
