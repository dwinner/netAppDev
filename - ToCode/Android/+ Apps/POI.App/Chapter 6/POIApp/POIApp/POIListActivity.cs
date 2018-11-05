using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace POIApp
{
	[Activity (Label = "POIApp", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class POIListActivity : Activity
	{
		// ListView instance 
		private ListView poiListView;
		// ProgressBar instance
		private ProgressBar progressBar;

		private List<PointOfInterest> poiListData;
		private POIListViewAdapter poiListAdapter;

		int scrollPosition;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Setting layout 
			SetContentView (Resource.Layout.POIList);

			// Initializing listview 
			poiListView = FindViewById<ListView> (Resource.Id.poiListView);
			poiListView.ItemClick += POIClicked;

			// Initializing progressbar 
			progressBar = FindViewById<ProgressBar> (Resource.Id.progressBar);
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			DownloadPoisListAsync ();
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState); 
			int currentPosition = poiListView.FirstVisiblePosition;
			outState.PutInt ("scroll_position", currentPosition);
		}

		protected override void OnRestoreInstanceState (Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState (savedInstanceState);
			scrollPosition = savedInstanceState.GetInt ("scroll_position");
		}


		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			// Inflate menus to be shown on ActionBar
			MenuInflater.Inflate(Resource.Menu.POIListViewMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Resource.Id.actionNew:
				// place holder for creating new poi
				StartActivity (typeof(POIDetailActivity));
				return true;
			case Resource.Id.actionRefresh:
				DownloadPoisListAsync ();
				return true;
			default :
				return base.OnOptionsItemSelected(item);
			}
		}

		public async void DownloadPoisListAsync(){
			POIService service = new POIService ();
			if (!service.isConnected (this)) {
				Toast toast = Toast.MakeText (this, "Not conntected to internet. Please check your device network settings.", ToastLength.Short);
				toast.Show ();
			} else {
				progressBar.Visibility = ViewStates.Visible;
				poiListData = await service.GetPOIListAsync ();
				progressBar.Visibility = ViewStates.Gone;

				poiListAdapter = new POIListViewAdapter (this, poiListData);
				poiListView.Adapter = poiListAdapter;

				poiListView.Post(() => {
					poiListView.SetSelection(scrollPosition);
				});

			}
		}

		/**
		 * Delegate that handles POI ListView row click event.
		 **/
		protected void POIClicked(object sender, ListView.ItemClickEventArgs e)
		{
			PointOfInterest poi = poiListData[(int)e.Id];

			Intent poiDetailIntent = new Intent(this, typeof(POIDetailActivity));
			string poiJson = JsonConvert.SerializeObject(poi); 
			poiDetailIntent.PutExtra("poi", poiJson);
			StartActivity(poiDetailIntent);
		}

	}
}