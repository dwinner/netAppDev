using System.Collections;
using System.Data;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpleDataSet
{
   class Program
   {
      static void Main()
      {
         // Создание объекта DataSet и добавление нескольких свойств.
         DataSet carsInventoryDataSet = new DataSet("Car Inventory");
         carsInventoryDataSet.ExtendedProperties["TimeStamp"] = DateTime.Now;
         carsInventoryDataSet.ExtendedProperties["DataSetId"] = Guid.NewGuid();
         carsInventoryDataSet.ExtendedProperties["Company"] = "Mikko market";

         // ManipulateDataRowState();

         FillDataSet(carsInventoryDataSet);
         PrintDataSet(carsInventoryDataSet);
         SaveAndLoadAsXml(carsInventoryDataSet);
         SaveAndLoadAsBinary(carsInventoryDataSet);

         Console.ReadLine();
      }

      static void FillDataSet(DataSet aDataSet)
      {
         // Создание столбцов данных, соответствующих "реальным"
         // столбцам таблицы Inventory из базы данных AutoLot.
         
         DataColumn carIdColumn = new DataColumn("CarId", typeof(int))
            {
               Caption = "Car Id",
               ReadOnly = true,
               AllowDBNull = false,
               Unique = true,
               AutoIncrement = true,
               AutoIncrementSeed = 0,
               AutoIncrementStep = 1
            };

         DataColumn carMakeColumn = new DataColumn("Make", typeof(string));
         DataColumn carColorColumn = new DataColumn("Color", typeof(string));
         DataColumn carPetNameColumn = new DataColumn("PetName", typeof(string)) {Caption = "Friendly name"};

         // Добавление объектов DataColumn в DataTable
         DataTable inventoryTable = new DataTable("Inventory");
         inventoryTable.Columns.AddRange(new[]
            {
               carIdColumn,
               carMakeColumn,
               carColorColumn,
               carPetNameColumn
            });
         inventoryTable.PrimaryKey = new[]   // Добавим первичный ключ к таблице
            {
               inventoryTable.Columns[0]
            };

         // Добавление нескольких строк в таблицу Inventory
         DataRow carRow = inventoryTable.NewRow();
         carRow["Make"] = "BMW";
         carRow["Color"] = "Black";
         carRow["PetName"] = "Hamlet";
         inventoryTable.Rows.Add(carRow);

         carRow = inventoryTable.NewRow();
         // Столбец 0 содержит автоинкрементное поле, поэтому начинаем с первого.
         carRow[1] = "Saab";
         carRow[2] = "Red";
         carRow[3] = "Sea Breeze";
         inventoryTable.Rows.Add(carRow);

         // Добавим таблицу в набор данных
         aDataSet.Tables.Add(inventoryTable);         
      }

      private static void ManipulateDataRowState() // Состояние строки и RowState
      {
         // Создание нового DataTable для демонстрационных целей.
         DataTable temp = new DataTable("Temp");
         temp.Columns.Add(new DataColumn("TempColumn", typeof (int)));

         // RowState = Detached (т.е. еще не принадлежит никакому DataTable).
         DataRow row = temp.NewRow();
         Console.WriteLine("After call NewRow(): {0}", row.RowState);

         // RowState = Added.
         temp.Rows.Add(row);
         Console.WriteLine("After call Rows.Add(): {0}", row.RowState);

         // RowState = Added.
         row["TempColumn"] = 10;
         Console.WriteLine("After the first assign: {0}", row.RowState);

         // RowState = Unchanged.
         temp.AcceptChanges();
         Console.WriteLine("After call AcceptChanges(): {0}", row.RowState);

         // RowState = Modified.
         row["TempColumn"] = 11;
         Console.WriteLine("After the second assign: {0}", row.RowState);

         // RowState = Deleted.
         temp.Rows[0].Delete();
         Console.WriteLine("After call Delete(): {0}", row.RowState);
      }

      #region Способы получения данных из объекта DataSet
      static void PrintDataSet(DataSet aDataSet)
      {
         // Вывод имени и расширенных свойств.
         Console.WriteLine("DataSet Name: {0}", aDataSet.DataSetName);
         foreach (DictionaryEntry dictionaryEntry in aDataSet.ExtendedProperties)
         {
            Console.WriteLine("Key = {0}, Value = {1}", dictionaryEntry.Key, dictionaryEntry.Value);
         }
         Console.WriteLine();

         /* Вывод каждой таблицы.
         foreach (DataTable dataTable in aDataSet.Tables)
         {
            Console.WriteLine("=> Table {0}:", dataTable.TableName);

            // Вывод имен столбцов.
            for (int currentColumn = 0; currentColumn < dataTable.Columns.Count; currentColumn++)
            {
               Console.WriteLine(dataTable.Columns[currentColumn].ColumnName + "\t");
            }
            Console.WriteLine("\n-------------------------------------------");

            // Вывод содержимого DataTable.
            for (int currentRow = 0; currentRow < dataTable.Rows.Count; currentRow++)
            {
               for (int currentColumn = 0; currentColumn < dataTable.Columns.Count; currentColumn++)
               {
                  Console.Write(dataTable.Rows[currentRow][currentColumn] + "\t");
               }
               Console.WriteLine();
            }
         }*/

         foreach (DataTable dataTable in aDataSet.Tables)
         {
            Console.WriteLine("=> Table {0}:", dataTable.TableName);

            // Вывод имен столбцов.
            for (int currentColumn = 0; currentColumn < dataTable.Columns.Count; currentColumn++)
            {
               Console.Write(dataTable.Columns[currentColumn].ColumnName.Trim() + "\t");
            }
            Console.WriteLine("\n---------------------------------------");
            PrintTable(dataTable);
         }
      }

      static void PrintTable(DataTable aDataTable) // Проход по таблице DataTable через объект чтения
      {
         // Создание объекта DataTableReader.
         DataTableReader dataTableReader = aDataTable.CreateDataReader();

         // dataTableReader работает так же, как и DataReader.
         while (dataTableReader.Read())
         {
            for (int i = 0; i < dataTableReader.FieldCount; i++)
            {
               Console.Write("{0}\t", dataTableReader.GetValue(i).ToString().Trim());
            }
            Console.WriteLine();
         }
         dataTableReader.Close();
      }
      #endregion
    
      static void SaveAndLoadAsXml(DataSet carsInventoryDataSet)   // Сохранение набора данных в виде XML
      {
         // Сохранение данного DataSet в виде XML.
         carsInventoryDataSet.WriteXml("carsDataSet.xml");
         carsInventoryDataSet.WriteXmlSchema("carsDataSet.xsd");

         // Очистка DataSet
         carsInventoryDataSet.Clear();

         // Загрузка DataSet из XML-файла.
         carsInventoryDataSet.ReadXml("carsDataSet.xml");
      }

      static void SaveAndLoadAsBinary(DataSet aDataSet)
      {
         // Установка признака двоичной сериализации.
         aDataSet.RemotingFormat = SerializationFormat.Binary;

         // Сохранение данного DataSet в двоичном виде.
         FileStream fileStream;
         BinaryFormatter binaryFormatter;
         using (fileStream = new FileStream("BinaryCars.dat", FileMode.Create))
         {            
            binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, aDataSet);            
         }

         // Очистка DataSet
         aDataSet.Clear();

         // Загрузка DataSet из двоичного файла
         using (fileStream = new FileStream("BinaryCars.dat", FileMode.Open))
         {
            DataSet data = (DataSet) binaryFormatter.Deserialize(fileStream);
         }
      }
   }
}
