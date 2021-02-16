using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Math;
using static Adu.DrawingApp.Utils.MathHelpers;

namespace Adu.DrawingApp.Utils
{
   /// <summary>
   ///    Some generator's methods
   /// </summary>
   public static class DrawingFactory
   {
      private static readonly SolidColorBrush BorderColor = new SolidColorBrush { Color = Colors.Gray };
      private static readonly SolidColorBrush FillColor = new SolidColorBrush { Color = Colors.LightGray, Opacity = 0.5 };

      /// <summary>
      ///    Get a new instance of the painting path
      /// </summary>
      private static Path NewPath => new Path { Fill = FillColor, Stroke = BorderColor };

      /// <summary>
      ///    Creating a new rectangle
      /// </summary>
      /// <param name="firstPoint">Top left point</param>
      /// <param name="secondPoint">Bottom right point</param>
      /// <returns>A new rectangle</returns>
      public static Path CreateRectangle(Point firstPoint, Point secondPoint)
      {
         var path = NewPath;
         var rectGeom = new RectangleGeometry();
         var inscriberRect = GetInscribingRect(firstPoint, secondPoint);
         rectGeom.Rect = new Rect(inscriberRect.Left, inscriberRect.Top, inscriberRect.Width, inscriberRect.Height);
         path.Data = rectGeom;

         return path;
      }

      /// <summary>
      ///    Creating a new square
      /// </summary>
      /// <param name="firstPoint">Top left point</param>
      /// <param name="secondPoint">Bottom right point</param>
      /// <returns>A new square</returns>
      public static Path CreateSquare(Point firstPoint, Point secondPoint)
      {
         var path = NewPath;
         var rectangleGeom = new RectangleGeometry();
         var inscriber = GetInscribingRect(firstPoint, secondPoint);
         var side = Min(inscriber.Width, inscriber.Height);
         rectangleGeom.Rect = new Rect(inscriber.Left, inscriber.Top, side, side);
         path.Data = rectangleGeom;

         return path;
      }

      /// <summary>
      ///    Creating a new circle
      /// </summary>
      /// <param name="firstPoint">Top left point</param>
      /// <param name="secondPoint">Bottom right point</param>
      /// <returns>A new circle</returns>
      public static Path CreateCircle(Point firstPoint, Point secondPoint)
      {
         var path = NewPath;
         var ellipseGeom = new EllipseGeometry();
         var inscriber = GetInscribingRect(firstPoint, secondPoint);
         var side = Min(inscriber.Width, inscriber.Height);
         ellipseGeom.Center = new Point(inscriber.Left + side / 2, inscriber.Top + side / 2);
         ellipseGeom.RadiusX = side / 2;
         ellipseGeom.RadiusY = side / 2;
         path.Data = ellipseGeom;

         return path;
      }

      /// <summary>
      ///    Creating a new ellipse
      /// </summary>
      /// <param name="firstPoint">Top left point</param>
      /// <param name="secondPoint">Bottom right point</param>
      /// <returns>A new ellipse</returns>
      public static Path CreateEllipse(Point firstPoint, Point secondPoint)
      {
         var path = NewPath;
         var ellipseGeom = new EllipseGeometry();
         var inscriber = GetInscribingRect(firstPoint, secondPoint);
         ellipseGeom.Center =
            new Point(inscriber.Left + inscriber.Width / 2, inscriber.Top + inscriber.Height / 2);
         ellipseGeom.RadiusX = inscriber.Width / 2;
         ellipseGeom.RadiusY = inscriber.Height / 2;
         path.Data = ellipseGeom;

         return path;
      }

      /// <summary>
      ///    Creating a new line
      /// </summary>
      /// <param name="firstPoint">Top left point</param>
      /// <param name="secondPoint">Bottom right point</param>
      /// <returns>A new line</returns>
      public static Path CreateLine(Point firstPoint, Point secondPoint)
      {
         var path = NewPath;
         var lineGeom = new LineGeometry { StartPoint = firstPoint, EndPoint = secondPoint };
         path.Data = lineGeom;

         return path;
      }
   }
}