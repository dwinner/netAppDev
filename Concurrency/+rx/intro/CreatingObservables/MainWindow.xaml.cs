using System;
using System.Reactive.Linq;

namespace CreatingObservables
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         Observe();
      }

      private void Observe()
      {
         var source = Observable.Return(42);
         source.Subscribe(
            x => messagesListBox.Items.Add($"OnNext: {x}"),
            ex => messagesListBox.Items.Add($"OnError: {ex.Message}"),
            () => messagesListBox.Items.Add("OnCompleted"));
      }
   }
}