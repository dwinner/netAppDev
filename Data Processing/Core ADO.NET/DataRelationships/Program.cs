/**
 * Отношения между данными
 */

using System;
using System.Data;

namespace DataRelationships
{
   internal class Program
   {
      private static void Main()
      {
         // Создаем набор данных, добавляем таблицы и отношения
         DataSet relDataSet = CreateDataSetWithRelationships();

         // Добавляем некоторые данные к таблицам
         DataRow buildingDataRow = relDataSet.Tables["Building"].NewRow();
         buildingDataRow["BuildingId"] = 1;
         buildingDataRow["Name"] = "The Lowry";

         relDataSet.Tables["Building"].Rows.Add(buildingDataRow);

         DataRow roomDataRow = relDataSet.Tables["Room"].NewRow();
         roomDataRow["RoomId"] = 1;
         roomDataRow["Name"] = "Reception";
         roomDataRow["BuildingId"] = 1;

         relDataSet.Tables["Room"].Rows.Add(roomDataRow);

         roomDataRow = relDataSet.Tables["Room"].NewRow();
         roomDataRow["RoomID"] = 2;
         roomDataRow["Name"] = "The Modern Art Gallery";
         roomDataRow["BuildingID"] = 1;

         relDataSet.Tables["Room"].Rows.Add(roomDataRow);

         // NOTE: Используем родительские отношения для прохода по данным
         foreach (DataRow building in relDataSet.Tables["Building"].Rows)
         {
            DataRow[] children = building.GetChildRows("Rooms");
            int roomCount = children.Length;

            Console.WriteLine("Building {0} contains {1} room{2}", building["Name"], roomCount,
               roomCount > 1 ? "s" : string.Empty);

            // Проходим через объекты Room
            foreach (DataRow room in children)
               Console.WriteLine("Room: {0}", room["Name"]);
         }

         // NOTE: Используем дочерние отношения для прохода по данным
         foreach (DataRow room in relDataSet.Tables["Room"].Rows)
         {
            DataRow[] parents = room.GetParentRows("Rooms");

            foreach (DataRow building in parents)
               Console.WriteLine("Room {0} is contained in building {1}", room["Name"], building["Name"]);
         }
      }

      public static DataTable CreateRoomTable()
      {
         var aRoomDataTable = new DataTable("Room");
         aRoomDataTable.Columns.Add(new DataColumn("RoomId", typeof (int)));
         aRoomDataTable.Columns.Add(new DataColumn("Name", typeof (string)));
         aRoomDataTable.Columns.Add(new DataColumn("BuildingId", typeof (int)));
         aRoomDataTable.Constraints.Add(new UniqueConstraint("PK_Room", aRoomDataTable.Columns[0]));
         aRoomDataTable.PrimaryKey = new[] {aRoomDataTable.Columns[0]};

         return aRoomDataTable;
      }

      public static DataTable CreateBuildingTable()
      {
         var buildingDataTable = new DataTable("Building");
         buildingDataTable.Columns.Add(new DataColumn("BuildingId", typeof (int)));
         buildingDataTable.Columns.Add(new DataColumn("Name", typeof (string)));
         buildingDataTable.Constraints.Add(new UniqueConstraint("PK_Building", buildingDataTable.Columns[0]));
         buildingDataTable.PrimaryKey = new[] {buildingDataTable.Columns[0]};

         return buildingDataTable;
      }

      public static DataSet CreateDataSetWithRelationships()
      {
         var relDataSet = new DataSet("Relationships");

         relDataSet.Tables.Add(CreateBuildingTable());
         relDataSet.Tables.Add(CreateRoomTable());

         // Создаем простое отношение между таблицами
         relDataSet.Relations.Add("Rooms", relDataSet.Tables["Building"].Columns["BuildingId"],
            relDataSet.Tables["Room"].Columns["BuildingId"]);

         return relDataSet;
      }
   }
}