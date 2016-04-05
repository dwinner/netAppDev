/**
 * Служба Windows для сервера цитат
 */

using System.ServiceProcess;

namespace QuoteService
{
   static class Program
   {      
      static void Main()
      {
         var servicesToRun = new ServiceBase[] 
         { 
            new QuoteService() 
         };
         ServiceBase.Run(servicesToRun);
      }
   }
}
