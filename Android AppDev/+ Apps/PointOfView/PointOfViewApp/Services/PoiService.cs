using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.Content;
using Android.Net;
using Newtonsoft.Json.Linq;
using PointOfViewApp.Poco;

namespace PointOfViewApp.Services
{
   public class PoiService
   {
      private const string GetPois = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/pois";

      public async Task<List<PointOfInterest>> GetPoiListAsync()
      {
         var httpClient = new HttpClient();
         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         var response = await httpClient.GetAsync(GetPois).ConfigureAwait(true);

         if (response?.IsSuccessStatusCode == true)
         {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var jsonResponse = JObject.Parse(content);
            var results = jsonResponse["pois"].ToList();

            var poiListData = new List<PointOfInterest>();
            results.ForEach(token => poiListData.Add(token.ToObject<PointOfInterest>()));

            return poiListData;
         }

         return null;
      }

      public static bool IsConnected(Context context)
      {
         var connectivityManager = (ConnectivityManager) context.GetSystemService(Context.ConnectivityService);
         var activeConnection = connectivityManager.ActiveNetworkInfo;
         return activeConnection?.IsConnected == true;
      }
   }
}