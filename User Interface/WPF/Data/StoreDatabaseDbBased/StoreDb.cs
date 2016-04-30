using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using StoreDatabase.Properties;

namespace StoreDatabase
{
   public class StoreDb
   {
      private readonly string _connectionString = Settings.Default.Store;

      public Product GetProduct(int productId)
      {
         using (var connection = new SqlConnection(_connectionString))
         using (
            var getProductsByIdCmd = new SqlCommand("GetProductByID", connection)
            {
               CommandType = CommandType.StoredProcedure
            })
         {
            getProductsByIdCmd.Parameters.AddWithValue("@ProductID", productId);
            connection.Open();
            var reader = getProductsByIdCmd.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.HasRows)
            {
               // Create a Product object that wraps the 
               // current record.
               var product = new Product((string)reader["ModelNumber"],
                  (string)reader["ModelName"], (decimal)reader["UnitCost"],
                  (string)reader["Description"], (int)reader["CategoryID"],
                  (string)reader["CategoryName"], (string)reader["ProductImage"]);

               return product;
            }

            return null;
         }
      }

      public ICollection<Product> GetProducts()
      {
         ObservableCollection<Product> products;
         using (var connection = new SqlConnection(_connectionString))
         using (
            var getProductsCmd = new SqlCommand("GetProducts", connection) { CommandType = CommandType.StoredProcedure })
         {
            products = new ObservableCollection<Product>();
            connection.Open();
            var reader = getProductsCmd.ExecuteReader();
            while (reader.Read())
            {
               // Create a Product object that wraps the 
               // current record.
               var product = new Product((string)reader["ModelNumber"],
                  (string)reader["ModelName"], (decimal)reader["UnitCost"],
                  (string)reader["Description"], (int)reader["CategoryID"],
                  (string)reader["CategoryName"], (string)reader["ProductImage"]);
               // Add to collection
               products.Add(product);
            }
         }

         return products;
      }

      public ICollection<Product> GetProductsSlow()
      {
         Thread.Sleep(TimeSpan.FromSeconds(5));
         return GetProducts();
      }

      public ICollection<Product> GetProductsFilteredWithLinq(decimal minimumCost)
      {
         // Get the full list of products.
         var products = GetProducts();

         // Create a second collection with matching products.
         var matches = from product in products
                       where product.UnitCost >= minimumCost
                       select product;

         return new ObservableCollection<Product>(matches.ToList());
      }

      public ICollection<Category> GetCategoriesAndProducts()
      {
         using (var connection = new SqlConnection(_connectionString))
         using (var command = new SqlCommand("GetProducts", connection) { CommandType = CommandType.StoredProcedure })
         {
            var adapter = new SqlDataAdapter(command);

            var dataSet = new DataSet();
            adapter.Fill(dataSet, "Products");
            command.CommandText = "GetCategories";

            adapter.Fill(dataSet, "Categories");

            // Set up a relation between these tables (optional).
            var relCategoryProduct = new DataRelation("CategoryProduct",
               dataSet.Tables["Categories"].Columns["CategoryID"],
               dataSet.Tables["Products"].Columns["CategoryID"]);
            dataSet.Relations.Add(relCategoryProduct);

            var categories = new ObservableCollection<Category>();
            foreach (var categoryRow in dataSet.Tables["Categories"].Rows.Cast<DataRow>())
            {
               var products = new ObservableCollection<Product>();
               foreach (var productRow in categoryRow.GetChildRows(relCategoryProduct))
               {
                  products.Add(new Product(productRow["ModelNumber"].ToString(),
                     productRow["ModelName"].ToString(), (decimal)productRow["UnitCost"],
                     productRow["Description"].ToString()));
               }

               categories.Add(new Category(categoryRow["CategoryName"].ToString(), products));
            }

            return categories;
         }
      }

      public ICollection<Category> GetCategories()
      {
         ObservableCollection<Category> categories;
         using (var connection = new SqlConnection(_connectionString))
         using (var command = new SqlCommand("GetCategories", connection) { CommandType = CommandType.StoredProcedure })
         {
            categories = new ObservableCollection<Category>();
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
               // Create a Category object that wraps the 
               // current record.
               var category = new Category((string)reader["CategoryName"], (int)reader["CategoryID"]);

               // Add to collection
               categories.Add(category);
            }
         }

         return categories;
      }
   }
}