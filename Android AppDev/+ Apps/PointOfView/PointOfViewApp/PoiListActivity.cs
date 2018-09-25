using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using PointOfViewApp.Adapters;
using PointOfViewApp.Poco;
using static PointOfViewApp.Resource;

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
         SetContentView(Layout.PoiList);

         _poiListView = FindViewById<ListView>(Id.poiListView);
         _poiProgressBar = FindViewById<ProgressBar>(Id.progressBar);
         DownloadPoiListAsync();
      }

      public async void DownloadPoiListAsync()
      {
         try
         {
            _poiProgressBar.Visibility = ViewStates.Visible;
            _poiListData = GetPoisListTestData();
            _poiListAdapter = new PoiListViewAdapter(this, _poiListData);
            _poiListView.Adapter = _poiListAdapter;
         }
         finally
         {
            _poiProgressBar.Visibility = ViewStates.Gone;
         }
      }

      private List<PointOfInterest> GetPoisListTestData()
      {
         var listData = new List<PointOfInterest>();
         for (var i = 0; i < 20; i++)
         {
            var poiItem = new PointOfInterest {Id = i, Name = $"Name {i}", Address = $"Address {i}"};
            listData.Add(poiItem);
         }

         return listData;
      }
   }
}