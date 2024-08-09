using System;
using System.Reactive.Linq;
using System.Threading;

namespace CreatingPeriodicUpdatableView;

public partial class MainWindow
{
   private readonly UpdatesWebService _updatesWebService;
   private IDisposable _subscription;

   public MainWindow()
   {
      InitializeComponent();
      _updatesWebService = new UpdatesWebService();
   }

   protected override void OnActivated(EventArgs e)
   {
      base.OnActivated(e);
      _subscription = Observable
         .Interval(TimeSpan.FromSeconds(3))
         .Take(3) // we don't want to run the example forever, so we'll do only 3 updates
         .SelectMany(_ => _updatesWebService.GetUpdatesAsync())
         .SelectMany(updates => updates)
         .ObserveOn(SynchronizationContext.Current)
         .Subscribe(message => messages.Items.Add(message));
   }

   protected override void OnDeactivated(EventArgs e)
   {
      _subscription.Dispose();
      base.OnDeactivated(e);
   }
}