﻿using Xamarin.Forms;

namespace ContactBookApp
{
   public partial class App
   {
      public App()
      {
         InitializeComponent();

         MainPage = new NavigationPage(new ContactListPage());
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