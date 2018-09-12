using System;

namespace AbstractFactory
{
   internal sealed class FrenchPhoneNumber : PhoneNumber
   {
      private const string CountryCodeLabel = "33";
      private const int NumberLength = 9;

      public override string TelephoneNumber
      {
         get { return base.TelephoneNumber; }
         set
         {
            if (value.Length == NumberLength)
            {
               base.TelephoneNumber = value;
            }
            else
            {
               throw new ArgumentException($"Length must be {NumberLength}");
            }
         }
      }

      public override string GetCountryCode() => CountryCodeLabel;
   }
}