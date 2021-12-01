// Chain of responsibility via enum: post office modelling

using System;
using System.Linq;

namespace ChainViaEnums
{
   internal static class PostOffice
   {
      private static void Main()
      {
         foreach (var mail in Mail.Generate(10))
         {
            Console.WriteLine(mail.GetDetails());
            Hanlde(mail);
            Console.WriteLine(Enumerable.Repeat('*', 20)
               .Aggregate(string.Empty, (current, @char) => $"{current}{@char}"));
         }
      }

      private static void Hanlde(Mail mail)
      {
         var handled = Enum.GetValues<MailHandler>().Any(handler => handler.HandleMail(mail));
         if (!handled)
         {
            Console.WriteLine($"{mail} is a dead letter");
         }
      }
   }
}