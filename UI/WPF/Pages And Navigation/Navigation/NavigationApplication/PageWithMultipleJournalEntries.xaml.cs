using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NavigationApplication
{
   public partial class PageWithMultipleJournalEntries : IProvideCustomContentState
   {
      private string _restoredStateName;

      public PageWithMultipleJournalEntries()
      {
         InitializeComponent();
      }

      public CustomContentState GetContentState()
      {
         return
            GetJournalEntry(_restoredStateName != string.Empty
               ? _restoredStateName
               : "PageWithMultipleJournalEntries");
      }

      private void OnPageLoaded(object sender, EventArgs e)
      {
         SourceListBox.Items.Add("Red");
         SourceListBox.Items.Add("Blue");
         SourceListBox.Items.Add("Green");
         SourceListBox.Items.Add("Yellow");
         SourceListBox.Items.Add("Orange");
         SourceListBox.Items.Add("Black");
         SourceListBox.Items.Add("Pink");
         SourceListBox.Items.Add("Purple");
      }

      private void OnAdd(object sender, RoutedEventArgs e)
      {
         if (SourceListBox.SelectedIndex != -1)
         {
            // Determine the best name to use in the navigation history.
            var navSvc = NavigationService.GetNavigationService(this);
            var itemText = SourceListBox.SelectedItem.ToString();
            var journalName = string.Format("Added {0}", itemText);

            // Update the journal (using the method shown below.)        
            if (navSvc != null)
               navSvc.AddBackEntry(GetJournalEntry(journalName));

            // Now perform the change.
            TargetListBox.Items.Add(itemText);
            SourceListBox.Items.Remove(itemText);
         }
      }

      private void Replay(ListSelectionJournalEntry state)
      {
         SourceListBox.Items.Clear();
         foreach (var item in state.SourceItems)
            SourceListBox.Items.Add(item);

         TargetListBox.Items.Clear();
         foreach (var item in state.TargetItems)
            TargetListBox.Items.Add(item);

         _restoredStateName = state.JournalEntryName;
      }

      private void OnRemove(object sender, RoutedEventArgs e)
      {
         if (TargetListBox.SelectedIndex == -1)
            return;

         // Determine the best name to use in the navigation history.
         var navSvc = NavigationService.GetNavigationService(this);
         var itemText = TargetListBox.SelectedItem.ToString();
         var journalName = string.Format("Removed {0}", itemText);

         // Update the journal (using the method shown below.)        
         if (navSvc != null)
            navSvc.AddBackEntry(GetJournalEntry(journalName));

         // Perform the change.
         SourceListBox.Items.Add(itemText);
         TargetListBox.Items.Remove(itemText);
      }

      private ListSelectionJournalEntry GetJournalEntry(string journalName)
      {
         // Get the state of both lists (using a helper method).
         var source = GetListState(SourceListBox);
         var target = GetListState(TargetListBox);

         // Create the custom state object with this information.
         // Point the callback to the Replay method in this class.
         return new ListSelectionJournalEntry(source, target, journalName, Replay);
      }

      private static List<string> GetListState(ItemsControl list)
      {
         return list.Items.Cast<string>().ToList();
      }
   }
}