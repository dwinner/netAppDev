using System.Data;
using System.Data.SqlClient;
using StoreDatabase.Properties;

namespace StoreDatabase
{
   public class StoreDbDataSet
   {
      private readonly string _connectionString = Settings.Default.Store;

      public DataTable GetProducts()
      {
         using (var connection = new SqlConnection(_connectionString))
         {
            SqlDataAdapter adapter;
            using (
               var getProductsCmd = new SqlCommand("GetProducts", connection)
               {
                  CommandType = CommandType.StoredProcedure
               })
            {
               adapter = new SqlDataAdapter(getProductsCmd);
            }

            var productsDataSet = new DataSet();
            adapter.Fill(productsDataSet, "Products");

            return productsDataSet.Tables[0];
         }
      }

      public DataSet GetCategoriesAndProducts()
      {
         using (var connection = new SqlConnection(_connectionString))
         {
            SqlDataAdapter adapter;
            DataSet products;
            using (var getProductsCmd = new SqlCommand("GetProducts", connection)
            {
               CommandType = CommandType.StoredProcedure
            })
            {
               adapter = new SqlDataAdapter(getProductsCmd);
               products = new DataSet();
               adapter.Fill(products, "Products");
               getProductsCmd.CommandText = "GetCategories";
            }
            adapter.Fill(products, "Categories");

            // Set up a relation between these tables (optional).
            var relCategoryProduct = new DataRelation("CategoryProduct",
               products.Tables["Categories"].Columns["CategoryID"],
               products.Tables["Products"].Columns["CategoryID"]);
            products.Relations.Add(relCategoryProduct);

            return products;
         }
      }
   }
}