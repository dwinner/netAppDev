using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SettingsDemo
{
   sealed partial class App
   {
      public App()
      {
         InitializeComponent();
         Suspending += OnSuspending;
      }

      protected override void OnLaunched(LaunchActivatedEventArgs args)
      {
         if (args.PreviousExecutionState == ApplicationExecutionState.Running)
         {
            Window.Current.Activate();
            return;
         }

         if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
         {
         }

         var rootFrame = new Frame();
         if (!rootFrame.Navigate(typeof(MainPage)))
         {
            throw new Exception("Failed to create initial page");
         }

         Window.Current.Content = rootFrame;
         Window.Current.Activate();
      }

      private void OnSuspending(object sender, SuspendingEventArgs e)
      {
         var deferral = e.SuspendingOperation.GetDeferral();
         deferral.Complete();
      }
   }
}