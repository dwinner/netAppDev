﻿using System;
using AuthDemoXForms.ViewModels;
using Foundation;
using UIKit;
using Xamarin.Auth.Presenters.XamarinIOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace AuthDemoXForms.iOS
{
   // The UIApplicationDelegate for the application. This class is responsible for launching the 
   // User Interface of the application, as well as listening (and optionally responding) to 
   // application events from iOS.
   [Register("AppDelegate")]
   public class AppDelegate : FormsApplicationDelegate
   {
      //
      // This method is invoked when the application has loaded and is ready to run. In this 
      // method you should instantiate the window, load the UI into it and then make the window
      // visible.
      //
      // You have 17 seconds to return from this method, or iOS will terminate your application.
      //
      public override bool FinishedLaunching(UIApplication app, NSDictionary options)
      {
         Forms.SetFlags("CollectionView_Experimental");
         Forms.Init();
         AuthenticationConfiguration.Init();
         LoadApplication(new App());

         return base.FinishedLaunching(app, options);
      }

      public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
      {
         // Convert NSUrl to Uri
         var uri = new Uri(url.AbsoluteString);

         // Load redirectUrl page
         AuthenticationState.Authenticator.OnPageLoading(uri);

         return true;
      }
   }
}