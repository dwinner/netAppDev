using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VisitorViaDLR
{
   public class Person
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public IList<Person> Friends { get; } = new Collection<Person>();
   }
}