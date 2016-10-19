namespace AbstractFactory
{
   internal sealed class UsAddress : Address
   {
      private const string CountryLabel = "UNITED STATES";
      private const string Comma = ",";

      public override string GetCountry() => CountryLabel;

      public override string GetFullAddress()
         =>
            string.Format("{0}{1}{2}{3}{4}{5}{4}{6}{1}{7}{1}",
               Street,
               EolString,
               City,
               Comma,
               Space,
               Region,
               PostalCode,
               CountryLabel);
   }
}