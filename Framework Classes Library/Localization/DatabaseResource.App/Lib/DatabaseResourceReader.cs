using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Resources;

namespace DatabaseResource.App.Lib
{
   public sealed class DatabaseResourceReader : IResourceReader
   {
      private readonly string _connectionString;
      private readonly string _language;

      public DatabaseResourceReader(string connectionString, CultureInfo language)
      {
         _connectionString = connectionString;
         var langName = language.Name;
         _language = string.IsNullOrEmpty(langName) ? "Default" : langName;
      }

      public void Close()
      {
      }

      public IDictionaryEnumerator GetEnumerator()
      {
         var dictionary = new Dictionary<string, string>();
         using (var i18NConn = new SqlConnection(_connectionString))
         using (var locMessageCmd = i18NConn.CreateCommand())
         {
            locMessageCmd.CommandText = string.Format("SELECT [Key], [{0}] FROM LocMessages", _language);

            try
            {
               i18NConn.Open();
               var reader = locMessageCmd.ExecuteReader();
               while (reader.Read())
               {
                  if (reader.GetValue(1) != DBNull.Value)
                  {
                     dictionary.Add(reader.GetString(0).Trim(), reader.GetString(1));
                  }
               }               
            }
            catch (SqlException sqlEx)
            {
               if (sqlEx.Number != 207) // Игнорировать отсутствующие столбцы в базе данных
               {
                  throw; // Повторно генерировать все прочие исключения
               }
            }
         }

         return dictionary.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      void IDisposable.Dispose()
      {
      }
   }
}