using System;
using System.Reactive.Linq;
using System.Threading;

namespace CreatingPeriodicUpdatableView
{
   public partial class MainWindow
   {
      private IDisposable _subscription;

      public MainWindow()
      {
         InitializeComponent();

         var updatesWebService = new UpdatesWebService();
         _subscription = Observable
            .Interval(TimeSpan.FromSeconds(3))
            .Take(3) // we dont want to run the example forever, so we'll do only 3 updates
            .SelectMany(_ => updatesWebService.GetUpdatesAsync())
            .SelectMany(updates => updates)
            .ObserveOn(SynchronizationContext.Current)
            .Subscribe(message => messages.Items.Add(message));
      }
   }
}