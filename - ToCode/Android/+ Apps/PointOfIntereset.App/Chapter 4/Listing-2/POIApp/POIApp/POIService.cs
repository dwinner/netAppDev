using System;
using Android.Content;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace POIApp
{
	public class POIService
	{
		// APIARY Mock server test feed urls
		private const string GET_POIS = "http://private-e451d-poilist.apiary-mock.com/com.packt.poiapp/api/poi/pois";
		//private const string GET_POIS = "http://192.168.1.108:8080/com.packt.poiapp/api/poi/pois";

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
	}
}

