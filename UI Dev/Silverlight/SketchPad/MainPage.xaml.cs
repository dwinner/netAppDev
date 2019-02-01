using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Printing;
using System.Windows.Shapes;

namespace SketchPad
{
   public partial class MainPage : UserControl
   {
      private bool _isDrawing;
      private Point _prevPt;

      public MainPage()
      {
         InitializeComponent();

         MouseLeftButtonDown += MainPage_MouseLeftButtonDown;
         MouseLeftButtonUp += MainPage_MouseLeftButtonUp;
         MouseMove += MainPage_MouseMove;
      }

      private void MainPage_MouseMove(object sender, MouseEventArgs e)
      {
         if (_isDrawing)
         {
            var pt = e.GetPosition(canvas);
            if (pt != _prevPt)
            {
               var line = new Line
               {
                  StrokeThickness = 2,
                  Stroke = new SolidColorBrush(Colors.Black),
                  X1 = _prevPt.X,
                  Y1 = _prevPt.Y,
                  X2 = pt.X,
                  Y2 = pt.Y
               };
               canvas.Children.Add(line);
               _prevPt = pt;
            }
         }
      }

      private void MainPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         _isDrawing = false;
      }

      private void MainPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         _isDrawing = true;
         _prevPt = e.GetPosition(canvas);
      }

      private void buttonClear_Click(object sender, RoutedEventArgs e)
      {
         canvas.Children.Clear();
      }

      private void buttonPrint_Click(object sender, RoutedEventArgs e)
      {
         var doc = new PrintDocument();
         doc.PrintPage += doc_PrintPage;
         doc.Print("");
      }

      private void doc_PrintPage(object sender, PrintPageEventArgs e)
      {
         //simply set the PageVisual property to the UIElement
         //that you want to print
         e.PageVisual = canvas;
         e.HasMorePages = false;
      }
   }
}