#nullable disable
using System.Collections.ObjectModel;

namespace VisitorDispatch;

internal class Person
{
   // The Friends collection may contain Customers & Employees:
   public readonly IList<Person> Friends = new Collection<Person>();

   public string FirstName { get; set; }

   public string LastName { get; set; }
}