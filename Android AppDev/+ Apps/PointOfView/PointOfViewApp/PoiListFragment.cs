using System.Collections.Generic;
using Android.App;
using Android.Content;
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

// ReSharper disable AvoidAsyncVoid

namespace PointOfViewApp
{
   public class PoiListFragment : ListFragmentV4
   {
      private const string PoiListScrollPositionBundleKey = "poi_list_scroll_position";

      private Activity _activity;
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
                  "Not connected to internet. Please check your device network settings.", /* TODO: Move to resources */
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
   }
}