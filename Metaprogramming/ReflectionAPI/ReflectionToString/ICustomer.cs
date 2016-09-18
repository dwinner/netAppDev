using System;

namespace ReflectionToString
{
   public interface ICustomer
   {
      Guid Id { get; set; }
      int Age { get; set; }
      string FirstName { get; set; }
      string LastName { get; set; }
   }
}