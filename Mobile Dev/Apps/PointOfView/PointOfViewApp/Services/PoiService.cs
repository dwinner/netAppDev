using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PointOfViewApp.Poco;

namespace PointOfViewApp.Services
{
   /// <summary>
   ///    POI Service
   /// </summary>
   public class PoiService
   {
      private const string GetAllPointOfInterestUrl =
         "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/pois";

      private const string CreatePoiUrl =
         "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/create";

      private const string DeletePoi =
         "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/delete/{0}";

      private const string UploadPoi =
         "http://YOUR_IP:8080/com.packt.poiapp/api/poi/upload";

      private const string PoiJsonArrayName = "pois";
      private readonly List<PointOfInterest> _poiList = new List<PointOfInterest>();

      /// <summary>
      ///    Get all points of interest objects
      /// </summary>
      /// <returns>Points of interest</returns>
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
            {
               _poiList.Clear();
            }

            results.ForEach(token => _poiList.Add(token.ToObject<PointOfInterest>()));

            return _poiList;
         }

         return null;
      }

      /// <summary>
      ///    Create Or Update Point Of interest
      /// </summary>
      /// <param name="poi">Point Of interest object</param>
      /// <returns>Updated content Or null, if nothing's been changed</returns>
      public async Task<string> CreateOrUpdateAsync(PointOfInterest poi)
      {
         var settings = new JsonSerializerSettings {ContractResolver = new PoiContractResolver()};
         var poiJson = JsonConvert.SerializeObject(poi, Formatting.Indented, settings);

         var httpClient = new HttpClient();
         var jsonContent = new StringContent(poiJson, Encoding.UTF8, "application/json");
         var response = await httpClient.PostAsync(CreatePoiUrl, jsonContent).ConfigureAwait(false);
         if (response?.IsSuccessStatusCode == true)
         {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
         }

         return null;
      }

      public async Task<string> CreateOrUpdateAsync(PointOfInterest interest, Bitmap bitmap)
      {
         var settings = new JsonSerializerSettings {ContractResolver = new PoiContractResolver()};
         var poiJson = JsonConvert.SerializeObject(interest, Formatting.None, settings);
         var stringContent = new StringContent(poiJson);

         // Convert the bitmap image to a byte array
         byte[] bitmapData;
         using (var stream = new MemoryStream())
         {
            await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, 0, stream).ConfigureAwait(false);
            bitmapData = stream.ToArray();
         }

         // Configure the headers
         var fileContent = new ByteArrayContent(bitmapData);
         fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
         fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
         {
            Name = "file",
            FileName = $"poiimage{interest.Id}.jpg"
         };
         const string boundary = "---8d0f01e6b3b5daf";
         var multipartContent = new MultipartFormDataContent(boundary)
         {
            fileContent, {stringContent, "poi"}
         };

         var httpClient = new HttpClient();
         var response = await httpClient.PostAsync(UploadPoi, multipartContent).ConfigureAwait(true);
         if (response?.IsSuccessStatusCode == true)
         {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            return content;
         }

         return null;
      }

      /// <summary>
      ///    Delete Point of interest object
      /// </summary>
      /// <param name="poiId">POI-id</param>
      /// <param name="photoPath">Photo path</param>
      /// <returns>Updated content Or null, if nothing's been changed</returns>
      public async Task<string> DeletePoiAsync(int poiId, string photoPath)
      {
         var httpClient = new HttpClient();
         var deleteEndPoint = string.Format(DeletePoi, poiId);
         var response = await httpClient.DeleteAsync(deleteEndPoint).ConfigureAwait(false);
         if (response?.IsSuccessStatusCode == true)
         {
            if (File.Exists(photoPath))
            {
               File.Delete(photoPath);
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
         }

         return null;
      }

      private sealed class PoiContractResolver : DefaultContractResolver
      {
         protected override string ResolvePropertyName(string propertyName) => propertyName.ToLower();
      }
   }
}