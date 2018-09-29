using Android.App;
using Android.OS;
using Android.Views;

namespace POIApp
{
	[Activity (Label = "POI List", MainLauncher = true)]
	public class POIListActivity : Activity
	{
		public static bool isDualMode = false; 

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.POIList);

			var detailsLayout = FindViewById (Resource.Id.poiDetailLayout);
			if (detailsLayout != null && detailsLayout.Visibility == ViewStates.Visible) {
				isDualMode = true;
			}else{
				isDualMode = false;
			}

			DBManager.Instance.CreateTable ();
		}
	}
}
