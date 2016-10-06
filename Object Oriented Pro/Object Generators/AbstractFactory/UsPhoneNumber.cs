using System;

namespace AbstractFactory
{
   internal sealed class UsPhoneNumber : PhoneNumber
   {
      private const string CountryCodeLabel = "01";
      private const int NumberLength = 10;

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