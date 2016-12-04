using System;

namespace AcmeCarRental.AopVersion.Data
{
   public class Customer
   {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public string DriversLicense { get; set; }
      public DateTime DateOfBirth { get; set; }
   }
}