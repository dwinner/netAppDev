using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Todo: Мы используем SQLServer-провайдер, однако более гибкое решение использовать factory-методы

namespace AutoLotDAL
{
   public class NewCar
   {
      public int CarId { get; set; }
      public string Color { get; set; }
      public string Make { get; set; }
      public string PetName { get; set; }
   }

   public class InventoryDal
   {
      private SqlConnection _sqlCn; // Этот член используется всеми методами

      #region Методы открытия/закрытия
      public void OpenConnection(string connectionString)
      {
         _sqlCn = new SqlConnection { ConnectionString = connectionString };
         _sqlCn.Open();
      }

      public void CloseConnection()
      {
         _sqlCn.Close();
      }
      #endregion

      #region Методы вставки
      public void InsertAuto(NewCar car)
      {
         string sql = string.Format("Insert Into Inventory (CarId, Make, Color, PetName) Values('{0}', '{1}', '{2}', '{3}')",
            car.CarId,
            car.Make,
            car.Color,
            car.PetName);

         using (var cmd = new SqlCommand(sql, _sqlCn))
         {
            cmd.ExecuteNonQuery();
         }
      }

      public void InsertAuto(int id, string color, string make, string petName)
      {
         string sql = string.Format("Insert Into Inventory" +
             "(CarId, Make, Color, PetName) Values" +
             "(@CarId, @Make, @Color, @PetName)"); // Заполнители

         using (var sqlCommand = new SqlCommand(sql, _sqlCn))   // Заполняем параметры значениями
         {
            sqlCommand.Parameters.AddRange(new[]
               {
                  new SqlParameter {ParameterName = "@CarId", Value = id, SqlDbType = SqlDbType.Int},
                  new SqlParameter {ParameterName = "@Make", Value = make, SqlDbType = SqlDbType.Char, Size = 10},
                  new SqlParameter {ParameterName = "@Color", Value = color, SqlDbType = SqlDbType.Char, Size = 10},
                  new SqlParameter {ParameterName = "@PetName", Value = petName, SqlDbType = SqlDbType.Char, Size = 10}
               });
            sqlCommand.ExecuteNonQuery();
         }
      }
      #endregion

      #region Метод удаление записи
      public void DeleteCar(int id)
      {         
         string sql = string.Format("Delete from Inventory where CarID = '{0}'", id);
         using (var cmd = new SqlCommand(sql, _sqlCn))
         {
            try
            {
               cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
               var error = new Exception("Sorry! That car is on order!", ex);
               throw error;
            }
         }
      }
      #endregion

      #region Метод обновления записи
      public void UpdateCarPetName(int id, string newPetName)
      {         
         string sql = string.Format("Update Inventory Set PetName = '{0}' Where CarID = '{1}'",
           newPetName,
           id);
         using (var cmd = new SqlCommand(sql, _sqlCn))
         {
            cmd.ExecuteNonQuery();
         }
      }
      #endregion

      #region Методы выборки данных
      
      public DataTable GetAllInventoryAsDataTable()
      {         
         DataTable inv = new DataTable();         
         const string sql = "Select * From Inventory";
         using (var cmd = new SqlCommand(sql, _sqlCn))
         {
            SqlDataReader dr = cmd.ExecuteReader();            
            inv.Load(dr);
            dr.Close();
         }
         return inv;
      }

      public List<NewCar> GetAllInventoryAsList()
      {         
         var inv = new List<NewCar>();         
         string sql = "Select * From Inventory";
         using (var cmd = new SqlCommand(sql, _sqlCn))
         {
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               inv.Add(new NewCar
               {
                  CarId = (int)dr["CarId"],
                  Color = (string)dr["Color"],
                  Make = (string)dr["Make"],
                  PetName = (string)dr["PetName"]
               });
            }
            dr.Close();
         }
         return inv;
      }

      #endregion

      #region Поиск определенных полей
      public string LookUpPetName(int carId)
      {
         string carPetName;
         
         using (var sqlCommand = new SqlCommand("GetPetName", _sqlCn))
         {
            sqlCommand.CommandType = CommandType.StoredProcedure;

            var param = new SqlParameter  // Входной параметр процедуры.
               {
                  ParameterName = "@carId",
                  SqlDbType = SqlDbType.Int,
                  Value = carId,
                  Direction = ParameterDirection.Input
               };
            sqlCommand.Parameters.Add(param);

            param = new SqlParameter   // Выходной параметр процедуры.
               {
                  ParameterName = "@petName",
                  SqlDbType = SqlDbType.Char,
                  Size = 10,
                  Direction = ParameterDirection.Output
               };
            sqlCommand.Parameters.Add(param);
            
            sqlCommand.ExecuteNonQuery();  // Выполняем хранимую процедуру
            
            carPetName = (string) sqlCommand.Parameters["@petName"].Value;   // Возвращаем выходной параметр
         }
         return carPetName;
      }

      #endregion

      #region Методы с транзакциями
      public void ProcessCreditRisk(bool throwEx, int custId)
      {        
         string firstName;
         string lastName;
         SqlCommand sqlCommand =
            new SqlCommand(string.Format("Select * from Customers where CustId = {0}", custId), _sqlCn);
         using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
         {
            if (sqlDataReader.HasRows)
            {
               sqlDataReader.Read();
               firstName = (string) sqlDataReader["FirstName"];
               lastName = (string) sqlDataReader["LastName"];
            }
            else
               return;
         }

         // Создадим объекты команд, представляющие каждый шаг операции
         SqlCommand removeCommand =
            new SqlCommand(string.Format("Delete from Customers where CustId = {0}", custId), _sqlCn);

         SqlCommand insertCommand = new SqlCommand(string.Format("Insert Into CreditRisks" +
                          "(CustId, FirstName, LastName) Values" +
                          "({0}, '{1}', '{2}')", custId, firstName, lastName), _sqlCn);
         
         SqlTransaction sqlTransaction = null;
         try   // Выполним транзакцию
         {
            sqlTransaction = _sqlCn.BeginTransaction();
            
            insertCommand.Transaction = sqlTransaction;
            removeCommand.Transaction = sqlTransaction;
            
            insertCommand.ExecuteNonQuery(); // Выполним все команды в транзакции
            removeCommand.ExecuteNonQuery();
            
            if (throwEx)   // Симуляция возникновения ошибки
            {
               throw new Exception("Sorry!  Database error! Tx failed...");
            }
            
            sqlTransaction.Commit();   // коммит
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);            
            if (sqlTransaction != null)   // Откат
               sqlTransaction.Rollback();
         }
      }
      #endregion
   }
}
