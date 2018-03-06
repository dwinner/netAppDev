using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;

namespace SilverlightDemos.Web
{
   public class EventRegistrationDataService : DataService<EventRegistrationEntities>
   {
      // This method is called only once to initialize service-wide policies.
      public static void InitializeService(DataServiceConfiguration config)
      {
         config.SetEntitySetAccessRule("Events", EntitySetRights.AllRead);
         config.SetEntitySetAccessRule("Attendees", EntitySetRights.All);
         config.SetServiceOperationAccessRule("AddAttendee", ServiceOperationRights.All);
         config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
      }

      [WebGet]
      public bool AddAttendee(string name, string email, string company, string registrationCode, int eventId)
      {
         if (!(from rc in this.CurrentDataSource.RegistrationCodes
               where rc.RegistrationCode1 == registrationCode
               select rc).Any())
         {
            return false;
         }

         var attendee = new Attendee
         {
            Name = name,
            Email = email,
            Company = company,
            RegistrationCode = registrationCode,
            EventId = eventId
         };

         CurrentDataSource.Attendees.AddObject(attendee);
         return !(CurrentDataSource.SaveChanges() < 1);
      }

      [WebGet(UriTemplate = "foo")]
      public IEnumerable<string> Test()
      {
         return new List<string> { "one", "two" };
      }
   }
}

