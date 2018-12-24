using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace WeatherViewer.App
{
   [Activity(Label = "@string/app_name",
      Theme = "@style/AppTheme.NoActionBar",
      MainLauncher = true,
      ScreenOrientation = ScreenOrientation.Portrait)]
   public class MainActivity : AppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.ActivityMain);

         var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
         SetSupportActionBar(toolbar);

         var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
         fab.Click += FabOnClick;
      }

      private void FabOnClick(object sender, EventArgs eventArgs)
      {
         var view = (View) sender;
         Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            .SetAction("Action", (View.IOnClickListener) null).Show();
      }
   }
}