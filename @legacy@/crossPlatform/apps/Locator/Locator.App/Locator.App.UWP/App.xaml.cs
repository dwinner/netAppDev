﻿using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Xamarin;
using Xamarin.Forms;
using Application = Windows.UI.Xaml.Application;
using Frame = Windows.UI.Xaml.Controls.Frame;
using NavigationEventArgs = Windows.UI.Xaml.Navigation.NavigationEventArgs;
// ReSharper disable WarningHighlighting
// ReSharper disable UnthrowableException

namespace Locator.App.UWP
{
   /// <summary>
   ///    Provides application-specific behavior to supplement the default Application class.
   /// </summary>
   public sealed partial class App
   {
      private const string AuthToken = "AIzaSyAyVDDQhBeL1ofTKlYf7W-4-W4v8y4Ufj0";
      private TransitionCollection _transitions;

      /// <summary>
      ///    Initializes the singleton application object.  This is the first line of authored code
      ///    executed, and as such is the logical equivalent of main() or WinMain().
      /// </summary>
      public App()
      {
         InitializeComponent();
         Suspending += OnSuspending;
      }

      /// <summary>
      ///    Invoked when the application is launched normally by the end user.  Other entry points
      ///    will be used when the application is launched to open a specific file, to display
      ///    search results, and so forth.
      /// </summary>
      /// <param name="e">Details about the launch request and process.</param>
      protected override void OnLaunched(LaunchActivatedEventArgs e)
      {
#if DEBUG
         if (Debugger.IsAttached)
         {
            DebugSettings.EnableFrameRateCounter = true;
         }
#endif

         // Do not repeat app initialization when the Window already has content,
         // just ensure that the window is active
         if (!(Window.Current.Content is Frame rootFrame))
         {
            // Create a Frame to act as the navigation context and navigate to the first page
            // TODO: change this value to a cache size that is appropriate for your application
            rootFrame = new Frame {CacheSize = 1};
            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
               // TODO: Load state from previously suspended application
            }

            FormsMaps.Init(AuthToken);
            Forms.Init(e);

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
         }

         if (rootFrame.Content == null)
         {
            // Removes the turnstile navigation for startup.
            if (rootFrame.ContentTransitions != null)
            {
               _transitions = new TransitionCollection();
               foreach (var c in rootFrame.ContentTransitions)
               {
                  _transitions.Add(c);
               }
            }

            rootFrame.ContentTransitions = null;
            rootFrame.Navigated += RootFrame_FirstNavigated;

            // When the navigation stack isn't restored navigate to the first page,
            // configuring the new page by passing required information as a navigation
            // parameter
            if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
            {
               throw new Exception("Failed to create initial page");
            }
         }

         // Ensure the current window is active
         Window.Current.Activate();
      }

      /// <summary>
      ///    Restores the content transitions after the app has launched.
      /// </summary>
      /// <param name="sender">The object where the handler is attached.</param>
      /// <param name="e">Details about the navigation event.</param>
      private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
      {
         var rootFrame = sender as Frame;
         rootFrame.ContentTransitions = _transitions ?? new TransitionCollection {new NavigationThemeTransition()};
         rootFrame.Navigated -= RootFrame_FirstNavigated;
      }

      /// <summary>
      ///    Invoked when application execution is being suspended.  Application state is saved
      ///    without knowing whether the application will be terminated or resumed with the contents
      ///    of memory still intact.
      /// </summary>
      /// <param name="sender">The source of the suspend request.</param>
      /// <param name="e">Details about the suspend request.</param>
      private void OnSuspending(object sender, SuspendingEventArgs e)
      {
         var deferral = e.SuspendingOperation.GetDeferral();

         // TODO: Save application state and stop any background activity
         deferral.Complete();
      }
   }
}