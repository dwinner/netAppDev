using System;

namespace Builder
{
   /// <summary>
   ///    Ãåíåðàòîð ìîæåò áûòü ðàñøèðåí ñ öåëüþ èíèöèàëèçàöèè äîïîëíèòåëüíûõ ïîëåé îáúåêòà
   ///    èëè äðóãèõ äîïîëíèòåëüíûõ ïðîâåðîê.
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