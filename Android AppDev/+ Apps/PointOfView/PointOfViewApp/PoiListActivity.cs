using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace PointOfViewApp
{
   [Activity(
      Label = "@string/app_name",
      Theme = "@style/AppTheme",
      MainLauncher = true,
      Icon = "@drawable/ic_launcher")]
   public class PoiListActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.PoiList);
      }
   }
}