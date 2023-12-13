using System.Data.SqlClient;
using System.Dynamic;

namespace Transaction;

public class CompanyRepository(Transaction transaction)
{
   public object[] GetCompany()
   {
      using (SqlConnection connection = new SqlConnection(transaction.ConnectionString))
      {
         var query = "SELECT * FROM dbo.Company";
         dynamic data = new ExpandoObject();//connection.QuerySingle(query);

         return new object[]
         {
            data.DomainName,
            data.NumberOfEmployees
         };
      }
   }

   public void SaveCompany(Company company)
   {
      using (var connection = new SqlConnection(transaction.ConnectionString))
      {
         var query = @"
                    UPDATE dbo.Company
                    SET DomainName = @DomainName,
                        NumberOfEmployees = @NumberOfEmployees";
         /*
         connection.Execute(query, new
         {
            company.DomainName,
            company.NumberOfEmployees
         });
         */
      }
   }
}