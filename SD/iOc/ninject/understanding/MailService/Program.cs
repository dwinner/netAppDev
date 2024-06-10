namespace Samples.MailService
{
   internal static class Program
   {
      private static void Main()
      {
         var mailService = new MailService(new EventLogger());
         mailService.SendMail("someone@somewhere.com", "My first DI App", "Hello World!");
      }
   }
}