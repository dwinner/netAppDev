using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using AppIds = AppDevUnited.AddressBook.App.Resource.Id;
using AppLayouts = AppDevUnited.AddressBook.App.Resource.Layout;

namespace AppDevUnited.AddressBook.App
{
   [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(AppLayouts.activity_main);

         var toolbar = FindViewById<Toolbar>(AppIds.toolbar);
         SetSupportActionBar(toolbar);
      }
   }
}