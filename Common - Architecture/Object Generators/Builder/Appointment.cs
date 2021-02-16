using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Builder
{
   /// <summary>
   ///    Класс для композиции других объектов.
   /// </summary>
   public class Appointment
   {
      [NotNull]
      public ICollection<IContact> Attendees { get; }
         = new LinkedList<IContact>();

      [CanBeNull]
      public ILocation Location { get; set; }

      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }

      [CanBeNull]
      public string Description { get; set; }

      public void AddAttendee([NotNull] IContact aContact) => Attendees.Add(aContact);

      public void RemoveAttendee([NotNull] IContact aContact) => Attendees.Remove(aContact);

      public override string ToString()
         =>
            $"[Location={Location}, StartDate={StartDate}, EndDate={EndDate}, Description={Description}]";
   }
}