using System;
using System.Collections.Generic;

namespace Builder
{
   /// <summary>
   /// Пошаговый генератор сложного объекта.
   /// </summary>
   public class AppointmentBuilder
   {
      protected Appointment Appointment;
      protected InfoRequiredException.InfoErrors RequiredElements = InfoRequiredException.InfoErrors.None;
      protected bool HasErrors = false;

      /// <summary>
      /// Инициализация сложного объекта
      /// </summary>
      public void BuildAppointment()
      {
         Appointment = new Appointment();
      }

      /// <summary>
      /// Инициализация дат для сложного объекта
      /// </summary>
      /// <param name="startDate">Начальная дата</param>
      /// <param name="endDate">Конечная дата</param>
      public void BuildDates(DateTime startDate, DateTime endDate)
      {         
         DateTime currentDateTime = DateTime.Now;
         Appointment.StartDate = startDate > currentDateTime ? startDate : currentDateTime;
         Appointment.EndDate = endDate >= startDate ? endDate : startDate;         
      }

      /// <summary>
      /// Инициализация описания для сложного объекта
      /// </summary>
      /// <param name="newDescription">Описание</param>
      public void BuildDescription(string newDescription)
      {
         Appointment.Description = newDescription;
      }

      /// <summary>
      /// Инициализация списка контактов сложного объекта
      /// </summary>
      /// <param name="attendees">Список контактов</param>
      public void BuildAttendees(ICollection<IContact> attendees)
      {
         if (attendees != null && !attendees.Empty())
         {
            Appointment.Attendees = attendees;
         }
      }

      /// <summary>
      /// Инициализация нового расположения для сложного объекта
      /// </summary>
      /// <param name="newLocation">Новое расположение</param>
      public void BuildLocation(ILocation newLocation)
      {
         if (newLocation != null)
         {
            Appointment.Location = newLocation;
         }
      }

      /// <summary>
      /// Получает готовый объект Appointment, построенный по шагам
      /// </summary>      
      /// <returns>объект Appointment</returns>
      ///<exception cref="InfoRequiredException">Если не все обязательные поля были проинициализированны</exception>
      public virtual Appointment GetAppointment()
      {
         if (Appointment.Location == null)
         {
            RequiredElements |= InfoRequiredException.InfoErrors.LocationRequired;
         }
         if (Appointment.Attendees == null || Appointment.Attendees.Empty())
         {
            RequiredElements |= InfoRequiredException.InfoErrors.AttendeeRequired;
         }
         if (RequiredElements != InfoRequiredException.InfoErrors.None)
         {
            HasErrors = true;
            throw new InfoRequiredException("Some field is not set", RequiredElements);            
         }
         return Appointment;
      }
   }

   /// <summary>
   /// Генератор может быть расширен с целью инициализации дополнительных полей объекта
   /// или других дополнительных проверок.
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
