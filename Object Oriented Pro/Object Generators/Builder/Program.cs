using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder
{
   class Program
   {
      static void Main()
      {
         AppointmentBuilder appointmentBuilder = new AppointmentBuilder();
         try
         {
            Appointment appointment = Scheduler.CreateAppointment(appointmentBuilder,
               new DateTime(2010, 7, 7),
               new DateTime(2011, 7, 7),
               "Trek conversion",
               new LocationImpl("Fargo, ND"),
               new List<IContact>
               {
                  new ContactImpl("Denny", "Glover", "Gun", "Hollywood")
               });
         }
         catch (InfoRequiredException infoRequiredEx)
         {
            PrintExceptions(infoRequiredEx);
         }

         Console.ReadKey();
      }

      private static void PrintExceptions(InfoRequiredException infoRequired)
      {
         InfoRequiredException.InfoErrors errors = infoRequired.InfoRequired;
         StringBuilder errorStringBuilder = new StringBuilder();
         foreach (InfoRequiredException.InfoErrors infoError in
            Enum.GetValues(typeof(InfoRequiredException.InfoErrors))
               .Cast<InfoRequiredException.InfoErrors>()
               .Where(infoError => errors.HasFlag(infoError)))
         {
            errorStringBuilder.AppendLine("Error: " + infoError);
         }
         Console.WriteLine(errorStringBuilder.ToString());
      }
   }
}
