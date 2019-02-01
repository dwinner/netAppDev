using Android.Content;
using Android.Net;
using Android.Provider;

namespace AppDevUnited.AddressBook.App.Data
{
   /**
    * Класс описывает имя таблицы и имена столбцов базы данных,
    * а также содержит другую информацию, необходимую ContentProvider
    */     
   public static class DatabaseDescription
   {
      /// <summary>
      ///    Имя Content provider
      ///    <remarks>Обычно совпадает с именем пакета</remarks>
      /// </summary>
      public const string Authority = "AppDevUnited.AddressBook.App.Data";

      /// <summary>
      ///    Базовый URI для взаимодействия с ContentProvider
      /// </summary>
      private static readonly Uri _BaseContentUri = Uri.Parse($"content://{Authority}");

      /// <summary>
      ///    Вложенный класс, определяющий содержимое таблицы contacts
      /// </summary>
      public static class Contact // BUG нужно отнаследовать от : BaseColumns, из-за константы _ID="_id"
      {
         /// <summary>
         ///    Имя таблицы
         /// </summary>
         public const string TableName = "contacts";

         // Имена столбцов таблицы
         public const string ColumnName = "name";
         public const string ColumnPhone = "phone";
         public const string ColumnEmail = "email";
         public const string ColumnStreet = "street";
         public const string ColumnCity = "city";
         public const string ColumnState = "state";
         public const string ColumnZip = "zip";

         /// <summary>
         ///    Объект Uri для таблицы contacts
         /// </summary>
         public static readonly Uri ContentUri = _BaseContentUri.BuildUpon().AppendPath(TableName).Build();

         public const string Id = BaseColumns.Id;

         /// <summary>
         ///    Создание Uri для конкретного контакта
         /// </summary>
         /// <param name="id">ID-контакта</param>
         /// <returns>Uri для конкретного контакта</returns>
         public static Uri BuildContactUri(long id) => ContentUris.WithAppendedId(ContentUri, id);
      }
   }
}