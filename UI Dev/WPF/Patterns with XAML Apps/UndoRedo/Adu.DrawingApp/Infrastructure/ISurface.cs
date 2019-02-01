using System.Collections.ObjectModel;
using System.Windows;

namespace Adu.DrawingApp.Infrastructure
{
   /// <summary>
   ///    Surface contracts
   /// </summary>
   public interface ISurface<T>
   {
      /// <summary>
      ///    Observable collection of paths
      /// </summary>
      ObservableCollection<T> Paths { get; }

      /// <summary>
      ///    Add rectangle to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      void AddRectangle(Point first, Point last);

      /// <summary>
      ///    Add square to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      void AddSquare(Point first, Point last);

      /// <summary>
      ///    Add circle to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      void AddCircle(Point first, Point last);

      /// <summary>
      ///    Add ellipse to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      void AddEllipse(Point first, Point last);

      /// <summary>
      ///    Add line to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      void AddLine(Point first, Point last);

      /// <summary>
      ///    Add path to path-collection
      /// </summary>
      /// <param name="path">Shape path</param>
      void AddPath(T path);

      /// <summary>
      ///    Remove last added path
      /// </summary>
      T RemoveLast();
   }
}