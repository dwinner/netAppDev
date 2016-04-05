using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace FillDataSetUsingSqlDataAdapter
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with Data Adapters *****\n");

         // жестко-закодированная строка подключения
         string cnStr =
            "Data Source=Hi-Tech-PC;Initial Catalog=AutoLot;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

         // Создание объекта DataSet вызывающим процессом.
         DataSet ds = new DataSet("AutoLot");

         // Передача адаптеру текста команды Select и подключения.
         SqlDataAdapter dAdapt = new SqlDataAdapter("Select * From Inventory", cnStr);

         // Заполнение DataSet новой таблицей с именем Inventory
         DataTableMapping custMap = dAdapt.TableMappings.Add("Inventory", "Current Inventory");
         custMap.ColumnMappings.Add("CarID", "Car ID");
         custMap.ColumnMappings.Add("PetName", "Name of Car");
         dAdapt.Fill(ds, "Inventory");

         // Вывод содержимого DataSet.
         PrintDataSet(ds);
         Console.ReadLine();
      }

      #region Print out data
      static void PrintDataSet(DataSet ds)
      {         
         Console.WriteLine("DataSet is named: {0}", ds.DataSetName);
         foreach (System.Collections.DictionaryEntry de in ds.ExtendedProperties)
         {
            Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
         }
         Console.WriteLine();

         foreach (DataTable dt in ds.Tables)
         {
            Console.WriteLine("=> {0} Table:", dt.TableName);
            
            for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
            {
               Console.Write(dt.Columns[curCol].ColumnName + "\t");
            }
            Console.WriteLine("\n----------------------------------");

            // Print the DataTable.
            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
               for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
               {
                  Console.Write(dt.Rows[curRow][curCol].ToString().Trim() + "\t");
               }
               Console.WriteLine();
            }
         }
      }
      #endregion
   }
}
