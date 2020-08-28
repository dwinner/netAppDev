using System.ServiceProcess;

namespace ServiceCtrlFrontEnd
{
   public static class ServiceControllerStatusExtensions
   {
      public static string GetServiceControllerStatusName(this ServiceControllerStatus serviceControllerStatus)
      {
         switch (serviceControllerStatus)
         {
            case ServiceControllerStatus.ContinuePending:
               return "Continue Pending";
            case ServiceControllerStatus.Paused:
               return "Paused";
            case ServiceControllerStatus.StartPending:
               return "Start Pending";
            case ServiceControllerStatus.Running:
               return "Running";
            case ServiceControllerStatus.Stopped:
               return "Stopped";
            case ServiceControllerStatus.StopPending:
               return "Stop Pending";
            default:
               return "Unknown Status";
         }
      }
   }
}