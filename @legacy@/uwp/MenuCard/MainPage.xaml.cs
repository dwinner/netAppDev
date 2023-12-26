using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Wrox.Metro.Common;
using Wrox.Metro.DataModel;
using Wrox.Metro.Storage;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Wrox.Metro
{
   /// <summary>
   /// A basic page that provides characteristics common to most applications.
   /// </summary>
   public sealed partial class MainPage : LayoutAwarePage
   {

      public MainPage()
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
      protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
      {
         var storage = new MenuCardStorage();
         MenuCardFactory.Instance.InitMenuCards(new ObservableCollection<MenuCard>(await storage.ReadMenuCardsAsync()));
         this.DefaultViewModel["Items"] = MenuCardFactory.Instance.Cards;
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

      private void OnAddMenuCard(object sender, RoutedEventArgs e)
      {
         this.Frame.Navigate(typeof(AddMenuCardPage));
      }

      private async void OnDeleteMenuCard(object sender, RoutedEventArgs e)
      {
         var selectedMenuCard = itemListView.SelectedItem as MenuCard;
         if (selectedMenuCard != null)
         {

            var dlg = new MessageDialog(string.Format("Delete the menu card {0}?", selectedMenuCard.Title));
            dlg.Commands.Add(new UICommand("Delete", new UICommandInvokedHandler(DeleteMenuCard)));
            dlg.Commands.Add(new UICommand("Cancel"));
            await dlg.ShowAsync();
         }
      }

      private void DeleteMenuCard(IUICommand command)
      {
         var menuCards = this.DefaultViewModel["Items"] as ObservableCollection<MenuCard>;
         if (menuCards != null)
         {
            menuCards.Remove(itemListView.SelectedItem as MenuCard);
            // TODO: remove it from storage
         }
      }

      private void OnMenuCardClick(object sender, ItemClickEventArgs e)
      {
         Frame.Navigate(typeof(MenuItemsPage), e.ClickedItem);
      }
   }
}
