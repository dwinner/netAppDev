using LifecycleSample.Common;
using LifecycleSample.DataModel;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LifecycleSample
{
   public sealed partial class App
   {
      public App()
      {
         InitializeComponent();
         Suspending += OnSuspending;
      }

      protected override async void OnLaunched(LaunchActivatedEventArgs args)
      {
         // Не повторять инициализацию приложения, когда оно уже выполняется,
         // а просто удостовериться в том, что окно активно
         if (args.PreviousExecutionState == ApplicationExecutionState.Running)
         {
            Window.Current.Activate();
            return;
         }

         SuspensionManager.KnownTypes.Add(typeof(Page2Data));

         var rootFrame = new Frame();
         SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

         if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
         {
            await SuspensionManager.RestoreAsync();
         }

         // Перейти на первую страницу
         if (rootFrame.Content == null && !rootFrame.Navigate(typeof(MainPage)))
         {
            throw new Exception("Failed to create initial page");
         }

         Window.Current.Content = rootFrame;
         Window.Current.Activate();
      }

      private static async void OnSuspending(object sender, SuspendingEventArgs e)
      {
         var deferral = e.SuspendingOperation.GetDeferral();
         await SuspensionManager.SaveAsync();
         deferral.Complete();
      }
   }
}