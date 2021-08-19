using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wrox.ProCSharp.Documents.Properties;

namespace Wrox.ProCSharp.Documents
{
   public partial class MenuPlannerPage
   {
      private readonly ObservableCollection<MenuEntry> _menus = new ObservableCollection<MenuEntry>();

      public MenuPlannerPage()
      {
         InitializeComponent();

         DaySelection.BlackoutDates.AddDatesInPast();
         DataContext = _menus;
      }

      private void FillMenuList(DateTime startDay)
      {
         _menus.Clear();
         var config = Settings.Default.MenuConfigDays;
         if (config == null) // default fill
         {
            _menus.Add(new MenuEntry {Day = startDay, Price = 6.70m});
            _menus.Add(new MenuEntry {Day = startDay + TimeSpan.FromDays(1), Price = 6.70m});
            _menus.Add(new MenuEntry {Day = startDay + TimeSpan.FromDays(2), Price = 6.70m});
            _menus.Add(new MenuEntry {Day = startDay + TimeSpan.FromDays(3), Price = 6.70m});
            _menus.Add(new MenuEntry {Day = startDay + TimeSpan.FromDays(4), Price = 7.30m});
            _menus.Add(new MenuEntry {Day = startDay + TimeSpan.FromDays(5), Price = 6.70m});
         }
         else
         {
            foreach (var c in config.MenuConfig)
               _menus.Add(new MenuEntry
               {
                  Day = startDay + TimeSpan.FromDays((int) c.DayOfWeek - 1),
                  Price = c.Price
               });
         }
      }

      private void OnDateSelection(object sender, SelectionChangedEventArgs e)
      {
         var date = DaySelection.SelectedDate ?? DateTime.Today;
         if (date.DayOfWeek != DayOfWeek.Monday) // start the menu with Monday
         {
            date = date + TimeSpan.FromDays(-((int) date.DayOfWeek - 1));
            DaySelection.SelectedDate = date; // fire event once more
            return;
         }

         FillMenuList(date);
      }

      private void OnRemoveDay(object sender, RoutedEventArgs e)
      {
         _menus.Remove(GridMenus.SelectedItem as MenuEntry);
      }

      private void OnAddDay(object sender, RoutedEventArgs e)
      {
         var selectedMenu = GridMenus.SelectedItem as MenuEntry;

         if (selectedMenu != null)
         {
            var mi = new MenuEntry
            {
               Day = selectedMenu.Day,
               Price = selectedMenu.Price
            };
            _menus.Insert(_menus.IndexOf(selectedMenu), mi);
         }
      }

      private void OnCreateDoc(object sender, RoutedEventArgs e)
      {
         if (_menus.Count == 0)
         {
            MessageBox.Show("Select a date first", "Menu Planner", MessageBoxButton.OK);
            return;
         }
         var page2 = new DocumentPage();
         if (NavigationService != null)
         {
            NavigationService.LoadCompleted += page2.NavigationService_LoadCompleted;
            NavigationService.Navigate(page2, _menus);
         }
         // NavigationService.Navigate(new Uri("pack://application:,,,/DocumentPage.xaml"), menus);
      }

      private void OnSaveConfig(object sender, RoutedEventArgs e)
      {
         var config = new MenuConfiguration();
         var configMenus = from m in _menus
            select new MenuConfigDay
            {
               DayOfWeek = m.Day.DayOfWeek,
               Price = m.Price
            };
         config.MenuConfig.AddRange(configMenus);
         Settings.Default.MenuConfigDays = config;
         Settings.Default.Save();
      }
   }
}