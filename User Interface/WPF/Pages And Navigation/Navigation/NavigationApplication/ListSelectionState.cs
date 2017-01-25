using System;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace NavigationApplication
{
   public delegate void ReplayListChange(ListSelectionJournalEntry state);

   [Serializable]
   public class ListSelectionJournalEntry : CustomContentState
   {
      private readonly string _journalName;
      private readonly ReplayListChange _replayListChange;

      public ListSelectionJournalEntry(
         List<string> sourceItems, List<string> targetItems, string journalName, ReplayListChange replayListChange)
      {
         SourceItems = sourceItems;
         TargetItems = targetItems;
         _journalName = journalName;
         _replayListChange = replayListChange;
      }

      public List<string> SourceItems { get; private set; }

      public List<string> TargetItems { get; private set; }

      // Need to override this property, if you want a CustomJournalEntry to appear in your back/forward stack
      public override string JournalEntryName
      {
         get { return _journalName; }
      }

      // MANDATORY:  Need to override this method to restore the required state.
      // Since the "navigation" is not user-initiated ie. set by the user selecting 
      // a new ListBoxItem, we set the flag to false.
      public override void Replay(NavigationService navigationService, NavigationMode mode)
      {
         _replayListChange(this);
      }
   }
}