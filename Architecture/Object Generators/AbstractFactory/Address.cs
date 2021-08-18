using System;

namespace AbstractFactory
{
   public abstract class Address
   {
      protected const string Space = " ";
      protected static readonly string EolString = Environment.NewLine;

      public string Street { protected get; set; }

      public string City { protected get; set; }

      public string Region { protected get; set; }

      public string PostalCode { protected get; set; }

      public abstract string GetCountry();

      public abstract string GetFullAddress();
   }
}