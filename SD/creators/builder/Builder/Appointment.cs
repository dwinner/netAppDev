using System;
using System.Collections.Generic;

namespace Builder
{
   /// <summary>
   ///    Класс для композиции других объектов.
   /// </summary>
   public class Appointment
   {
      public ICollection<IContact> Attendees { get; }
         = new LinkedList<IContact>();

      public ILocation Location { get; set; }

      public DateTime StartDate { get; set; }

      public DateTime EndDate { get; set; }

      public string Description { get; set; }

      public void AddAttendee(IContact aContact) => Attendees.Add(aContact);

      public void RemoveAttendee(IContact aContact) => Attendees.Remove(aContact);

      public override string ToString()
         =>
            $"[Location={Location}, StartDate={StartDate}, EndDate={EndDate}, Description={Description}]";
   }
}