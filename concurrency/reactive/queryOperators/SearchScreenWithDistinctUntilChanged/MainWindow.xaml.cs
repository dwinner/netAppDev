using System;
using System.Reactive.Linq;

namespace SearchScreenWithDistinctUntilChanged;

public partial class MainWindow
{
   public MainWindow()
   {
      InitializeComponent();

      Observable.FromEventPattern(searchTerm, "TextChanged")
         .Select(_ => searchTerm.Text)
         .Throttle(TimeSpan.FromMilliseconds(400))
         .DistinctUntilChanged()
         .ObserveOnDispatcher()
         .Subscribe(s => terms.Items.Add(s));
   }
}