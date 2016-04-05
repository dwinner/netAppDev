using System.ServiceProcess;

namespace AutolotWindowsServiceHost
{
   public static class Program
   {
      /// <summary>
      /// Главная точка входа для приложения.
      /// </summary>
      static void Main()
      {
         ServiceBase[] servicesToRun = new ServiceBase[] 
            { 
               new AutolotWinService() 
            };
         ServiceBase.Run(servicesToRun);
      }
   }
}
