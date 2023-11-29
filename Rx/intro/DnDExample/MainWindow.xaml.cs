using System;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace DnDExample;

public partial class MainWindow
{
   public MainWindow()
   {
      InitializeComponent();
      TuneRxRelationships();
   }

   private void TuneRxRelationships()
   {
      var mouseDown =
         from mouseEvt in Observable.FromEventPattern<MouseButtonEventArgs>(sourceImage, nameof(MouseLeftButtonDown))
         select mouseEvt.EventArgs.GetPosition(sourceImage);

      var mouseUp = Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseLeftButtonUp));

      var mouseMove =
         from evt in Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseMove))
         select evt.EventArgs.GetPosition(this);

      var subscriptionQuery = from start in mouseDown
         from end in mouseMove.TakeUntil(mouseUp)
         select new
         {
            X = end.X - start.X,
            Y = end.Y - start.Y
         };

      subscriptionQuery.Subscribe(arg =>
      {
         Canvas.SetLeft(sourceImage, arg.X);
         Canvas.SetTop(sourceImage, arg.Y);
      });
   }
}