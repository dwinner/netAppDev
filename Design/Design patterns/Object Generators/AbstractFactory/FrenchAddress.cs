namespace AbstractFactory
{
   internal sealed class FrenchAddress : Address
   {
      private const string CountryLabel = "FRANCE";

      public override string GetCountry() => CountryLabel;

      public override string GetFullAddress()
         => string.Format("{0}{1}{2}{3}{4}{1}{5}{1}",
               Street,
               EolString,
               PostalCode,
               Space,
               City,
               CountryLabel);
   }
}