using System;
using System.Data;
using AutoLotDAL;
using AutoLotDAL.AutoLotDataSetTableAdapters;

namespace LinqToDataSetApp
{
   class Program
   {
      static void Main()
      {         
         AutoLotDataSet dal = new AutoLotDataSet();
         InventoryTableAdapter da = new InventoryTableAdapter();
         AutoLotDataSet.InventoryDataTable data = da.GetData();
         
         PrintAllCarIds(data);
         Console.WriteLine();
         
         ShowRedCars(data);
         Console.WriteLine();

         BuildDataTableFromQuery(data);
         Console.WriteLine();

         Console.ReadLine();
      }

      private static void BuildDataTableFromQuery(AutoLotDataSet.InventoryDataTable data)
      {
         var cars = from car in data.AsEnumerable()
                    where car.Field<int>("CarId") > 5
                    select car;
         DataTable newTable = cars.CopyToDataTable(); // Использование этого результирующего набора для создания нового DataTable
         for (int currentRow = 0; currentRow < newTable.Rows.Count; currentRow++)
         {
            for (int currentColumn = 0; currentColumn < newTable.Columns.Count; currentColumn++)
            {
               Console.Write(newTable.Rows[currentRow][currentColumn].ToString().Trim() + "\t");
            }
         }
         Console.WriteLine();
      }

      private static void PrintAllCarIds(DataTable dataTable)
      {
         EnumerableRowCollection enumerableRow = dataTable.AsEnumerable(); // Получение перечисляемой версии DataTable
         foreach (DataRow dataRow in enumerableRow)
         {
            Console.WriteLine("Car Id = {0}", dataRow["CarId"]);
         }
      }

      private static void ShowRedCars(DataTable data)
      {
         if (data as AutoLotDataSet.InventoryDataTable == null)
         {
            return;
         }
         /*var cars = from car in data.AsEnumerable()
                    where (string) car["Color"] == "Red"
                    select new
                       {
                          Id = (int) car["CarId"],
                          Make = (string) car["Make"]
                       };*/
         var cars = from car in data.AsEnumerable()
                    where car.Field<string>("Color") == "Red"
                    select new
                       {
                          Id = car.Field<int>("CarId"),
                          Make = car.Field<string>("Make")
                       };
         foreach (var item in cars)
         {
            Console.WriteLine("CarId = {0} - {1}", item.Id, item.Make);
         }
      }
   }
}
