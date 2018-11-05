using System;
using Android.Content;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Android.Net;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace POIApp
{
	public class POIService
	{
		// APIARY Mock server test feed urls
		private const string GET_POIS = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/pois";
		private const string CREATE_POI = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/create";
		private const string DELETE_POI = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/delete";

		//private const string GET_POIS = "http://<YOUR_SERVER_IP>:8080/com.packt.poiapp/api/poi/pois";
		//private const string CREATE_POI = "http://<YOUR_SERVER_IP>:8080/com.packt.poiapp/api/poi/create";
		//private const string DELETE_POI = "http://<YOUR_SERVER_IP>:8080/com.packt.poiapp/api/poi/delete/{0}";

		public async Task<List<PointOfInterest>> GetPOIListAsync() {
			HttpClient httpClient = new HttpClient ();

			// Adding Accept-Type as application/json header
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			HttpResponseMessage response = await httpClient.GetAsync (GET_POIS);
			if (response != null || response.IsSuccessStatusCode) {

				// await! control returns to the caller and the task continues 
				string content = await response.Content.ReadAsStringAsync ();

				// Printing response body in console 
				Console.Out.WriteLine ("Response Body: \r\n {0}", content);

				// Initialize the poi list 
				var poiListData = new List<PointOfInterest> ();

				// Load a JObject from response string
				JObject jsonResponse = JObject.Parse (content);
				IList<JToken> results = jsonResponse ["pois"].ToList ();
				foreach (JToken token in results) {
					PointOfInterest poi = token.ToObject<PointOfInterest> ();
					poiListData.Add (poi);
				}

				return poiListData;
			} else {
				Console.Out.WriteLine("Failed to fetch data. Try again later!");
				return null;
			}

		}

		public bool isConnected(Context activity){
			var connectivityManager = (ConnectivityManager)activity.GetSystemService (Context.ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;
			return (null != activeConnection && activeConnection.IsConnected);
		}


		public async Task<String> CreateOrUpdatePOIAsync (PointOfInterest poi)
		{
			var settings = new JsonSerializerSettings ();
			settings.ContractResolver = new POIContractResolver ();
			var poiJson = JsonConvert.SerializeObject (poi, Formatting.Indented, settings);

			HttpClient httpClient = new HttpClient ();
			StringContent jsonContent = new StringContent (poiJson, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await httpClient.PostAsync (CREATE_POI, jsonContent);

			if (response != null || response.IsSuccessStatusCode) {
				string content = await response.Content.ReadAsStringAsync ();
				Console.Out.WriteLine ("{0} saved.", poi.Name); 

				return content;
			}	
			return null;
		}

		/**
		 * Converts all json keys into lowercase
		 */
		public class POIContractResolver : DefaultContractResolver
		{
			protected override string ResolvePropertyName(string key)
			{
				return key.ToLower();
			}
		}


		public async Task<String> DeletePOIAsync (int poiId)
		{	
			HttpClient httpClient = new HttpClient ();
			String url = String.Format (DELETE_POI, poiId);
			HttpResponseMessage response = await httpClient.DeleteAsync (url);
			if (response != null || response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				Console.Out.WriteLine ("One record deleted.");
				return content;
			}
			return null;
		}
	}
}

