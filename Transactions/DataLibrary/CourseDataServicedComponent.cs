/**
 * Транзакции в стиле EnterpriseServices
 */

using System.Data.SqlClient;
using System.EnterpriseServices;

namespace DataLibrary
{
   [Transaction(TransactionOption.Required)]
   public class CourseDataServicedComponent : ServicedComponent
   {
      [AutoComplete]
      public void AddCourse(Course course)
      {
         var connection =
            new SqlConnection(Properties.Settings.Default.CourseManagementConnectionString);
         SqlCommand courseCommand = connection.CreateCommand();
         courseCommand.CommandText = "INSERT INTO Courses (Number, Title) VALUES (@Number, @Title)";
         connection.Open();
         try
         {
            courseCommand.Parameters.AddWithValue("@Number", course.Number);
            courseCommand.Parameters.AddWithValue("@Title", course.Title);
            courseCommand.ExecuteNonQuery();
         }
         finally
         {
            connection.Close();
         }
      }
   }
}