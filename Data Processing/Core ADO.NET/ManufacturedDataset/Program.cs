/**
 * Настраиваемый набор данных
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ManufacturedDataset
{
   internal class Program
   {
      private const string ConnectionStringName = "Northwind";

      private static readonly string ConnectionString =
         ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

      private static void Main()
      {
         const string productSelect = "SELECT * FROM Products";
         const string categoriesSelect = "SELECT * FROM Categories";

         using (var connection = new SqlConnection(ConnectionString))
         {
            var productDataAdapter = new SqlDataAdapter(productSelect, connection);
            var northwindDataSet = new DataSet();
            ManufactureProductDataTable(northwindDataSet); // Конфигурируем таблицу продуктов
            productDataAdapter.Fill(northwindDataSet, "Products");

            foreach (DataRow row in northwindDataSet.Tables["Products"].Rows)
            {
               Console.WriteLine("'{0}' from {1}", row[0], row[1]);
            }

            var catDataAdapter = new SqlDataAdapter(categoriesSelect, connection);
            ManufactureCategoryTable(northwindDataSet); // Конфигурируем таблицу категорий
            catDataAdapter.Fill(northwindDataSet, "Categories");

            AddForeignKeyConstraint(northwindDataSet); // Добавляем ограничение внешнего ключа
         }
      }

      public static void ManufactureProductDataTable(DataSet aDataSet) // Настройка таблицы Products
      {
         var productsTable = new DataTable("Products");

         productsTable.Columns.Add(new DataColumn("ProductId", typeof (int)));
         productsTable.Columns.Add(new DataColumn("ProductName", typeof (string)));
         productsTable.Columns.Add(new DataColumn("SupplierId", typeof (int)));
         productsTable.Columns.Add(new DataColumn("CategoryId", typeof (int)));
         productsTable.Columns.Add(new DataColumn("QuantityPerUnit", typeof (string)));
         productsTable.Columns.Add(new DataColumn("UnitPrice", typeof (decimal)));
         productsTable.Columns.Add(new DataColumn("UnitsInStock", typeof (short)));
         productsTable.Columns.Add(new DataColumn("UnitsOnOrder", typeof (short)));
         productsTable.Columns.Add(new DataColumn("ReorderLevel", typeof (short)));
         productsTable.Columns.Add(new DataColumn("Discontinued", typeof (bool)));

         ManufacturePrimaryKey(productsTable);
         aDataSet.Tables.Add(productsTable);
      }

      public static void ManufacturePrimaryKey(DataTable aProductsTable) // Настройка первичного ключа таблицы Products
      {
         var primaryKeys = new DataColumn[1];
         primaryKeys[0] = aProductsTable.Columns["ProductId"];
         aProductsTable.Constraints.Add(new UniqueConstraint("PK_Products", primaryKeys[0]));
         aProductsTable.PrimaryKey = primaryKeys;
      }

      public static void ManufactureCategoryTable(DataSet aDataSet) // Настройка таблицы категорий
      {
         var categoriesDataTable = new DataTable("Categories");

         categoriesDataTable.Columns.Add(new DataColumn("CategoryId", typeof (int)));
         categoriesDataTable.Columns.Add(new DataColumn("CategoryName", typeof (string)));
         categoriesDataTable.Columns.Add(new DataColumn("Description", typeof (string)));

         categoriesDataTable.Constraints.Add(
            new UniqueConstraint("PK_Categories", categoriesDataTable.Columns["CategoryId"]));
         categoriesDataTable.PrimaryKey = new[] {categoriesDataTable.Columns["CategoryId"]};
         aDataSet.Tables.Add(categoriesDataTable);
      }

      public static void AddForeignKeyConstraint(DataSet aDataSet) // Добавление внешнего ключа к таблице категорий
      {
         DataColumn parent = aDataSet.Tables["Categories"].Columns["CategoryId"];
         DataColumn child = aDataSet.Tables["Products"].Columns["CategoryId"];

         var foreignKey = new ForeignKeyConstraint("FK_Product_CategoryId", parent, child)
         {
            UpdateRule = Rule.Cascade,
            DeleteRule = Rule.SetNull
         };

         // "Пытаемся" создать ограничение. Если этого не удалось, то в таблице продуктов у нас могут
         // быть записи, не относящиеся к какой-либо категории
         aDataSet.Tables["Products"].Constraints.Add(foreignKey);
      }
   }
}