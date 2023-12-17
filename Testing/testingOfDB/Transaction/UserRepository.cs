using System.Data.SqlClient;
using System.Dynamic;

namespace Transaction;

public class UserRepository(Transaction transaction)
{
   public object[] GetUserById(int userId)
   {
      using (var connection = new SqlConnection(transaction.ConnectionString))
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
      using (var connection = new SqlConnection(transaction.ConnectionString))
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
         var userId = 1;
         /*connection.Query<int>(query, new
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
}