﻿using Android.App;
using Android.OS;
using connectivity.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace connectivity.Droid
{
   [Activity(Label = "connectivity", MainLauncher = true, Icon = "@mipmap/icon")]
   public partial class MainActivity : Activity
   {
      private MainViewModel ViewModel => App.Locator.Main;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Website);

         CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

         RegisterWebview();

         NoConnection();

         // starts as false
         ViewModel.IsOnline = CrossConnectivity.Current.IsConnected;

         ViewModel.WebsiteCommand.Execute(true);
      }

      private void NoConnection() =>
         WebView.LoadData("<html><body><h1>Not connected</h1></body></html>", "text/html", "utf8");

      private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e) =>
         ViewModel.IsOnline = e.IsConnected;

      private void RegisterWebview() => this.SetBinding(() => ViewModel.IsOnline, BindingMode.TwoWay)
         .WhenSourceChanges(SourceChanges);

      private void SourceChanges()
      {
         if (ViewModel.IsOnline)
         {
            WebView.LoadUrl("http://www.liverpoolfc.net");
         }
         else
         {
            NoConnection();
         }
      }
   }
}