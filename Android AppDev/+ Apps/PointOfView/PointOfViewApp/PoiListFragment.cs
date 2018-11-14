using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PointOfViewApp.Adapters;
using PointOfViewApp.Poco;
using PointOfViewApp.Services;
using PointOfViewApp.Utils;
using ListFragmentV4 = Android.Support.V4.App.ListFragment;
using LayoutRes = PointOfViewApp.Resource.Layout;
using IdRes = PointOfViewApp.Resource.Id;
using MenuRes = PointOfViewApp.Resource.Menu;
// ReSharper disable AvoidAsyncVoid

namespace PointOfViewApp
{
   public class PoiListFragment : ListFragmentV4
   {
      //private const string PoiListScrollPositionBundleKey = "poi_list_scroll_position";
      //private int _scrollPosition;

      private Activity _activity;
      private PoiListViewAdapter _poiListAdapter;
      private List<PointOfInterest> _poiListData;
      private ProgressBar _poiProgressBar;

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         var view = inflater.Inflate(LayoutRes.PoiListFragment, container, false);
         _poiProgressBar = view.FindViewById<ProgressBar>(IdRes.progressBar);
         HasOptionsMenu = true;

         return view;
      }

      public override void OnAttach(Context context)
      {
         base.OnAttach(context);
         _activity = (Activity) context;
      }

      public override void OnResume()
      {
         DownloadPoiListAsync();
         base.OnResume();
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
               var intent = new Intent(_activity, typeof(PoiDetailActivity));
               StartActivity(intent);
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
         var selectedPoi = _poiListData[position];
         var poiDetailIntent = new Intent(_activity, typeof(PoiDetailActivity));
         var poiJson = JsonConvert.SerializeObject(selectedPoi);
         poiDetailIntent.PutExtra(IntentKeys.PoiDetailKey, poiJson);
         StartActivity(poiDetailIntent);
      }

      /* BUG: The scrolling behavior is available from scratch?!
      public override void OnSaveInstanceState(Bundle outState)
      {
         base.OnSaveInstanceState(outState);
         var currentPosition = ListView.FirstVisiblePosition;
         outState.PutInt(PoiListScrollPositionBundleKey, currentPosition);
      }      

      public override void OnViewStateRestored(Bundle savedInstanceState)
      {
         base.OnViewStateRestored(savedInstanceState);
         _scrollPosition = savedInstanceState.GetInt(PoiListScrollPositionBundleKey);
      }
      */

      private async void DownloadPoiListAsync()
      {
         try
         {
            var service = new PoiService();
            if (!ConnectionUtils.IsConnected(_activity))
            {
               var toast = Toast.MakeText(_activity,
                  "Not connected to internet. Please check your device network settings.", /* TODO: Move to resources */
                  ToastLength.Short);
               toast.Show();
            }
            else
            {
               _poiProgressBar.Visibility = ViewStates.Visible;
               _poiListData = await service.GetPoiListAsync().ConfigureAwait(true);
               _poiListAdapter = new PoiListViewAdapter(_activity, _poiListData);
               ListAdapter = _poiListAdapter;

               /*
               _poiListView.Adapter = _poiListAdapter;               
               _poiListView.Post(() => _poiListView.SetSelection(_scrollPosition)); // Restore the selection on the scroll position
               */
            }
         }
         finally
         {
            _poiProgressBar.Visibility = ViewStates.Gone;
         }
      }
   }
}