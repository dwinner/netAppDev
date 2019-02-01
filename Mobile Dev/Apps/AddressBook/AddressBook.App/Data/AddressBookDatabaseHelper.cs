using Android.Content;
using Android.Database.Sqlite;
using static AppDevUnited.AddressBook.App.Data.DatabaseDescription.Contact;

namespace AppDevUnited.AddressBook.App.Data
{
   /// <summary>
   ///    Подкласс SQLiteOpenHelper, определяющий базу данных приложения
   /// </summary>
   public sealed class AddressBookDatabaseHelper : SQLiteOpenHelper
   {
      private const string DbName = "AddressBook.db";
      private const int DatabaseVersion = 1;

      public AddressBookDatabaseHelper(Context context)
         : base(context, DbName, null, DatabaseVersion)
      {
      }

      /// <summary>
      ///    Создание таблицы Contacts при создании БД
      /// </summary>
      /// <param name="db"></param>      
      /// <exception cref="T:Android.Database.SQLException">if the SQL string is invalid</exception>
      public override void OnCreate(SQLiteDatabase db)
      {
         var createContactsTable =
            $"CREATE TABLE {TableName}(" +
            $"{Id} integer primary key," +
            $"{ColumnName} TEXT," +
            $"{ColumnPhone} TEXT," +
            $"{ColumnEmail} TEXT," +
            $"{ColumnStreet} TEXT," +
            $"{ColumnCity} TEXT," +
            $"{ColumnState} TEXT," +
            $"{ColumnZip} TEXT);";
         db.ExecSQL(createContactsTable); // Создание таблицы Contacts
      }

      public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
      {
         // Обычно определяет способ обновления при изменении схемы базы данных
      }
   }
}