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

			// Initializing progressbar 
			progressBar = FindViewById<ProgressBar> (Resource.Id.progressBar);

			DownloadPoisListAsync ();
		}

		public async void DownloadPoisListAsync(){
			// Displaying loading progressbar 
			progressBar.Visibility = ViewStates.Visible;

			//For testing only, here we will initiate the data download form server
			poiListData = GetPoisListTestData();
			progressBar.Visibility = ViewStates.Gone;

			poiListAdapter = new POIListViewAdapter (this, poiListData);
			poiListView.Adapter = poiListAdapter;
		}

		private List<PointOfInterest> GetPoisListTestData(){
			List<PointOfInterest> listData = new List<PointOfInterest> ();
			for(int i=0; i<20; i++){
				PointOfInterest poi = new PointOfInterest ();
				poi.Id = i;
				poi.Name = "Name " + i;
				poi.Address = "Address " + i;
				listData.Add (poi);
			}
			return listData;
		}
	}
}