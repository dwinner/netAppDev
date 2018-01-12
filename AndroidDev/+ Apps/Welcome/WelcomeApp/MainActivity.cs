using Android.App;
using Android.OS;
using AppDevUnited.WelcomeApp;

namespace WelcomeApp
{
   [Activity(Label = "WelcomeApp", MainLauncher = true)]
   public class MainActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);         
         SetContentView(Resource.Layout.Main);
      }
   }
}