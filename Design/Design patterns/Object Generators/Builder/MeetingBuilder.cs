using System;

namespace Builder
{
   /// <summary>
   ///    Генератор может быть расширен с целью инициализации дополнительных полей объекта
   ///    или других дополнительных проверок.
   /// </summary>
   public class MeetingBuilder : AppointmentBuilder
   {
      public override Appointment GetAppointment()
      {
         try
         {
            base.GetAppointment();
         }
         finally
         {
            if (Appointment.EndDate == DateTime.Today)
            {
               RequiredElements |= InfoRequiredException.InfoErrors.EndDateRequired;
            }

            if (!HasErrors && RequiredElements != InfoRequiredException.InfoErrors.None)
            {
               throw new InfoRequiredException("Some field is not set", RequiredElements);
            }
         }

         return Appointment;
      }
   }
}