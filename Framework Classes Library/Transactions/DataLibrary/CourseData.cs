using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataLibrary
{
   /// <summary>
   /// Интерфейс к таблице курсов
   /// </summary>
   public class CourseData
   {
      /// <summary>
      /// Добавление курса в контексте традиционной транзакции ADO.NET
      /// </summary>
      /// <param name="course">Курс</param>
      /// <returns>Задача продолжения</returns>
      public async Task AddCourceAsync(Course course)
      {
         var cmConn = new SqlConnection(Properties.Settings.Default.CourseManagementConnectionString);
         SqlCommand courceCommand = cmConn.CreateCommand();
         courceCommand.CommandText = "INSERT INTO Courses (Number, Title) VALUES (@Number, @Title)";
         await cmConn.OpenAsync();

         SqlTransaction transaction = cmConn.BeginTransaction();
         try
         {
            courceCommand.Transaction = transaction;
            courceCommand.Parameters.AddWithValue("@Number", course.Number);
            courceCommand.Parameters.AddWithValue("@Title", course.Title);
            await courceCommand.ExecuteNonQueryAsync();

            transaction.Commit();
         }
         catch (Exception ex)
         {
            Trace.WriteLine(string.Format("Error: {0}", ex.Message));
            transaction.Rollback();
            throw;
         }
         finally
         {
            cmConn.Close();
         }
      }
   }
}