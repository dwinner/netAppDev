using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace AppDevUnited.FlagQuizApp
{
   [Activity(Label = "FlagQuizApp", MainLauncher = true)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Main);
      }
   }
}

