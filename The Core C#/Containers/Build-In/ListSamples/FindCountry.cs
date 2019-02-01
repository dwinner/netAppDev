using System;

namespace ListSamples
{
   public class FindCountry
   {
      private readonly string _country;

      public FindCountry(string country)
      {
         _country = country;
      }

      public bool FindCountryPredicate(Racer racer)
      {
         //Contract.Requires<ArgumentNullException>(racer != null);
         if (racer == null)
            throw new ArgumentNullException("racer");
         return racer.Country == _country;
      }
   }
}