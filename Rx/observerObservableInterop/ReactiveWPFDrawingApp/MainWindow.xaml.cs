using System;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ReactiveWPFDrawingApp
{
   public partial class MainWindow
   {
      private IDisposable _subscription;

      public MainWindow()
      {
         InitializeComponent();

         var mouseDowns = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseDown");
         var mouseUp = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseUp");
         var movements = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove");

         Polyline polyline = null;
         _subscription =
            movements
               .SkipUntil(
                  mouseDowns.Do(_ =>
                  {
                     polyline = new Polyline
                     {
                        Stroke = Brushes.Black,
                        StrokeThickness = 3
                     };

                     canvas.Children.Add(polyline);
                  }))
               .TakeUntil(mouseUp)
               .Select(m => m.EventArgs.GetPosition(this))
               .Repeat()
               .Subscribe(pos => polyline.Points.Add(pos));
      }
   }
}