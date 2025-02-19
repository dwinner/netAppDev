﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace InstagramApp.Droid
{
   [Activity(Label = "InstagramApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : FormsAppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(savedInstanceState);

         Platform.Init(this, savedInstanceState);
         Forms.Init(this, savedInstanceState);
         ImageCircleRenderer.Init();
         LoadApplication(new App());
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
         [GeneratedEnum] Permission[] grantResults)
      {
         Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }
   }
}