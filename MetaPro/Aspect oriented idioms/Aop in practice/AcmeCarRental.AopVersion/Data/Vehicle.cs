using System;

namespace AcmeCarRental.AopVersion.Data
{
   public class Vehicle
   {
      public Guid Id { get; set; }
      public string Make { get; set; }
      public string Model { get; set; }
      public Size Size { get; set; }
      public string Vin { get; set; }
   }
}