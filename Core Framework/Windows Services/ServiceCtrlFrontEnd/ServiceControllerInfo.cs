using System.ServiceProcess;

namespace ServiceCtrlFrontEnd
{
   /// <summary>
   /// Тип, предоставляющий информацию о службе Windows
   /// </summary>
   public class ServiceControllerInfo
   {
      private readonly ServiceController _controller;

      /// <summary>
      /// Объект службы Windows
      /// </summary>
      public ServiceController Controller
      {
         get { return _controller; }
      }

      /// <summary>
      /// Конструктор типа информации о службе Windows
      /// </summary>
      /// <param name="controller">Объект службы Windows</param>
      public ServiceControllerInfo(ServiceController controller)
      {
         _controller = controller;
      }

      /// <summary>
      /// Тип службы
      /// </summary>
      public string ServiceTypeName
      {
         get
         {
            ServiceType type = _controller.ServiceType;
            string serviceTypeName = string.Empty;
            if ((type & ServiceType.InteractiveProcess) != 0)
            {
               serviceTypeName = "Interactive";
               type -= ServiceType.InteractiveProcess;
            }

            switch (type)
            {
               case ServiceType.Adapter:
                  serviceTypeName += "Adapter";
                  break;
               case ServiceType.FileSystemDriver:
               case ServiceType.KernelDriver:
               case ServiceType.RecognizerDriver:
                  serviceTypeName += "Driver";
                  break;
               case ServiceType.Win32OwnProcess:
                  serviceTypeName += "Win32 Service Process";
                  break;
               case ServiceType.Win32ShareProcess:
                  serviceTypeName += "Win32 Shared Process";
                  break;
               default:
                  serviceTypeName += string.Format("Unknown type {0}", type);
                  break;
            }

            return serviceTypeName;
         }
      }

      /// <summary>
      /// Статус службы
      /// </summary>
      public string ServiceStatusName
      {
         get { return _controller.Status.GetServiceControllerStatusName(); }
      }

      /// <summary>
      /// Отображаемое имя службы
      /// </summary>
      public string DisplayName
      {
         get { return _controller.DisplayName; }
      }

      /// <summary>
      /// Имя сервиса
      /// </summary>
      public string ServiceName
      {
         get { return _controller.ServiceName; }
      }

      /// <summary>
      /// Возможность запуска
      /// </summary>
      public bool EnableStart
      {
         get { return _controller.Status == ServiceControllerStatus.Stopped; }
      }

      /// <summary>
      /// Возможность останова
      /// </summary>
      public bool EnableStop
      {
         get { return _controller.Status == ServiceControllerStatus.Running; }
      }

      /// <summary>
      /// Возможность приостановки
      /// </summary>
      public bool EnablePause
      {
         get { return _controller.Status == ServiceControllerStatus.Running && _controller.CanPauseAndContinue; }
      }

      /// <summary>
      /// Возможность возобновления
      /// </summary>
      public bool EnableContinue
      {
         get { return _controller.Status == ServiceControllerStatus.Paused; }
      }
   }
}