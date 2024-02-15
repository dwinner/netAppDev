using System;
using System.Reactive.Linq;

namespace ObserveOnDispatcher;

public partial class MainWindow
{
   public MainWindow()
   {
      InitializeComponent();

      Observable.FromEventPattern(textBox, "TextChanged")
         .Select(_ => textBox.Text)
         .Throttle(TimeSpan.FromMilliseconds(400))
         .ObserveOnDispatcher()
         //.ObserveOn(DispatcherScheduler.Current)
         .Subscribe(text => throttledResults.Items.Add(text));
   }
}