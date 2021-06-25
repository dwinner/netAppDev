using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Wrox.Metro.Common;
using Wrox.Metro.DataModel;
using Wrox.Metro.Notification;
using Wrox.Metro.Storage;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace Wrox.Metro
{
   /// <summary>
   /// Provides application-specific behavior to supplement the default Application class.
   /// </summary>
   sealed partial class App : Application
   {
      /// <summary>
      /// Initializes the singleton application object.  This is the first line of authored code
      /// executed, and as such is the logical equivalent of main() or WinMain().
      /// </summary>
      public App()
      {
         this.InitializeComponent();
         this.Suspending += OnSuspending;
      }

      /// <summary>
      /// Invoked when the application is launched normally by the end user.  Other entry points
      /// will be used when the application is launched to open a specific file, to display
      /// search results, and so forth.
      /// </summary>
      /// <param name="args">Details about the launch request and process.</param>
      protected override async void OnLaunched(LaunchActivatedEventArgs args)
      {
         // Do not repeat app initialization when already running, just ensure that
         // the window is active
         if (args.PreviousExecutionState == ApplicationExecutionState.Running)
         {
            Window.Current.Activate();
            return;
         }

         if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
         {
            //TODO: Load state from previously suspended application
         }

         await InitSampleDataAsync();

         TileManager.UpdateTile();


         // Create a Frame to act navigation context and navigate to the first page
         var rootFrame = new Frame();
         if (!rootFrame.Navigate(typeof(MainPage)))
         {
            throw new Exception("Failed to create initial page");
         }

         // Place the frame in the current Window and ensure that it is active
         Window.Current.Content = rootFrame;
         Window.Current.Activate();
      }

      private static async Task InitSampleDataAsync()
      {
         var storage = new MenuCardStorage();
         var imageStorage = new MenuCardImageStorage();
         if (await storage.IsRoamingFolderEmpty())
         {
            List<MenuCard> menuCards = MenuCardFactory.GetSampleMenuCards().ToList();

            foreach (var card in menuCards)
            {
               RandomAccessStreamReference streamRef = RandomAccessStreamReference.CreateFromUri(new Uri(card.ImagePath));
               using (IRandomAccessStreamWithContentType stream = await streamRef.OpenReadAsync())
               {
                  card.ImagePath = string.Format("{0}.png", Guid.NewGuid());
                  await imageStorage.WriteImageAsync(stream, card.ImagePath);
               }
            }

            await storage.WriteMenuCardsAsync(menuCards);
         }
      }

      /// <summary>
      /// Invoked when application execution is being suspended.  Application state is saved
      /// without knowing whether the application will be terminated or resumed with the contents
      /// of memory still intact.
      /// </summary>
      /// <param name="sender">The source of the suspend request.</param>
      /// <param name="e">Details about the suspend request.</param>
      private async void OnSuspending(object sender, SuspendingEventArgs e)
      {
         var deferral = e.SuspendingOperation.GetDeferral();
         //TODO: Save application state and stop any background activity
         await SuspensionManager.SaveAsync();
         deferral.Complete();
      }

      /// <summary>
      /// Invoked when the application is activated as the target of a sharing operation.
      /// </summary>
      /// <param name="args">Details about the activation request.</param>
      protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
      {
         //var rootFrame = new Frame();
         //rootFrame.Navigate(typeof(ShareTargetPage1), args.ShareOperation);
         //Window.Current.Content = rootFrame;
         //Window.Current.Activate();
         ShareTargetPage1 p1 = new ShareTargetPage1();
         p1.Activate(args);
      }
   }
}
