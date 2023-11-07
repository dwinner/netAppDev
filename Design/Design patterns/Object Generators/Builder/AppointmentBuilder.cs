using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Builder
{
   /// <summary>
   ///    Пошаговый генератор сложного объекта.
   /// </summary>
   public class AppointmentBuilder
   {
      [NotNull] protected Appointment Appointment = new Appointment();
      protected bool HasErrors;

      protected InfoRequiredException.InfoErrors RequiredElements
         = InfoRequiredException.InfoErrors.None;

      /// <summary>
      ///    Инициализация сложного объекта
      /// </summary>
      public void BuildAppointment() => Appointment = new Appointment();

      /// <summary>
      ///    Инициализация дат для сложного объекта
      /// </summary>
      /// <param name="startDate">Начальная дата</param>
      /// <param name="endDate">Конечная дата</param>
      public void BuildDates(DateTime startDate, DateTime endDate)
      {
         var currentDateTime = DateTime.Now;
         Appointment.StartDate = startDate > currentDateTime
            ? startDate
            : currentDateTime;
         Appointment.EndDate = endDate >= startDate ? endDate : startDate;
      }

      /// <summary>
      ///    Инициализация описания для сложного объекта
      /// </summary>
      /// <param name="newDescription">Описание</param>
      public void BuildDescription([CanBeNull] string newDescription)
         => Appointment.Description = newDescription;

      /// <summary>
      ///    Инициализация списка контактов сложного объекта
      /// </summary>
      /// <param name="attendees">Список контактов</param>
      public void BuildAttendees([CanBeNull] ICollection<IContact> attendees)
      {
         if (attendees == null || attendees.Empty())
         {
            return;
         }

         foreach (var attendee in attendees)
         {
            Appointment.Attendees.Add(attendee);
         }
      }

      /// <summary>
      ///    Инициализация нового расположения для сложного объекта
      /// </summary>
      /// <param name="newLocation">Новое расположение</param>
      public void BuildLocation([NotNull] ILocation newLocation)
         => Appointment.Location = newLocation;

      /// <summary>
      ///    Получает готовый объект Appointment, построенный по шагам
      /// </summary>
      /// <returns>объект Appointment</returns>
      /// <exception cref="InfoRequiredException">Если не все обязательные поля были проинициализированны</exception>
      [NotNull]
      public virtual Appointment GetAppointment()
      {
         if (Appointment.Location == null)
         {
            RequiredElements
               |= InfoRequiredException.InfoErrors.LocationRequired;
         }

         if (Appointment.Attendees.Empty())
         {
            RequiredElements
               |= InfoRequiredException.InfoErrors.AttendeeRequired;
         }

         if (RequiredElements != InfoRequiredException.InfoErrors.None)
         {
            HasErrors = true;
            throw new InfoRequiredException(
               "Some field is not set", RequiredElements);
         }

         return Appointment;
      }
   }
}