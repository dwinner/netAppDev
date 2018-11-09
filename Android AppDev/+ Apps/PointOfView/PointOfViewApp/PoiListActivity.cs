using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PointOfViewApp.Adapters;
using PointOfViewApp.Poco;
using PointOfViewApp.Services;
using PointOfViewApp.Utils;
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
         _poiListView.ItemClick += OnPoiListItemClicked;
         _poiProgressBar = FindViewById<ProgressBar>(R.Id.progressBar);

         DownloadPoiListAsync();
      }

      private void OnPoiListItemClicked(object sender, AdapterView.ItemClickEventArgs e)
      {
         var selectedPoi = _poiListData[(int) e.Id];
         var poiDetailIntent = new Intent(this, typeof(PoiDetailActivity));
         var poiJson = JsonConvert.SerializeObject(selectedPoi);
         poiDetailIntent.PutExtra(IntentKeys.PoiDetailKey, poiJson);
         StartActivity(poiDetailIntent);
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
               StartActivity(typeof(PoiDetailActivity));
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
                  "Not connected to internet. Please check your device network settings.", /* TODO: Move to resources */
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