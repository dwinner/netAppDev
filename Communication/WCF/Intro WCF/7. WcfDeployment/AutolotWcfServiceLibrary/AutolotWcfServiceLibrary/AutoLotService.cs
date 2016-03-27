using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace AutolotWcfServiceLibrary
{   
   public class AutoLotService : IAutoLotService
   {
      private static readonly string ConnectionString;

      static AutoLotService()
      {
         ConnectionString = ConfigurationManager.ConnectionStrings["AutoLotConnect"].ConnectionString;
      }

      public void InsertCar(int carId, string make, string color, string petName)
      {
         using (InventoryDalAdapter adapter = new InventoryDalAdapter(ConnectionString))
         {
            adapter.InsertAuto(carId, make, color, petName);
         }
      }

      public void InsertCar(InventoryRecord aCar)
      {
         InsertCar(aCar.CarId, aCar.Make, aCar.Color, aCar.PetName);
      }

      public InventoryRecord[] GetInventory()
      {
         using (InventoryDalAdapter adapter = new InventoryDalAdapter(ConnectionString))
         {
            DataTable dataTable = adapter.GetAllInventoryAsDataTable();
            List<InventoryRecord> records = new List<InventoryRecord>();
            DataTableReader reader = dataTable.CreateDataReader();
            while (reader.Read())
            {
               InventoryRecord invRecord = new InventoryRecord();
               int? carId = reader["CarId"] as int?;
               string color = reader["Color"] as string;
               string make = reader["Make"] as string;
               string petName = reader["PetName"] as string;
               invRecord.CarId = carId.HasValue ? carId.Value : 0;
               invRecord.Color = color ?? "Black";
               invRecord.Make = make ?? "None";
               invRecord.PetName = petName ?? "None";
               records.Add(invRecord);               
            }
            return records.ToArray();
         }         
      }
   }
}
