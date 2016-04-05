using System;
using System.Configuration;
using System.Data.Common;

namespace Connection.Library
{
   /// <summary>
   /// Провайдер подключения к Базе данных
   /// </summary>
   public static class DatabaseProvider
   {
      /// <summary>
      /// Получает подключение по имени строки подключения в конфигурации
      /// </summary>
      /// <param name="name">Имя строки подключения в конфигурации</param>
      /// <returns>Объект подключения</returns>
      public static DbConnection GetDatabaseConnection(string name)
      {
         ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
         DbProviderFactory providerFactory = DbProviderFactories.GetFactory(settings.ProviderName);
         DbConnection connection = providerFactory.CreateConnection();
         if (connection != null)
         {
            connection.ConnectionString = settings.ConnectionString;
            return connection;
         }

         throw new NotSupportedException(string.Format("{0} not supported", name));
      }
   }
}