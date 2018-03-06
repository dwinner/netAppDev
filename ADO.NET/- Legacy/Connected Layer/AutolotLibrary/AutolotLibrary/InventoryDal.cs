using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AutolotLibrary
{
   public class InventoryDal
   {
      private SqlConnection _sqlConnection;

      public void OpenConnection(string connectionString)
      {
         _sqlConnection = new SqlConnection(connectionString);
         _sqlConnection.Open();
      }

      public void CloseConnection()
      {
         _sqlConnection.Close();
      }
      
      public void InsertAuto(int id, string make, string color, string petName)
      {
         // Формирование и выполнение оператора SQL.
         string sql = string.Format("Insert into Inventory " +
                                    "(CarId, Make, Color, PetName) Values " +
                                    "(@CarId, @Make, @Color, @PetName)");
         using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
         {
            // Заполнение коллекции параметров.            
            command.Parameters.AddRange(new[]
               {
                  new SqlParameter()
                     {
                        ParameterName = "@CarId",
                        Value = id,
                        SqlDbType = SqlDbType.Int
                     },
                  new SqlParameter()
                     {
                        ParameterName = "@Make",
                        Value = make,
                        SqlDbType = SqlDbType.Char,
                        Size = 10
                     },
                  new SqlParameter()
                     {
                        ParameterName = "@Color",
                        Value = color,
                        SqlDbType = SqlDbType.Char,
                        Size = 10
                     },
                  new SqlParameter()
                     {
                        ParameterName = "@PetName",
                        Value = petName,
                        SqlDbType = SqlDbType.Char,
                        Size = 10
                     }
               });
            command.ExecuteNonQuery();
         }         
      }

      public void InsertAuto(NewCar car)
      {
         InsertAuto(car.CarId, car.Make, car.Color, car.PetName);
      }

      public void DeleteCar(int carId)
      {
         string sql = string.Format("Delete from Inventory where CarId = @carId");
         using (SqlCommand sqlCommand = new SqlCommand(sql, _sqlConnection))
         {
            try
            {
               sqlCommand.Parameters.Add(new SqlParameter()
                  {
                     ParameterName = "@carId",
                     Value = carId,
                     SqlDbType = SqlDbType.Int
                  });
               sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException sqlException)
            {
               Exception error = new Exception("The car is ordered", sqlException);
               throw error;
            }            
         }
      }

      public void UpdateCarPetName(int carId, string newPetName)
      {
         string sql = string.Format("update Inventory set PetName = '{0}' where carId = '{1}'",
                                    newPetName,
                                    carId);
         using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
         {
            command.ExecuteNonQuery();
         }
      }

      public List<NewCar> GetAllInventoryAsList()
      {
         // Здесь будут находиться записи.
         List<NewCar> inv = new List<NewCar>();

         // Подготовка объекта команды.
         string sql = "select * from Inventory";
         using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
         {
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
               int? carId = dataReader["CarId"] as int?;
               string color = dataReader["Color"] as string;
               string make = dataReader["Make"] as string;
               string petName = dataReader["PetName"] as string;
               NewCar aNewCar = new NewCar
                  {
                     CarId = carId.HasValue ? carId.Value : 0,
                     Color = color ?? "None",
                     Make = make ?? "None",
                     PetName = petName ?? "None"
                  };
               inv.Add(aNewCar);
            }
            dataReader.Close();
         }

         return inv;
      }

      public DataTable GetAllInventoryAsDataTable()
      {
         DataTable inv = new DataTable();
         string sql = "select * from Inventory";
         using (SqlCommand sqlCommand = new SqlCommand(sql, _sqlConnection))
         {
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            inv.Load(dataReader);
            dataReader.Close();
         }

         return inv;
      }

      public string LookUpPetName(int carId)
      {
         string carPetName;
         // Задание имени хранимой процедуры.
         using (SqlCommand sqlCommand = new SqlCommand("GetPetName", _sqlConnection))
         {
            sqlCommand.CommandType = CommandType.StoredProcedure;
            
            // Входной параметр
            SqlParameter parameter = new SqlParameter()
               {
                  ParameterName = "@carId",
                  SqlDbType = SqlDbType.Int,
                  Value = carId,
                  Direction = ParameterDirection.Input
               };
            sqlCommand.Parameters.Add(parameter);

            // Выходной параметр.
            parameter = new SqlParameter()
               {
                  ParameterName = "@petName",
                  SqlDbType = SqlDbType.Char,
                  Size = 10,
                  Direction = ParameterDirection.Output
               };
            sqlCommand.Parameters.Add(parameter);

            // Выполнение хранимой процедуры.
            sqlCommand.ExecuteNonQuery();

            // Возврат выходного параметра.
            carPetName = ((string) sqlCommand.Parameters["@petName"].Value).Trim();
         }

         return carPetName;
      }

      public void ProcessCreditRisk(int custId, bool throwEx)  // Транзакции для некредитоспособных клиентов
      {
         // Вначале выборка имени по идентификатору клиента.
         string firstName = string.Empty;
         string lastName = string.Empty;
         SqlCommand selectCommand = new SqlCommand(
            string.Format("select * from Customers where custId = {0}", custId),
            _sqlConnection);
         using (SqlDataReader dataReader = selectCommand.ExecuteReader())
         {
            if (dataReader.HasRows)
            {
               dataReader.Read();
               firstName = (string) dataReader["FirstName"];
               lastName = (string) dataReader["LastName"];
            }
            else
            {
               return;
            }
         }

         // Создание объектов команд для каждого шага операции.
         SqlCommand insertCommand = new SqlCommand(
            string.Format("insert into CreditRisks (custId, FirstName, LastName) values ({0}, '{1}', '{2}')",
               custId, firstName, lastName), _sqlConnection);
         SqlCommand removeCommand = new SqlCommand(
            string.Format("delete from Customers where custId = {0}", custId),
            _sqlConnection);         

         // Получаем из объекта подключения.
         SqlTransaction transaction = null;
         try
         {
            transaction = _sqlConnection.BeginTransaction();

            // Включение команд в транзакцию.
            insertCommand.Transaction = transaction;
            removeCommand.Transaction = transaction;            

            // Выполнение команд.
            insertCommand.ExecuteNonQuery();
            removeCommand.ExecuteNonQuery();

            // Имитация ошибки.
            if (throwEx)
            {
               throw new ApplicationException("Database error. Transaction finished failed");
            }

            transaction.Commit(); // Фиксируем.
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            if (transaction != null) transaction.Rollback(); // откат при ошибках
         }
      }
   }
}
