namespace EF;

public class CompanyRepository
{
   private readonly CrmContext _context;

   public CompanyRepository(CrmContext context) => _context = context;

   public Company GetCompany() =>
      _context.Companies
         .SingleOrDefault();

   public void SaveCompany(Company company)
   {
      _context.Companies.Update(company);
   }

   public void AddCompany(Company company)
   {
      _context.Companies.Add(company);
   }
}