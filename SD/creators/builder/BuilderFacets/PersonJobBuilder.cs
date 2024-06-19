namespace BuilderFacets;

public class PersonJobBuilder(Person person) : PersonBuilder(person)
{
   public PersonJobBuilder At(string companyName)
   {
      Person.CompanyName = companyName;
      return this;
   }

   public PersonJobBuilder As(string position)
   {
      Person.Position = position;
      return this;
   }

   public PersonJobBuilder Earning(int annualIncome)
   {
      Person.AnnualIncome = annualIncome;
      return this;
   }
}