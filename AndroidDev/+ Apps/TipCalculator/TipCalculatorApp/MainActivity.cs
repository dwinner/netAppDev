using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

namespace AppDevUnited.TipCalculatorApp
{
   [Activity(
      Label = "TipCalculatorApp",
      Name = "AppDevUnited.TipCalculatorApp.MainActivity",
      MainLauncher = true,      
      WindowSoftInputMode = SoftInput.StateAlwaysVisible,
      ScreenOrientation = ScreenOrientation.Portrait)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);
      }
   }
}