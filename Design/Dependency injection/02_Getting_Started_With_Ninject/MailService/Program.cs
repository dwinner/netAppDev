using Ninject;

namespace Samples.MailService
{
   internal static class Program
   {
      private static void Main()
      {
         using (var kernel = new StandardKernel())
         {
            kernel.Bind<ILogger>().To<ConsoleLogger>();
            var mailService = kernel.Get<MailService>();
            mailService.SendMail("someone@domain.com", "Hi", null);
         }
      }
   }
}