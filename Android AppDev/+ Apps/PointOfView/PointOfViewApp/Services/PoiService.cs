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
      private readonly List<PointOfInterest> _poiListAsync = new List<PointOfInterest>();

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
            if (_poiListAsync.Count > 0)
               _poiListAsync.Clear();
            results.ForEach(token => _poiListAsync.Add(token.ToObject<PointOfInterest>()));

            return _poiListAsync;
         }

         return null;
      }
   }
}