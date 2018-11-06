using System.Collections.Generic;
using Android.App;
using Android.Content;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SatelliteMovingApp.Code
{
   public sealed class SatelliteJsonSerializer
   {
      private const string SatellitePrefKey = nameof(SatellitePrefKey);
      private readonly Activity _activity;

      public SatelliteJsonSerializer(Activity activity) => _activity = activity;

      public IList<Satellite> Read()
      {
         IList<Satellite> satellites = new List<Satellite>();
         var preferences = _activity.GetPreferences(FileCreationMode.Private);

         if (preferences.Contains(SatellitePrefKey))
         {
            var jsonSatellites = preferences.GetString(SatellitePrefKey, string.Empty);
            if (!string.IsNullOrEmpty(jsonSatellites))
               satellites = JsonConvert.DeserializeObject<List<Satellite>>(jsonSatellites);
         }

         return satellites;
      }

      public void Save(IList<Satellite> satellites)
      {
         var satelliteJson = JsonConvert.SerializeObject(satellites, Formatting.Indented,
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
         var preferences = _activity.GetPreferences(FileCreationMode.Private);
         var editor = preferences.Edit();
         editor.PutString(SatellitePrefKey, satelliteJson);
         editor.Commit();
      }
   }
}