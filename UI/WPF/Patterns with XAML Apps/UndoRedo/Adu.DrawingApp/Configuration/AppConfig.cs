using System;
using System.Configuration;

namespace Adu.DrawingApp.Configuration
{
   /// <summary>
   ///    Application config reader
   /// </summary>
   public sealed class AppConfig
   {      
      private readonly AppSettingsReader _appSettings = new AppSettingsReader();

      /// <summary>
      ///    Limitation threshold value for undo/redo history
      /// </summary>
      public int HistoryLimit => Get<int>("HistoryLimit");

      private T Get<T>(string key)
      {
         try
         {
            return (T) _appSettings.GetValue(key, typeof (T));
         }
         catch (InvalidOperationException invalidOperationEx)
         {
            throw new InvalidSettingsException($"Key {key} does not exists in App.config or incompatible type",
               invalidOperationEx);
         }
      }
   }
}