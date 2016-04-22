using System.Data;
using AutolotLibrary;
using System.Collections.Generic;

namespace AutolotWcfService
{   
   public class AutoLotService : IAutoLotService
   {
      private const string ConnectionString = "Data Source=Hi-Tech-PC;Initial Catalog=AutoLot;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

      public void InsertCar(int carId, string make, string color, string petName)
      {
         InventoryDal dal = null;
         try
         {
            dal = new InventoryDal();
            dal.OpenConnection(ConnectionString);
            dal.InsertAuto(carId, make, color, petName);
         }
         finally
         {
            if (dal != null)
            {
               dal.CloseConnection();
            }
         }         
      }

      public void InsertCar(InventoryRecord aCar)
      {
         InsertCar(aCar.CarId, aCar.Make, aCar.Color, aCar.PetName);         
      }

      public InventoryRecord[] GetInventory()
      {
         InventoryDal dal = null;
         try
         {
            // Сначала получить DataTable из базы данных.
            dal = new InventoryDal();
            dal.OpenConnection(ConnectionString);
            DataTable dataTable = dal.GetAllInventoryAsDataTable();

            List<InventoryRecord> records = new List<InventoryRecord>();   // Создать List<T> для хранения полученных данных
            DataTableReader reader = dataTable.CreateDataReader();   // Скопировать таблицу данных
            while (reader.Read())
            {
               InventoryRecord invRecord = new InventoryRecord
                  {
                     CarId = (int) reader["CarId"],
                     Color = (string) reader["Color"],
                     Make = (string) reader["Make"],
                     PetName = (string) reader["PetName"]
                  };
               records.Add(invRecord);
            }
            return records.ToArray();
         }
         finally
         {
            if (dal != null)
            {
               dal.CloseConnection();
            }
         }
      }
   }
}
