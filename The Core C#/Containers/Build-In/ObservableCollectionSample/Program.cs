/**
 * Наблюдаемые коллекции
 */

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObservableCollectionSample
{
   internal static class Program
   {
      static void Main()
      {
         var data = new ObservableCollection<string>();
         data.CollectionChanged += DataOnCollectionChanged;

         // Add
         data.Add("One");
         data.Add("Two");
         data.Insert(1, "Three");

         // Remove
         data.Remove("One");

         // Move
         if (data.Count > 2)
            data.Move(0, 1);

         // Replace
         if (data.Count != 0)
            data[0] = "One To Replaced";

         // Reset
         data.Clear();
      }

      private static void DataOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
         NotifyCollectionChangedAction action = e.Action;
         Console.WriteLine("Action: {0}", action);
         switch (action)
         {
            case NotifyCollectionChangedAction.Add:
               if (e.NewItems != null)
               {
                  Console.WriteLine("Starting index for new item(s): {0}", e.NewStartingIndex);
                  Console.Write("New items: ");
                  foreach (var newItem in e.NewItems)
                  {
                     Console.Write("{0} ", newItem);
                  }
               }
               break;

            case NotifyCollectionChangedAction.Remove:
               if (e.OldItems != null)
               {
                  Console.WriteLine("Starting index for old item(s): {0}", e.OldStartingIndex);
                  Console.Write("Old item(s): ");
                  foreach (var oldItem in e.OldItems)
                  {
                     Console.Write("{0} ", oldItem);
                  }
               }
               break;

            case NotifyCollectionChangedAction.Replace:
               Console.WriteLine("Some data was replaced at {0}", e.OldStartingIndex);
               break;

            case NotifyCollectionChangedAction.Reset:
               Console.WriteLine("Data was reset");
               break;

            case NotifyCollectionChangedAction.Move:
               Console.WriteLine("Some data was moved: ");
               if (e.NewItems != null && e.OldItems != null)
               {
                  Console.WriteLine("Data index from move: {0}", e.OldStartingIndex);
                  Console.WriteLine("Data index to move: {0}", e.NewStartingIndex);
               }
               break;
         }

         Console.WriteLine();
      }
   }
}
