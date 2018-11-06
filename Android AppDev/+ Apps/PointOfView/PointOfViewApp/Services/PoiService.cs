using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PointOfViewApp.Poco;

namespace PointOfViewApp.Services
{
   public class PoiService
   {
      private const string GetAllPointOfInterestUrl =
         "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/pois";

      private const string PoiJsonArrayName = "pois";
      private readonly List<PointOfInterest> _poiList = new List<PointOfInterest>();

      public async Task<List<PointOfInterest>> GetPoiListAsync()
      {
         var httpClient = new HttpClient();
         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         var response = await httpClient.GetAsync(GetAllPointOfInterestUrl).ConfigureAwait(true);

         if (response?.IsSuccessStatusCode == true)
         {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            var jsonResponse = JObject.Parse(content);
            var results = jsonResponse[PoiJsonArrayName].ToList();
            if (_poiList.Count > 0)
               _poiList.Clear();
            results.ForEach(token => _poiList.Add(token.ToObject<PointOfInterest>()));

            return _poiList;
         }

         return null;
      }
   }
}