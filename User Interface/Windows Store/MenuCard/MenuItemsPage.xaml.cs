using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Wrox.Metro.Contracts;
using Wrox.Metro.DataModel;
using Wrox.Metro.Storage;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace Wrox.Metro
{
   /// <summary>
   /// A page that displays a collection of item previews.  In the Split Application this page
   /// is used to display and select one of the available groups.
   /// </summary>
   public sealed partial class MenuItemsPage : Wrox.Metro.Common.LayoutAwarePage
   {
      private MenuCard card;
      private SharingContract sharing;

      public MenuItemsPage()
      {
         this.InitializeComponent();
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
         // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]

         card = navigationParameter as MenuCard;
         if (card != null)
         {
            // this.DefaultViewModel["Card"] = card; // some bug...
            this.DefaultViewModel["Items"] = card.MenuItems;

            sharing = new SharingContract();
            sharing.ShareMenuCard(card);
         }
      }

      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         base.OnNavigatedTo(e);
         if (card != null)
         {
            sharing = new SharingContract();
            sharing.ShareMenuCard(card);
         }
      }

      protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
      {
         if (sharing != null) sharing.Dispose();

         base.OnNavigatingFrom(e);
      }

      private async void OnSave(object sender, RoutedEventArgs e)
      {
         var picker = new FileSavePicker();
         picker.DefaultFileExtension = ".xml";
         picker.SuggestedFileName = string.Format("MenuCard {0}.xml", card.Title);
         picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
         picker.FileTypeChoices.Add("XML File", new List<string>() { ".xml" });

         StorageFile file = await picker.PickSaveFileAsync();
         MenuCardStorage storage = new MenuCardStorage();
         await storage.WriteMenuCardToFileAsync(card, file);
      }

      private void OnOpen(object sender, RoutedEventArgs e)
      {

      }

      private void OnAddMenuItem(object sender, RoutedEventArgs e)
      {
         card.MenuItems.Add(new MenuItem());
      }

      private void OnDeleteMenuItem(object sender, RoutedEventArgs e)
      {

      }
   }
}
