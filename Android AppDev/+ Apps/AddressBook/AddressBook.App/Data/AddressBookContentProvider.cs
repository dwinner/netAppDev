using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Net;
using Java.Lang;
using static AppDevUnited.AddressBook.App.Data.DatabaseDescription;
using static AppDevUnited.AddressBook.App.Data.DatabaseDescription.Contact;
using AppStrings = AppDevUnited.AddressBook.App.Resource.String;

namespace AppDevUnited.AddressBook.App.Data
{
   /// <summary>
   ///    Подкласс ContentProvider для работы с базой данных приложения
   /// </summary>
   public sealed class AddressBookContentProvider : ContentProvider
   {
      // Константы, используемые для определения выполняемой операции TODO: Use enums
      private const int OneContact = 1; // Один контакт
      private const int Contacts = 2; // Таблица контактов

      // UriMatcher помогает ContentProvider определить выполняемую операцию
      private static readonly UriMatcher _UriMatcher = new UriMatcher(UriMatcher.NoMatch);
      private AddressBookDatabaseHelper _dbHelper; // Используется для обращения к базе данных

      static AddressBookContentProvider()
      {
         // Uri для контакта с заданным идентификатором
         _UriMatcher.AddURI(Authority, $"{TableName}/#", OneContact);

         // Uri для таблицы
         _UriMatcher.AddURI(Authority, TableName, Contacts);
      }

      public override int Delete(Uri uri, string selection, string[] selectionArgs)
      {
         int deletedCount;

         switch (_UriMatcher.Match(uri))
         {
            case OneContact:
               // Получение из URI идентификатор контакта
               var id = uri.LastPathSegment;

               // Удаление контакта
               deletedCount = _dbHelper.WritableDatabase.Delete(TableName, $"{Id}={id}", selectionArgs);
               break;

            default:
               throw new UnsupportedOperationException($"{Context.GetString(AppStrings.invalid_delete_uri)}{uri}");
         }

         // Оповестить наблюдателей об изменениях в базе данных
         if (deletedCount != 0) Context.ContentResolver.NotifyChange(uri, null);

         return deletedCount;
      }

      public override string GetType(Uri uri) => null;

      public override Uri Insert(Uri uri, ContentValues values)
      {
         Uri newContactUri;

         switch (_UriMatcher.Match(uri))
         {
            case Contacts: // При успехе возвращается идентификатор записи нового контакта
               var rowId = _dbHelper.WritableDatabase.Insert(TableName, null, values);

               // Если контакт был вставлен, создать подходящий Uri, в противном случае выдать исключение
               if (rowId > 0)
               {
                  newContactUri = BuildContactUri(rowId);

                  // Оповестить наблюдателей об изменениях в базе данных
                  Context.ContentResolver.NotifyChange(uri, null);
               }
               else
               {
                  throw new SQLException($"{Context.GetString(AppStrings.insert_failed)}{uri}");
               }

               break;

            default:
               throw new UnsupportedOperationException($"{Context.GetString(AppStrings.invalid_insert_uri)}{uri}");
         }

         return newContactUri;
      }

      /// <summary>
      ///    Вызывается при создании AddressBookDatabaseHelper
      /// </summary>
      /// <returns>bool</returns>
      public override bool OnCreate()
      {
         _dbHelper = new AddressBookDatabaseHelper(Context);
         return true;
      }

      public override ICursor Query(Uri uri, string[] projection, string selection, string[] selectionArgs,
         string sortOrder)
      {
         // Создание SQLiteQueryBuilder для запроса к таблице contacts
         var queryBuilder = new SQLiteQueryBuilder {Tables = TableName};

         switch (_UriMatcher.Match(uri))
         {
            case OneContact: // Выбрать контакт с заданным идентификатором
               queryBuilder.AppendWhere($"{Id}={uri.LastPathSegment}");
               break;

            case Contacts: // Выбрать все контакты
               break;

            default:
               throw new UnsupportedOperationException($"{Context.GetString(AppStrings.invalid_query_uri)}{uri}");
         }

         // Выполнить запрос для получения одного или всех контактов
         var cursor = queryBuilder.Query(_dbHelper.ReadableDatabase, projection, selection, selectionArgs, null, null,
            sortOrder);

         // Настройка отслеживания изменений в контенте
         cursor.SetNotificationUri(Context.ContentResolver, uri);

         return cursor;
      }

      public override int Update(Uri uri, ContentValues values, string selection, string[] selectionArgs)
      {
         int updatedCount; // 1, если обновление успешно; 0 при неудаче

         switch (_UriMatcher.Match(uri))
         {
            case OneContact:
               // Получение идентификатора контакта из Uri
               var id = uri.LastPathSegment;

               // Обновление контакта
               updatedCount = _dbHelper.WritableDatabase.Update(TableName, values, $"{Id}={id}", selectionArgs);
               break;

            default:
               throw new UnsupportedOperationException($"{Context.GetString(AppStrings.invalid_update_uri)}{uri}");
         }

         // Если были внесены изменения, оповестить наблюдателей
         if (updatedCount != 0) Context.ContentResolver.NotifyChange(uri, null);

         return updatedCount;
      }
   }
}