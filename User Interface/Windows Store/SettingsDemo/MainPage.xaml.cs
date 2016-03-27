using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace SettingsDemo
{
   /// <summary>
   /// A basic page that provides characteristics common to most applications.
   /// </summary>
   public sealed partial class MainPage : SettingsDemo.Common.LayoutAwarePage
   {
      private Rect windowBounds;
      private double settingsWidth = 350;
      Popup settingsPopup;

      public MainPage()
      {
         this.InitializeComponent();
         this.SizeChanged += MainPage_SizeChanged;
      }

      void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
      {
         windowBounds = Window.Current.Bounds;
      }

      /// <summary>
      /// Populates the page with content passed during navigation.  Any saved state is also
      /// provided when recreating a page from a prior session.
      /// </summary>
      /// <param name="navigationParameter">The parameter value passed to
      /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
      /// </param>
      /// <param name="pageState">A dictionary of state preserved by this page during an earlier
      /// session.  This will be null the first time a page is visited.</param>
      protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
      {
         SettingsPane.GetForCurrentView().CommandsRequested += MainPage_CommandsRequested;
      }



      void MainPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
      {
         SettingsCommand command1 = new SettingsCommand("command1", "Command 1", new UICommandInvokedHandler(Command1));
         args.Request.ApplicationCommands.Add(command1);
         SettingsCommand command2 = new SettingsCommand("command2", "Command 2", new UICommandInvokedHandler(Command2));
         args.Request.ApplicationCommands.Add(command2);
      }

      void Command1(IUICommand command)
      {
      }

      void Command2(IUICommand command)
      {
         Window.Current.Activated += Window_Activated;
         SampleSettingsPane myPane = new SampleSettingsPane();
         myPane.Width = this.settingsWidth;
         myPane.Height = this.windowBounds.Height;
         settingsPopup = new Popup();
         settingsPopup.Closed += SettingsPopup_Closed;
         settingsPopup.Child = myPane;
         settingsPopup.IsLightDismissEnabled = true;
         settingsPopup.SetValue(Canvas.LeftProperty, windowBounds.Width - settingsWidth);
         settingsPopup.SetValue(Canvas.TopProperty, 0);

         settingsPopup.IsOpen = true;

      }

      void SettingsPopup_Closed(object sender, object e)
      {
         Window.Current.Activated -= Window_Activated;
      }

      private void Window_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
      {
         if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
         {
            settingsPopup.IsOpen = false;
         }

      }


      /// <summary>
      /// Preserves state associated with this page in case the application is suspended or the
      /// page is discarded from the navigation cache.  Values must conform to the serialization
      /// requirements of <see cref="SuspensionManager.SessionState"/>.
      /// </summary>
      /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
      protected override void SaveState(Dictionary<String, Object> pageState)
      {
      }
   }
}
