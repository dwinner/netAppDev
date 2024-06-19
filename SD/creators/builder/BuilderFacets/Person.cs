namespace BuilderFacets;

public class Person
{
   public int AnnualIncome { get; set; }

   // employment info
   public string CompanyName { get; set; } = string.Empty;
   public string Position { get; set; } = string.Empty;

   // address
   public string StreetAddress { get; set; } = string.Empty;
   public string Postcode { get; set; } = string.Empty;
   public string City { get; set; } = string.Empty;

   public override string ToString() =>
      $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
}