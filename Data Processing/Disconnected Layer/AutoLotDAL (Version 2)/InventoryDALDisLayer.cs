using System.Data;
using System.Data.SqlClient;

namespace AutoLotDAL
{
   public class InventoryDalDisLayer
   {      
      private readonly string _connectionString = string.Empty;
      private readonly SqlDataAdapter _sqlDataAdapter;

      public InventoryDalDisLayer(string connectionString)
      {
         _connectionString = connectionString;         
         ConfigureAdapter(out _sqlDataAdapter);
      }

      private void ConfigureAdapter(out SqlDataAdapter sqlDataAdapter)
      {         
         sqlDataAdapter = new SqlDataAdapter("Select * From Inventory", _connectionString);         
         // SqlCommandBuilder builder = new SqlCommandBuilder(sqlDataAdapter);
      }

      public DataTable GetAllInventory()
      {
         DataTable inv = new DataTable("Inventory");
         _sqlDataAdapter.Fill(inv);
         return inv;
      }

      public void UpdateInventory(DataTable modifiedTable)
      {
         _sqlDataAdapter.Update(modifiedTable);
      }
   }
}
