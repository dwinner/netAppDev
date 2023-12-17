using System.Data.SqlClient;
using System.Dynamic;

namespace V1;

public class Database
{
   private readonly string _connectionString;

   public Database(string connectionString) => _connectionString = connectionString;

   public object[] GetUserById(int userId)
   {
      using (var connection = new SqlConnection(_connectionString))
      {
         var query = "SELECT * FROM [dbo].[User] WHERE UserID = @UserID";
         dynamic data = new ExpandoObject(); //connection.QuerySingle(query, new { UserID = userId });

         return new object[]
         {
            data.UserID,
            data.Email,
            data.Type,
            data.IsEmailConfirmed
         };
      }
   }

   public void SaveUser(User user)
   {
      using (var connection = new SqlConnection(_connectionString))
      {
         var updateQuery = @"
                    UPDATE [dbo].[User]
                    SET Email = @Email, Type = @Type,
                        IsEmailConfirmed = @IsEmailConfirmed
                    WHERE UserID = @UserID
                    SELECT @UserID";

         var insertQuery = @"
                    INSERT [dbo].[User] (Email, Type, IsEmailConfirmed)
                    VALUES (@Email, @Type, @IsEmailConfirmed)
                    SELECT CAST(SCOPE_IDENTITY() as int)";

         var query = user.UserId == 0
            ? insertQuery
            : updateQuery;
         var userId = 1; /*connection.Query<int>(query, new
            {
               user.Email,
               user.UserId,
               user.IsEmailConfirmed,
               Type = (int)user.Type
            })
            .Single();*/

         user.UserId = userId;
      }
   }

   public object[] GetCompany()
   {
      using (var connection = new SqlConnection(_connectionString))
      {
         var query = "SELECT * FROM dbo.Company";
         dynamic data = new ExpandoObject(); //connection.QuerySingle(query);

         return new object[]
         {
            data.DomainName,
            data.NumberOfEmployees
         };
      }
   }

   public void SaveCompany(Company company)
   {
      using (var connection = new SqlConnection(_connectionString))
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