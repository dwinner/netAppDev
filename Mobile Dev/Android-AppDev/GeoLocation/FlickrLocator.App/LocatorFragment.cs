using Android;
using Android.Content.PM;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using FlickrLocart_App.Model;
using ILocationListener = Android.Gms.Location.ILocationListener;
using ResDimensions = FlickrLocator.App.Resource.Dimension;
using ResId = FlickrLocator.App.Resource.Id;
using ResMenu = FlickrLocator.App.Resource.Menu;

namespace FlickrLocator.App
{
   public partial class LocatorFragment : SupportMapFragment
   {
      private const string ErrorTag = nameof(LocatorFragment);
      private const int RequestLocationPermission = 0;

      private static readonly string[] _LocationPermissions =
      {
         Manifest.Permission.AccessFineLocation,
         Manifest.Permission.AccessCoarseLocation
      };

      private GoogleApiClient _client;
      private Location _currentLocation;
      private GoogleMap _map;
      private Bitmap _mapImage;
      private GalleryItem _mapItem;

      public static Fragment Instance => new LocatorFragment();

      private bool HasLocationPermission =>
         ContextCompat.CheckSelfPermission(Activity, _LocationPermissions[0]) == Permission.Granted;

      public override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         HasOptionsMenu = true;
         _client = new GoogleApiClient.Builder(Activity)
            .AddApi(LocationServices.API)
            .AddConnectionCallbacks(bundle => Activity.InvalidateOptionsMenu())
            .Build();
         IOnMapReadyCallback mapReadyCallback = new MapReadyCallbackImpl(this);
         GetMapAsync(mapReadyCallback);
      }

      public override void OnStart()
      {
         base.OnStart();
         Activity.InvalidateOptionsMenu();
         _client.Connect();
      }

      public override void OnStop()
      {
         base.OnStop();
         _client.Disconnect();
      }

      public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
      {
         base.OnCreateOptionsMenu(menu, inflater);
         inflater.Inflate(ResMenu.FragmentLocator, menu);
         var searchItem = menu.FindItem(ResId.action_locate);
         searchItem.SetEnabled(_client.IsConnected);
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         switch (item.ItemId)
         {
            case ResId.action_locate:
               if (HasLocationPermission)
               {
                  FindImage();
               }
               else
               {
                  RequestPermissions(_LocationPermissions, RequestLocationPermission);
               }

               return true;

            default:
               return base.OnOptionsItemSelected(item);
         }
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
      {
         switch (requestCode)
         {
            case RequestLocationPermission:
               if (HasLocationPermission)
               {
                  FindImage();
               }

               break;

            default:
               base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
               return;
         }
      }

      private void FindImage()
      {
         var request = LocationRequest.Create();
         request.SetPriority(LocationRequest.PriorityHighAccuracy);
         request.SetNumUpdates(1);
         request.SetInterval(0);
         ILocationListener locationListener = new LocationListenerImpl(this);
         LocationServices.FusedLocationApi.RequestLocationUpdates(_client, request, locationListener);
      }

      private void UpdateUi()
      {
         if (_map == null || _mapImage == null)
         {
            return;
         }

         var itemPoint = new LatLng(_mapItem.Lat, _mapItem.Lon);
         var myPoint = new LatLng(_currentLocation.Latitude, _currentLocation.Longitude);

         var itemBitmap = BitmapDescriptorFactory.FromBitmap(_mapImage);
         var itemMarker = new MarkerOptions()
            .SetPosition(itemPoint)
            .SetIcon(itemBitmap);
         var myMarker = new MarkerOptions()
            .SetPosition(myPoint);
         _map.Clear();
         _map.AddMarker(itemMarker);
         _map.AddMarker(myMarker);

         var bounds = new LatLngBounds.Builder()
            .Include(itemPoint)
            .Include(myPoint)
            .Build();

         var margin = Resources.GetDimensionPixelSize(ResDimensions.map_inset_margin);
         var update = CameraUpdateFactory.NewLatLngBounds(bounds, margin);
         _map.AnimateCamera(update);
      }
   }
}