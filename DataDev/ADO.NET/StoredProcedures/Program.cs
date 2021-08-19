/**
 * Выполнение хранимых процедур 
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace StoredProcedures
{
   internal class Program
   {
      private const string ConnectionStringName = "Northwind";

      private static readonly string ConnectionString =
         ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

      private static void Main()
      {
         using (var connection = new SqlConnection(ConnectionString))
         using (var scope = new TransactionScope())
         {
            connection.Open();

            SqlCommand updateCommand = GenerateUpdateCommand(connection); // Сгенерируем команду обновления
            SqlCommand deleteCommand = GenerateDeleteCommand(connection); // Добавим команду на удаление записи
            SqlCommand insertCommand = GenerateInsertCommand(connection); // Добавим команду на вставку записи

            DumpRegions(connection, "Regions prior to any stored procedure calls");

            insertCommand.Parameters["@RegionDescription"].Value = "South West"; // Установим новое значение для параметра команды @RegionDescription
            insertCommand.ExecuteNonQuery(); // Выполним команду
            var newRegionId = (int)insertCommand.Parameters["@RegionId"].Value; // Извлекаем значение выходного параметра

            DumpRegions(connection, "Regions after inserting 'South West'");

            updateCommand.Parameters[0].Value = newRegionId;   // Выполним команду обновления, задав необходимые значения параметрам
            updateCommand.Parameters[1].Value = "South Western England";
            int affected = updateCommand.ExecuteNonQuery();

            DumpRegions(connection,
               string.Format(
                  "Regions after updating 'South West' to 'South Western England'.{0}Affected: {1} record(s)",
                  Environment.NewLine, affected));

            deleteCommand.Parameters["@RegionId"].Value = newRegionId;  // Удалим ранее созданную запись
            deleteCommand.ExecuteNonQuery();

            DumpRegions(connection, "Regions after deleting 'South Western England'");

            scope.Complete();
            connection.Close();
         }
      }

      /// <summary>
      ///    Создание команды, которая вставляет запись с регионом
      /// </summary>
      /// <param name="sqlConnection">Объект подключения</param>
      /// <returns>Команда вставки записи</returns>
      private static SqlCommand GenerateInsertCommand(SqlConnection sqlConnection)
      {
         var command = new SqlCommand("RegionInsert", sqlConnection) { CommandType = CommandType.StoredProcedure };
         command.Parameters.AddRange(new[]
         {
            new SqlParameter("@RegionDescription", SqlDbType.NChar, 50, "RegionDescription"),
            new SqlParameter("@RegionId", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "RegionID",
               DataRowVersion.Default, null)
         });
         command.UpdatedRowSource = UpdateRowSource.OutputParameters;

         return command;
      }

      /// <summary>
      ///    Создает команду, которая удаляет запись с регионом
      /// </summary>
      /// <param name="sqlConnection">Объект подключения</param>
      /// <returns>Команда удаления записи</returns>
      private static SqlCommand GenerateDeleteCommand(SqlConnection sqlConnection)
      {
         var command = new SqlCommand("RegionDelete", sqlConnection) { CommandType = CommandType.StoredProcedure };
         command.Parameters.Add("@RegionId", SqlDbType.Int, 0, "RegionID");
         command.UpdatedRowSource = UpdateRowSource.None;

         return command;
      }

      /// <summary>
      ///    Создание команды, которая обновляет запись с регионом
      /// </summary>
      /// <param name="sqlConnection">Объект подключения</param>
      /// <returns>SQL-команда</returns>
      private static SqlCommand GenerateUpdateCommand(SqlConnection sqlConnection)
      {
         var command = new SqlCommand("RegionUpdate", sqlConnection) { CommandType = CommandType.StoredProcedure };
         command.Parameters.Add(new SqlParameter("@RegionId", SqlDbType.Int, 0, "RegionID"));
         command.Parameters.Add(new SqlParameter("@RegionDescription", SqlDbType.NChar, 50, "RegionDescription"));
         command.UpdatedRowSource = UpdateRowSource.None;

         return command;
      }

      private static void DumpRegions(SqlConnection sqlConnection, string message)
      {
         var aCommand = new SqlCommand("SELECT RegionID, RegionDescription FROM Region", sqlConnection);
         using (SqlDataReader sqlDataReader = aCommand.ExecuteReader(CommandBehavior.KeyInfo))
         {
            Console.WriteLine(message);
            while (sqlDataReader.Read())
            {
               Console.WriteLine("  {0,-20} {1,-40}", sqlDataReader[0], sqlDataReader[1]);
            }
            sqlDataReader.Close();
         }
      }
   }
}