using System;
using System.Data;
using AutoLotDAL;
using AutoLotDAL.AutoLotDataSetTableAdapters;

namespace StronglyTypedDataSetConsoleClient
{
   public static class Program
   {
      static void Main(string[] args)
      {
         AutoLotDataSet.InventoryDataTable table = new AutoLotDataSet.InventoryDataTable();         
         InventoryTableAdapter adapter = new InventoryTableAdapter();         
         AddRecords(table, adapter);
         table.Clear();
         adapter.Fill(table);
         PrintInventory(table);
         Console.ReadLine();
      }

      private static void PrintInventory(DataTable table)
      {
         for (int currentColumn = 0; currentColumn < table.Columns.Count; currentColumn++)   // Вывод имен столбцов
         {
            Console.Write(table.Columns[currentColumn].ColumnName + "\t");
         }
         for (int currentRow = 0; currentRow < table.Rows.Count; currentRow++)
         {
            for (int currentColumn = 0; currentColumn < table.Columns.Count; currentColumn++)
            {
               Console.Write(table.Rows[currentRow][currentColumn] + "\t");
            }
            Console.WriteLine();
         }
      }

      private static void AddRecords(AutoLotDataSet.InventoryDataTable table, InventoryTableAdapter adapter)   // Добавление записей
      {
         AutoLotDataSet.InventoryRow newRow = table.NewInventoryRow();  // Получение из таблицы новой строго типизированной строки
         newRow.CarID = 999;  // Заполнение строки данными
         newRow.Color = "Purple";
         newRow.Make = "BMW";
         newRow.PetName = "Saku";
         table.AddInventoryRow(newRow);   // Вставка новой строки
         table.AddInventoryRow(888, "Yugo", "Green", "Zippy");
         adapter.Update(table);  // Обновление базы данных
      }

      private static void RemoveRecords(AutoLotDataSet.InventoryDataTable table, InventoryTableAdapter adapter)   // Удаление записей
      {
         AutoLotDataSet.InventoryRow toDeleteRow = table.FindByCarID(999);
         adapter.Delete(toDeleteRow.CarID, toDeleteRow.Make, toDeleteRow.Color, toDeleteRow.PetName);
         toDeleteRow = table.FindByCarID(888);
         adapter.Delete(toDeleteRow.CarID, toDeleteRow.Make, toDeleteRow.Color, toDeleteRow.PetName);
      }

      private static void CallStoredProcedure() // Вызов хранимой процедуры
      {
         QueriesTableAdapter queriesTableAdapter = new QueriesTableAdapter();
         Console.WriteLine("Enter car id:");
         string carId = Console.ReadLine();
         string carName = string.Empty;
         if (carId != null)
         {
            queriesTableAdapter.GetPetName(int.Parse(carId), ref carName);
            Console.WriteLine("Name: {0}; Id: {1}", carName, carId);
         }
      }
   }
}
