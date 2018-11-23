using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using PointOfViewApp.Orm;
using ResId = PointOfViewApp.Resource.Id;

// TODO: Add Google API to enable location in emulator
namespace PointOfViewApp
{
   [Activity(
      Label = "@string/app_name",
      Theme = "@style/AppTheme",
      MainLauncher = true,
      Icon = "@drawable/ic_launcher")]
   public class PoiListActivity : AppCompatActivity
   {
      internal static bool IsDualMode;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.PoiList);

         var detailLayout = FindViewById<FrameLayout>(ResId.poiDetailLayout);
         IsDualMode = detailLayout?.Visibility == ViewStates.Visible;

         SqLiteDbManager.Instance.CreateTable();
      }

      protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
      {
         base.OnActivityResult(requestCode, resultCode, data);
      }
   }
}