using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Builder
{
   internal static class Program
   {
      private static void Main()
      {
         var appointmentBuilder = new AppointmentBuilder();
         try
         {
            var appointment = Scheduler.CreateAppointment(appointmentBuilder,
               new DateTime(2010, 7, 7),
               new DateTime(2011, 7, 7),
               "Trek conversion",
               new LocationImpl("Fargo, ND"),
               new List<IContact>
               {
                  new ContactImpl("Denny", "Glover", "Gun", "Hollywood")
               });

            foreach (var contact in appointment.Attendees)
            {
               WriteLine(contact);
            }

            WriteLine(appointment.Description);
            WriteLine(appointment.StartDate);
            WriteLine(appointment.EndDate);
            WriteLine(appointment.Location);
         }
         catch (InfoRequiredException infoRequiredEx)
         {
            PrintExceptions(infoRequiredEx);
         }

         ReadKey();
      }

      private static void PrintExceptions(InfoRequiredException infoRequired)
      {
         var errors = infoRequired.InfoRequired;
         var errorStringBuilder = new StringBuilder();
         foreach (var infoError in
            Enum.GetValues(typeof (InfoRequiredException.InfoErrors))
               .Cast<InfoRequiredException.InfoErrors>()
               .Where(infoError => errors.HasFlag(infoError)))
         {
            errorStringBuilder.AppendLine("Error: " + infoError);
         }
         WriteLine(errorStringBuilder.ToString());
      }
   }
}