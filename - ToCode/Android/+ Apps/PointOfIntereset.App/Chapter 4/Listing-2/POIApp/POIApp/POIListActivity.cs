using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace POIApp
{
	[Activity (Label = "POIApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class POIListActivity : Activity
	{
		// ListView instance 
		private ListView poiListView;
		// ProgressBar instance
		private ProgressBar progressBar;

		private List<PointOfInterest> poiListData;
		private POIListViewAdapter poiListAdapter;

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

			DownloadPoisListAsync ();
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
			}
		}


		/**
		 * Delegate that handles POI ListView row click event.
		 **/
		protected void POIClicked(object sender, ListView.ItemClickEventArgs e)
		{
			// Fetching the object at user clicked position
			PointOfInterest poi = poiListData[(int)e.Id];

			// For now showing log in console
			// We will complete this in Chapter-5 and navigate user to details activity
			Console.Out.WriteLine("POI Clicked: Name is {0}", poi.Name);
		}
	}
}