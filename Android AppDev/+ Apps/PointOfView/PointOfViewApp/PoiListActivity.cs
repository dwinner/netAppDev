using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using PointOfViewApp.Adapters;
using PointOfViewApp.Poco;
using PointOfViewApp.Services;
using static PointOfViewApp.Utils.ConnectionUtils;
using R = PointOfViewApp.Resource;

namespace PointOfViewApp
{
   [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true /*,Icon = "@drawable/icon"*/)]
   public class PoiListActivity : AppCompatActivity
   {
      private PoiListViewAdapter _poiListAdapter;
      private List<PointOfInterest> _poiListData;
      private ListView _poiListView;
      private ProgressBar _poiProgressBar;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(R.Layout.PoiList);

         _poiListView = FindViewById<ListView>(R.Id.poiListView);
         _poiListView.ItemClick += (sender, args) =>
         {
            // TODO: Fetching the object at user clicked position
            var poi = _poiListData[(int) args.Id];
            Console.WriteLine("POI Clicked: Name is {0}", poi.Name);
         };

         _poiProgressBar = FindViewById<ProgressBar>(R.Id.progressBar);
         DownloadPoiListAsync();
      }

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         MenuInflater.Inflate(R.Menu.poi_listview_menu, menu);
         return base.OnCreateOptionsMenu(menu);
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         switch (item.ItemId)
         {
            case R.Id.actionNew:
               // TODO: Create new poi
               return true;

            case R.Id.actionRefresh:
               DownloadPoiListAsync();
               return true;

            default:
               return base.OnOptionsItemSelected(item);
         }
      }

      // ReSharper disable once AvoidAsyncVoid
      private async void DownloadPoiListAsync()
      {
         try
         {
            var service = new PoiService();
            if (!IsConnected(this))
            {
               var toast = Toast.MakeText(this,
                  "Not connected to internet. Please check your device network settings.",/* TODO: Move to resources */
                  ToastLength.Short);
               toast.Show();
            }
            else
            {
               _poiProgressBar.Visibility = ViewStates.Visible;
               _poiListData = await service.GetPoiListAsync().ConfigureAwait(true);
               _poiListAdapter = new PoiListViewAdapter(this, _poiListData);
               _poiListView.Adapter = _poiListAdapter;
            }
         }
         finally
         {
            _poiProgressBar.Visibility = ViewStates.Gone;
         }
      }
   }
}