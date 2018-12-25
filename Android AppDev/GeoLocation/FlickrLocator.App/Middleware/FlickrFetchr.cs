using System;
using System.Collections.Generic;
using Android.Locations;
using Android.Util;
using FlickrLocart_App.Model;
using Java.IO;
using Java.Net;
using Org.Json;
using AndroidUri = Android.Net.Uri;

namespace FlickrLocart_App.Middleware
{
   public class FlickrFetchr
   {
      private const string Tag = nameof(FlickrFetchr);
      private const string ApiKey = ""; // TODO: Insert your API key here
      private const string FetchRecentsMethod = "flickr.photos.getRecent";
      private const string SearchMethod = "flickr.photos.search";

      private static readonly AndroidUri _EndPoint = AndroidUri.Parse("https://api.flickr.com/services/rest/")
         .BuildUpon()
         .AppendQueryParameter("api_key", ApiKey)
         .AppendQueryParameter("format", "json")
         .AppendQueryParameter("nojsoncallback", "1")
         .AppendQueryParameter("extras", "url_s,geo")
         .Build();

      public List<GalleryItem> SearchPhotos(Location location)
      {
         var url = BuildUrl(location);
         return DownloadGalleryItems(url);
      }

      public List<GalleryItem> SearchPhotos(string query)
      {
         var url = BuildUrl(SearchMethod, query);
         return DownloadGalleryItems(url);
      }

      public List<GalleryItem> FetchRecentPhotos()
      {
         var url = BuildUrl(FetchRecentsMethod, null);
         return DownloadGalleryItems(url);
      }

      public byte[] GetUrlBytes(string urlSpec)
      {
         using (var url = new URL(urlSpec))
         using (var connection = (HttpURLConnection) url.OpenConnection())
         using (var @out = new ByteArrayOutputStream())
         {
            var @in = connection.InputStream;
            if (connection.ResponseCode != HttpStatus.Ok)
            {
               throw new IOException($"{connection.ResponseMessage}: with {urlSpec}");
            }

            int bytesRead;
            var buffer = new byte[1024];
            while ((bytesRead = @in.Read(buffer, 0, 1024)) > 0) @out.Write(buffer, 0, bytesRead);

            return @out.ToByteArray();
         }
      }

      private List<GalleryItem> DownloadGalleryItems(string url)
      {
         var items = new List<GalleryItem>();

         try
         {
            var jsonString = GetUrlString(url);
            Log.Info(Tag, $"Received JSON: {jsonString}");
            var jsonBody = new JSONObject(jsonString);
            ParseItems(items, jsonBody);
         }
         catch (IOException ioEx)
         {
            Log.Error(Tag, "Failed to fetch items", ioEx);
         }
         catch (JSONException jsonEx)
         {
            Log.Error(Tag, "Failed to parse JSON", jsonEx);
         }

         return items;
      }

      private string GetUrlString(string urlSpec)
      {
         var urlChars = Array.ConvertAll(GetUrlBytes(urlSpec), Convert.ToChar);
         return new string(urlChars);
      }

      private string BuildUrl(string method, string query)
      {
         var uriBuilder = _EndPoint.BuildUpon().AppendQueryParameter("method", method);
         if (method.Equals(SearchMethod))
         {
            uriBuilder.AppendQueryParameter("text", query);
         }

         return uriBuilder.Build().ToString();
      }

      private string BuildUrl(Location location) => _EndPoint.BuildUpon()
         .AppendQueryParameter("method", SearchMethod)
         .AppendQueryParameter("lat", $"{location.Latitude}")
         .AppendQueryParameter("lon", $"{location.Longitude}")
         .Build()
         .ToString();

      private void ParseItems(List<GalleryItem> items, JSONObject jsonBody)
      {
         var photosJsonObject = jsonBody.GetJSONObject("photos");
         var photoJsonArray = photosJsonObject.GetJSONArray("photo");

         for (var i = 0; i < photoJsonArray.Length(); i++)
         {
            var photoJsonObject = photoJsonArray.GetJSONObject(i);
            var item = new GalleryItem
            {
               Id = photoJsonObject.GetString("id"),
               Caption = photoJsonObject.GetString("title")
            };

            if (!photoJsonObject.Has("url_s"))
            {
               continue;
            }

            item.Url = photoJsonObject.GetString("url_s");
            item.Owner = photoJsonObject.GetString("owner");
            item.Lat = photoJsonObject.GetDouble("latitude");
            item.Lon = photoJsonObject.GetDouble("longitude");
            items.Add(item);
         }
      }
   }
}