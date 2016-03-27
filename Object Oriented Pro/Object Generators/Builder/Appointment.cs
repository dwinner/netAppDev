using System;
using System.Collections.Generic;

namespace Builder
{
   /// <summary>
   /// Класс для композиции других объектов.
   /// </summary>
   public class Appointment
   {
      private ICollection<IContact> _attendees = new LinkedList<IContact>();

      public ICollection<IContact> Attendees
      {
         get { return _attendees; }
         set
         {
            if (value != null)
            {
               _attendees = value;
            }            
         }
      }

      private ILocation _location;

      public ILocation Location
      {
         get { return _location; }
         set
         {
            if (value != null)
            {
               _location = value;
            }
         }
      }

      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public string Description { get; set; }
      
      public void AddAttendee(IContact aContact)
      {
         if (aContact != null)
         {
            _attendees.Add(aContact);
         }
      }

      public void RemoveAttendee(IContact aContact)
      {
         if (aContact != null)
         {
            _attendees.Remove(aContact);
         }
      }

      public override string ToString()
      {
         return string.Format("[Appointment Attendees={0}, Location={1}, StartDate={2}, EndDate={3}, Description={4}]",
            _attendees,
            _location,
            StartDate,
            EndDate,
            Description);
      }

   }
}
