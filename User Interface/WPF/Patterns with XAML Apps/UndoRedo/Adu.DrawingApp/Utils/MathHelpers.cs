using System.Windows;
using Adu.DrawingApp.Poco;
using static System.Math;

namespace Adu.DrawingApp.Utils
{
   /// <summary>
   ///    A set of domain math helper methods
   /// </summary>
   public static class MathHelpers
   {
      /// <summary>
      ///    Get the inscribing rectangle of two points
      /// </summary>
      /// <param name="firstPoint">A first point</param>
      /// <param name="secondPoint">A second point</param>
      /// <returns>The inscribing rectangle of two points</returns>
      public static Inscriber GetInscribingRect(Point firstPoint, Point secondPoint)
         => new Inscriber
         {
            Width = Abs(firstPoint.X - secondPoint.X),
            Height = Abs(firstPoint.Y - secondPoint.Y),
            Left = Min(firstPoint.X, secondPoint.X),
            Top = Min(firstPoint.Y, secondPoint.Y)
         };
   }
}