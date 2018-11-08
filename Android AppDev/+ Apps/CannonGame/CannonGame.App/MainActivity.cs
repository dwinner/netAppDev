using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace CannonGame.App
{
   [Activity(
      Label = "@string/app_name",
      Theme = "@style/AppTheme.NoActionBar",
      MainLauncher = true,
      ScreenOrientation = ScreenOrientation.Landscape)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.ActivityMain);
      }
   }
}