using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLibrary
{
   /// <summary>
   /// Интерфейс к таблице студентов
   /// </summary>
   public class StudentData
   {
      /// <summary>
      /// Добавляет студента в таблицу
      /// </summary>
      /// <param name="student">Студент</param>
      /// <returns>Задача продолжения</returns>
      public async Task AddStudentAsync(Student student)
      {
         var connection = new SqlConnection(Properties.Settings.Default.CourseManagementConnectionString);
         await connection.OpenAsync();
         try
         {
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
               "INSERT INTO Students (FirstName, LastName, Company) VALUES (@FirstName, @LastName, @Company)";
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Company", student.Company);

            await command.ExecuteNonQueryAsync();
         }
         finally
         {
            connection.Close();
         }
      }

      /// <summary>
      /// Добавляет студента в таблицу
      /// </summary>
      /// <param name="aStudent">Студент</param>
      /// <returns>Кол-во добавленных записей</returns>
      public int AddStudent(Student aStudent)
      {
         var connection = new SqlConnection(Properties.Settings.Default.CourseManagementConnectionString);
         connection.Open();
         try
         {
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
               "INSERT INTO Students (FirstName, LastName, Company) VALUES (@FirstName, @LastName, @Company)";
            command.Parameters.AddWithValue("@FirstName", aStudent.FirstName);
            command.Parameters.AddWithValue("@LastName", aStudent.LastName);
            command.Parameters.AddWithValue("@Company", aStudent.Company);

            return command.ExecuteNonQuery();
         }
         finally
         {
            connection.Close();
         }
      }

      /// <summary>
      /// Добавляет студента в таблицу
      /// </summary>
      /// <param name="aStudent">Студент</param>
      /// <param name="transaction">Транзакция</param>
      /// <returns>Задача продолжения</returns>
      public async Task AddStudentAsync(Student aStudent, Transaction transaction)
      {
         //Contract.Requires<ArgumentNullException>(aStudent != null);

         var connection = new SqlConnection(Properties.Settings.Default.CourseManagementConnectionString);
         await connection.OpenAsync();
         try
         {
            if (transaction != null)   // NOTE: Присоединение транзакции к соединению ADO.NET
            {
               connection.EnlistTransaction(transaction);
            }

            SqlCommand command = connection.CreateCommand();
            command.CommandText =
               "INSERT INTO Students (FirstName, LastName, Company) VALUES (@FirstName, @LastName, @Company)";
            command.Parameters.AddWithValue("@FirstName", aStudent.FirstName);
            command.Parameters.AddWithValue("@LastName", aStudent.LastName);
            command.Parameters.AddWithValue("@Company", aStudent.Company);

            await command.ExecuteNonQueryAsync();
         }
         finally
         {
            connection.Close();
         }
      }
   }
}