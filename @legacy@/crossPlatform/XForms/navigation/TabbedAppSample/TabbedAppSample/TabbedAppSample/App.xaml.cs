﻿using TabbedAppSample.Services;
using TabbedAppSample.Views;
using Xamarin.Forms;

namespace TabbedAppSample
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();

         DependencyService.Register<MockDataStore>();
         MainPage = new MainPage();
      }

      protected override void OnStart()
      {
         // Handle when your app starts
      }

      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      protected override void OnResume()
      {
         // Handle when your app resumes
      }
   }
}