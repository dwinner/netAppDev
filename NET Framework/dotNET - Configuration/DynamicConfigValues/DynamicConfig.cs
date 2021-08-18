using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;

namespace DynConfReading
{
   public sealed class DynamicConfig : DynamicObject
   {
      private readonly AppSettingsReader _appSettingsReader;
      private readonly Dictionary<string, dynamic> _configValues = new Dictionary<string, dynamic>();

      public DynamicConfig()
      {
         _appSettingsReader = new AppSettingsReader();
      }

      public override bool TryGetMember(GetMemberBinder binder, out object result)
      {
         bool success;
         if (_configValues.ContainsKey(binder.Name))
         {
            result = _configValues[binder.Name];
            success = true;
         }
         else
         {
            success = TryGet(binder.Name, out result);
            if (success) _configValues.Add(binder.Name, result);
         }

         return success;
      }

      private T Get<T>(string key)
      {
         try
         {
            return (T) _appSettingsReader.GetValue(key, typeof (T));
         }
         catch (InvalidOperationException invOpEx)
         {
            throw new InvalidSettingsException($"Key {key} does not exists in App.config or incompatible type", invOpEx);
         }
         catch (ArgumentNullException argNullEx)
         {
            throw new InvalidSettingsException($"Key {key} is not found", argNullEx);
         }
      }

      private bool TryGet<T>(string key, out T configValue)
      {
         try
         {
            configValue = Get<T>(key);
            return true;
         }
         catch (InvalidSettingsException)
         {
            configValue = default(T);
            return false;
         }
      }
   }
}