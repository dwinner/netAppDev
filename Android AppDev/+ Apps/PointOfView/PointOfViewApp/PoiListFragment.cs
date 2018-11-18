using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PointOfViewApp.Adapters;
using PointOfViewApp.Orm;
using PointOfViewApp.Poco;
using PointOfViewApp.Services;
using PointOfViewApp.Utils;
using ListFragmentV4 = Android.Support.V4.App.ListFragment;
using LayoutRes = PointOfViewApp.Resource.Layout;
using IdRes = PointOfViewApp.Resource.Id;
using MenuRes = PointOfViewApp.Resource.Menu;
using JavaObj = Java.Lang.Object;

// ReSharper disable AvoidAsyncVoid

namespace PointOfViewApp
{
   public class PoiListFragment : ListFragmentV4
   {
      private const string PoiListScrollPositionBundleKey = "poi_list_scroll_position";

      private static readonly Dictionary<LocationRequestCode, (string permission, string alertMessage)>
         _LocationPermissionMap = new Dictionary<LocationRequestCode, (string permission, string alertMessage)>
         {
            {
               LocationRequestCode.CoarseLocation,
               (Manifest.Permission.AccessCoarseLocation, "Need coarse location permission")
            },
            {
               LocationRequestCode.FineLocation,
               (Manifest.Permission.AccessFineLocation, "Need fine location permission")
            },
            {
               LocationRequestCode.NetworkState,
               (Manifest.Permission.AccessNetworkState, "Need network state permission")
            }
         };

      private Activity _activity;
      private ILocationListener _locationListener;
      private LocationManager _locationManager;
      private PoiListViewAdapter _poiListAdapter;
      private List<PointOfInterest> _poiListData;
      private ProgressBar _poiProgressBar;
      private int _scrollPosition;

      public override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         if (savedInstanceState != null) _scrollPosition = savedInstanceState.GetInt(PoiListScrollPositionBundleKey);
      }

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         var view = inflater.Inflate(LayoutRes.PoiListFragment, container, false);
         _poiProgressBar = view.FindViewById<ProgressBar>(IdRes.progressBar);
         HasOptionsMenu = true;
         _locationManager = (LocationManager) Activity.GetSystemService(Context.LocationService);
         _locationListener = new LocationListenerImpl(this);

         return view;
      }

      public override void OnAttach(Context context)
      {
         base.OnAttach(context);
         _activity = (Activity) context;
      }

      public override void OnResume()
      {
         base.OnResume();
         DownloadPoiListAsync();

         // Request Geo location permissions
         foreach (var (requestCode, (permission, alertMessage)) in _LocationPermissionMap)
            RequestPermission(permission, alertMessage, requestCode);

         SetupLocationProvider();

         void RequestPermission(string permission, string alertMessage, LocationRequestCode locationRequestCode)
         {
            if (Context.CheckSelfPermission(permission) == Permission.Granted)
               return;

            if (ShouldShowRequestPermissionRationale(permission))
               new AlertDialog.Builder(Activity)
                  .SetMessage(alertMessage)
                  .SetPositiveButton(Android.Resource.String.Ok,
                     (sender, args) => RequestPermissions(new[] {permission}, (int) locationRequestCode))
                  .Create()
                  .Show();
            else
               RequestPermissions(new[] {permission}, (int) locationRequestCode);
         }
      }

      private void SetupLocationProvider()
      {
         var criteria = new Criteria
         {
            Accuracy = Accuracy.NoRequirement,
            PowerRequirement = Power.NoRequirement
         };
         var provider = _locationManager.GetBestProvider(criteria, true);
         _locationManager.RequestLocationUpdates(provider, 2000, 100, _locationListener);
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
      {
         switch (requestCode)
         {
            case (int) LocationRequestCode.CoarseLocation:
            case (int) LocationRequestCode.NetworkState:
            case (int) LocationRequestCode.FineLocation:
            {
               if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                  SetupLocationProvider();
               break;
            }
         }
      }

      public override void OnPause()
      {
         base.OnPause();
         _locationManager.RemoveUpdates(_locationListener);
      }

      public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
      {
         inflater.Inflate(MenuRes.poi_listview_menu, menu);
         base.OnCreateOptionsMenu(menu, inflater);
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         switch (item.ItemId)
         {
            case IdRes.actionNew:
               if (PoiListActivity.IsDualMode)
               {
                  var detailFragment = new PoiDetailFragment();
                  FragmentManager.BeginTransaction().Replace(IdRes.poiDetailLayout, detailFragment).Commit();
               }
               else
               {
                  var intent = new Intent(_activity, typeof(PoiDetailActivity));
                  StartActivity(intent);
               }

               return true;

            case IdRes.actionRefresh:
               DownloadPoiListAsync();
               return true;

            default:
               return base.OnOptionsItemSelected(item);
         }
      }

      public override void OnListItemClick(ListView l, View v, int position, long id)
      {
         var interest = _poiListData[position];
         if (PoiListActivity.IsDualMode)
         {
            var detailFragment = new PoiDetailFragment {Arguments = new Bundle()};
            detailFragment.Arguments.PutString(IntentKeys.PoiDetailKey, JsonConvert.SerializeObject(interest));
            FragmentManager.BeginTransaction().Replace(IdRes.poiDetailLayout, detailFragment).Commit();
         }
         else
         {
            var poiDetailIntent = new Intent(_activity, typeof(PoiDetailActivity));
            poiDetailIntent.PutExtra(IntentKeys.PoiDetailKey, JsonConvert.SerializeObject(interest));
            StartActivity(poiDetailIntent);
         }
      }

      public override void OnSaveInstanceState(Bundle outState)
      {
         base.OnSaveInstanceState(outState);
         var currentPosition = ListView.FirstVisiblePosition;
         outState.PutInt(PoiListScrollPositionBundleKey, currentPosition);
      }

      private async void DownloadPoiListAsync()
      {
         try
         {
            var service = new PoiService();
            if (!ConnectionUtils.IsConnected(_activity))
            {
               var toast = Toast.MakeText(_activity,
                  "Not connected to internet. Please check your device network settings.",
                  ToastLength.Short);
               toast.Show();
               _poiListData = SqLiteDbManager.Instance.Select();
            }
            else
            {
               _poiProgressBar.Visibility = ViewStates.Visible;
               _poiListData = await service.GetPoiListAsync().ConfigureAwait(true);

               SqLiteDbManager.Instance.Delete(); // Clear cached data
               SqLiteDbManager.Instance.Save(_poiListData); // Save updated interests
               _poiListAdapter = new PoiListViewAdapter(_activity, _poiListData);
               ListAdapter = _poiListAdapter;
               ListView.Post(() =>
                  ListView.SetSelection(
                     _scrollPosition)); // Restore the selection on the scroll position               
            }
         }
         finally
         {
            _poiProgressBar.Visibility = ViewStates.Gone;
         }
      }

      private enum LocationRequestCode : short
      {
         CoarseLocation = 1,
         FineLocation = 2,
         NetworkState = 3
      }

      private sealed class LocationListenerImpl : JavaObj, ILocationListener
      {
         private readonly PoiListFragment _fragment;

         public LocationListenerImpl(PoiListFragment fragment) => _fragment = fragment;

         public void OnLocationChanged(Location location)
         {
            if (_fragment.ListAdapter is PoiListViewAdapter adapter)
            {
               adapter.CurrentLocation = location;
               _fragment.ListView.InvalidateViews();
            }
         }

         public void OnProviderDisabled(string provider)
         {
         }

         public void OnProviderEnabled(string provider)
         {
         }

         public void OnStatusChanged(string provider, Availability status, Bundle extras)
         {
         }
      }
   }
}