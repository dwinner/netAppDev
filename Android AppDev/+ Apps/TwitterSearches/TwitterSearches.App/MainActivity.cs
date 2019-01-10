using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace AppDevUnited.TwitterSearches.App
{
   [Activity(
      Label = "@string/app_name",
      Theme = "@style/AppTheme.NoActionBar",
      MainLauncher = true,
      WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.activity_main);

         var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
         SetSupportActionBar(toolbar);

         var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
         fab.Click += OnFabClick;
      }

      private void OnFabClick(object sender, EventArgs eventArgs)
      {
      }
   }
}