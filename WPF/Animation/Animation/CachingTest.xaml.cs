using System;
using System.Windows;
using System.Windows.Media;

namespace Animation
{
   public partial class CachingTest
   {
      public CachingTest()
      {
         InitializeComponent();

         var pathGeometry = new PathGeometry();
         var pathFigure = new PathFigure { StartPoint = new Point(0, 0) };
         var pathSegmentCollection = new PathSegmentCollection();

         var maxHeight = (int)Height;
         var maxWidth = (int)Width;
         var rand = new Random();
         for (var i = 0; i < 500; i++)
         {
            var newSegment = new LineSegment { Point = new Point(rand.Next(0, maxWidth), rand.Next(0, maxHeight)) };
            pathSegmentCollection.Add(newSegment);
         }

         pathFigure.Segments = pathSegmentCollection;
         pathGeometry.Figures.Add(pathFigure);

         BackgroundPath.Data = pathGeometry;
      }

      private void OnCacheOnOff(object sender, RoutedEventArgs e)
      {
         BackgroundPath.CacheMode = CacheCheckbox.IsChecked == true ? new BitmapCache() : null;
      }
   }
}