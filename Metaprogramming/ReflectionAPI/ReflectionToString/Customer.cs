using System;

namespace ReflectionToString
{
   public abstract class Customer : ICustomer
   {
      protected Customer()
      {
         Id = Guid.NewGuid();
      }

      public Guid Id { get; set; }
      public int Age { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
   }
}