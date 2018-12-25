using Android.Gms.Maps;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Util;
using FlickrLocart_App.Middleware;
using FlickrLocart_App.Model;
using Java.IO;
using Java.Lang;
using ILocationListener = Android.Gms.Location.ILocationListener;

namespace FlickrLocator.App
{
   public partial class LocatorFragment
   {
      private sealed class SearchTask : AsyncTask<Location, Void, Void>
      {
         private Bitmap _bitmap;
         private GalleryItem _galleryItem;
         private Location _location;
         private readonly LocatorFragment _locatorFragment;

         public SearchTask(LocatorFragment locatorFragment) => _locatorFragment = locatorFragment;

         protected override Void RunInBackground(params Location[] @params)
         {
            if (@params.Length <= 0)
            {
               return null;
            }

            _location = @params[0];
            var fetchr = new FlickrFetchr();
            var items = fetchr.SearchPhotos(_location);
            if (items.Count == 0)
            {
               return null;
            }

            _galleryItem = items[0];
            try
            {
               var imageBytes = fetchr.GetUrlBytes(_galleryItem.Url);
               _bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
            catch (IOException ioEx)
            {
               Log.Info(ErrorTag, "Unable to decode bitmap", ioEx);
            }

            return null;
         }

         protected override void OnPostExecute(Void result)
         {
            _locatorFragment._mapImage = _bitmap;
            _locatorFragment._mapItem = _galleryItem;
            _locatorFragment._currentLocation = _location;
            _locatorFragment.UpdateUi();
         }
      }

      private sealed class LocationListenerImpl : Object, ILocationListener
      {
         private readonly LocatorFragment _fragment;

         public LocationListenerImpl(LocatorFragment fragment) => _fragment = fragment;

         public void OnLocationChanged(Location location)
         {
            Log.Info(ErrorTag, $"Got a fix: {location}");
            new SearchTask(_fragment).Execute(location);
         }
      }

      private sealed class MapReadyCallbackImpl : Object, IOnMapReadyCallback
      {
         private readonly LocatorFragment _fragment;

         public MapReadyCallbackImpl(LocatorFragment fragment) => _fragment = fragment;

         public void OnMapReady(GoogleMap googleMap)
         {
            _fragment._map = googleMap;
            _fragment.UpdateUi();
         }
      }
   }
}