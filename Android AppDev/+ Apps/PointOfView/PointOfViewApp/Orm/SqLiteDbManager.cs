using System;
using System.Collections.Generic;
using System.IO;
using PointOfViewApp.Poco;
using SQLite;

namespace PointOfViewApp.Orm
{
   /// <summary>
   ///    SQLite wrapper class for incapsulating CRUD-operations
   /// </summary>
   public class SqLiteDbManager
   {
      private const string DbName = "PointOfInterest_DB.db3";
      private SQLiteConnection _dbConn;

      private SqLiteDbManager()
      {
      }

      public static SqLiteDbManager Instance { get; } = new SqLiteDbManager();

      /// <summary>
      ///    Create table
      /// </summary>
      public void CreateTable()
      {
         var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         _dbConn = new SQLiteConnection(Path.Combine(path, DbName));
         _dbConn.CreateTable<PointOfInterest>();
      }

      /// <summary>
      ///    Save the record
      /// </summary>
      /// <param name="poiToSave">Item to be saved</param>
      /// <returns>Saved result</returns>
      public int Save(PointOfInterest poiToSave) => _dbConn.InsertOrReplace(poiToSave);

      /// <summary>
      ///    Save the records
      /// </summary>
      /// <param name="interests">Interests</param>
      /// <returns>Save result</returns>
      public int Save(IEnumerable<PointOfInterest> interests) => _dbConn.InsertAll(interests);

      /// <summary>
      ///    Select all records
      /// </summary>
      /// <returns>All table records</returns>
      public List<PointOfInterest> Select() => _dbConn.Table<PointOfInterest>().ToList();

      /// <summary>
      ///    Select a single record
      /// </summary>
      /// <param name="poiId">Id</param>
      /// <returns>Single record</returns>
      public PointOfInterest Select(int poiId) =>
         _dbConn.Table<PointOfInterest>().FirstOrDefault(interest => interest.Id.Equals(poiId));

      /// <summary>
      ///    Delete single record
      /// </summary>
      /// <param name="poiId">Record Id</param>
      /// <returns>Delete result</returns>
      public int Delete(int poiId) => _dbConn.Delete<PointOfInterest>(poiId);

      /// <summary>
      ///    Delete all records
      /// </summary>
      /// <returns>Delete result</returns>
      public int Delete() => _dbConn.DeleteAll<PointOfInterest>();
   }
}