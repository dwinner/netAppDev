/**
 * CRUD-операции на адаптере данных
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDviaDataAdapter
{
   internal class Program
   {
      private const string ConnectionStringName = "Northwind";

      private static readonly string ConnectionString =
         ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

      private static void Main()
      {
         using (var sqlConnection = new SqlConnection(ConnectionString))   // Создание и открытие подключения
         {
            sqlConnection.Open();
            var dataSet = new DataSet();
            CreateTable(dataSet); // Создание таблицы Region
            var sqlDataAdapter = new SqlDataAdapter // Создание настроенного адаптера данных для заполнения набора
            {
               SelectCommand = GenerateSelectCommand(sqlConnection),
               InsertCommand = GenerateInsertCommand(sqlConnection),
               UpdateCommand = GenerateUpdateCommand(sqlConnection),
               DeleteCommand = GenerateDeleteCommand(sqlConnection)
            };

            sqlDataAdapter.Fill(dataSet, "Region"); // Выполнение команды выборки для заполнения набора данных
            DumpDataSet(dataSet, "Initial data selected from database");

            DataRow newRegionRow = dataSet.Tables["Region"].NewRow(); // Добавление новой строки к набору данных
            newRegionRow["RegionID"] = 999;
            newRegionRow["RegionDescription"] = "North West";
            dataSet.Tables["Region"].Rows.Add(newRegionRow); // Добавление созданной строки к таблице
            DumpDataSet(dataSet, "New row pending inserting into database");

            sqlDataAdapter.Update(dataSet, "Region"); // Обновим записи физически через адаптер
            DumpDataSet(dataSet, "New row updated and new RegionID assigned by database");

            string regionId = newRegionRow[0].ToString(); // Сохраним regionId для наших экспериментов
            newRegionRow["RegionDescription"] = "North West England"; // Обновим что-нибудь
            DumpDataSet(dataSet, string.Format("Changed RegionID {0} description", regionId));

            sqlDataAdapter.Update(dataSet, "Region"); // Обновим записи физически
            newRegionRow.Delete(); // Удалим ранее созданную строку

            DumpDataSet(dataSet, string.Format("Deleted RegionID {0}", regionId));
            sqlDataAdapter.Update(dataSet, "Region");

            // Сгенерируем XML файлы со схемой данных в заголовке и без схемы
            dataSet.WriteXml(".\\WithoutSchema.xml");
            dataSet.WriteXml(".\\WithSchema.xml", XmlWriteMode.WriteSchema);
         }
      }

      /// <summary>
      ///    Вывод состояние набора данных на консоль
      /// </summary>
      /// <param name="aDataSet">Набор данных</param>
      /// <param name="aMessage">Сообщение</param>
      private static void DumpDataSet(DataSet aDataSet, string aMessage)
      {
         Console.WriteLine(aMessage);

         foreach (string output in from DataRow dataRow
            in aDataSet.Tables["Region"].Rows
            select dataRow.RowState == DataRowState.Deleted
               ? "Row Deleted"
               : string.Format("  {0,-3} {1,-50} {2}", dataRow[0], dataRow[1], dataRow.RowState))
         {
            Console.WriteLine(output);
         }
      }

      /// <summary>
      ///    Создание команды выборки записей из таблицы Region
      /// </summary>
      /// <param name="aSqlConnection">Объект подключения</param>
      /// <returns>Команда выборки записей из таблицы Region</returns>
      private static SqlCommand GenerateSelectCommand(SqlConnection aSqlConnection)
      {
         return new SqlCommand("RegionSelect", aSqlConnection)
         {
            CommandType = CommandType.StoredProcedure,
            UpdatedRowSource = UpdateRowSource.None
         };
      }

      /// <summary>
      ///    Создание команды обновления записи в таблице Region
      /// </summary>
      /// <param name="aSqlConnection">Объект подключения</param>
      /// <returns>Команда обновления записи в таблице Region</returns>
      private static SqlCommand GenerateUpdateCommand(SqlConnection aSqlConnection)
      {
         var regionUpdateCmd = new SqlCommand("RegionUpdate", aSqlConnection)
         {
            CommandType = CommandType.StoredProcedure
         };

         regionUpdateCmd.Parameters.Add(new SqlParameter("@RegionId", SqlDbType.Int, 0, "RegionID"));
         regionUpdateCmd.Parameters.Add(new SqlParameter("@RegionDescription", SqlDbType.NChar, 50, "RegionDescription"));
         regionUpdateCmd.UpdatedRowSource = UpdateRowSource.None;

         return regionUpdateCmd;
      }

      /// <summary>
      ///    Создание команды вставки записи в таблицу Region
      /// </summary>
      /// <param name="aSqlConnection">Объект подключения</param>
      /// <returns>Команда вставки записи в таблицу Region</returns>
      private static SqlCommand GenerateInsertCommand(SqlConnection aSqlConnection)
      {
         var regionInsertCmd = new SqlCommand("RegionInsert", aSqlConnection)
         {
            CommandType = CommandType.StoredProcedure
         };

         regionInsertCmd.Parameters.Add(new SqlParameter("@RegionDescription", SqlDbType.NChar, 50, "RegionDescription"));
         regionInsertCmd.Parameters.Add(new SqlParameter("@RegionId", SqlDbType.Int, 0, ParameterDirection.Output, false,
            0, 0, "RegionID", DataRowVersion.Default, null));
         regionInsertCmd.UpdatedRowSource = UpdateRowSource.OutputParameters;

         return regionInsertCmd;
      }

      /// <summary>
      ///    Создание команды удаления записи из таблицы Region
      /// </summary>
      /// <param name="aSqlConnection">Объект подключения к SQL Server'у</param>
      /// <returns>Команда удаления записи из таблицы Region</returns>
      private static SqlCommand GenerateDeleteCommand(SqlConnection aSqlConnection)
      {
         var regionDeleteCommand = new SqlCommand("RegionDelete", aSqlConnection)
         {
            CommandType = CommandType.StoredProcedure
         };

         regionDeleteCommand.Parameters.Add(new SqlParameter("@RegionId", SqlDbType.Int, 0, "RegionID"));
         regionDeleteCommand.UpdatedRowSource = UpdateRowSource.None;

         return regionDeleteCommand;
      }

      /// <summary>
      ///    Создание таблицы Region
      /// </summary>
      /// <param name="aDataSet">Набор данных</param>
      private static void CreateTable(DataSet aDataSet)
      {
         var regionDataTble = new DataTable("Region");

         var regionId = new DataColumn("RegionID", typeof (int))
         {
            AllowDBNull = false,
            AutoIncrement = true,
            AutoIncrementSeed = 1
         };

         var regionDescription = new DataColumn("RegionDescription", typeof (string)) {AllowDBNull = false};

         regionDataTble.Columns.Add(regionId);
         regionDataTble.Columns.Add(regionDescription);

         aDataSet.Tables.Add(regionDataTble);
      }
   }
}