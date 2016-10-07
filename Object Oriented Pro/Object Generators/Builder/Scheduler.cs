using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Builder
{
   /// <summary>
   ///    Класс-распорядитель для создания объекта через пошаговый генератор.
   /// </summary>
   public static class Scheduler
   {
      /// <summary>
      ///    Пошаговое создание объекта через его строитель
      /// </summary>
      /// <param name="builder">Построитель объекта Appointment</param>
      /// <param name="startDateTime">Начальная дата</param>
      /// <param name="endDateTime">Конечная дата</param>
      /// <param name="description">Описание</param>
      /// <param name="location">Положение</param>
      /// <param name="attendees">Список контактов</param>
      /// <returns>Готовый объект Appointment</returns>
      /// <exception cref="InfoRequiredException">Если не все поля были проинициализированны</exception>
      [NotNull]
      public static Appointment CreateAppointment(
         [NotNull] AppointmentBuilder builder,
         DateTime startDateTime,
         DateTime endDateTime,
         [NotNull] string description,
         [NotNull] ILocation location,
         [NotNull] ICollection<IContact> attendees)
      {
         builder.BuildAppointment();
         builder.BuildDates(startDateTime, endDateTime);
         builder.BuildDescription(description);
         builder.BuildAttendees(attendees);
         builder.BuildLocation(location);

         return builder.GetAppointment();
      }
   }
}