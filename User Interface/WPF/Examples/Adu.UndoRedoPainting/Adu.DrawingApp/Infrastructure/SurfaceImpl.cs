using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Shapes;
using static Adu.DrawingApp.Utils.DrawingFactory;

namespace Adu.DrawingApp.Infrastructure
{
   /// <summary>
   ///    Default implementation of ISurface
   /// </summary>
   public sealed class SurfaceImpl : ISurface<Path>
   {
      public SurfaceImpl()
      {
         Paths = new ObservableCollection<Path>();
      }

      /// <summary>
      ///    Observable collection of paths
      /// </summary>
      public ObservableCollection<Path> Paths { get; }

      /// <summary>
      ///    Add rectangle to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      public void AddRectangle(Point first, Point last)
      {
         var path = CreateRectangle(first, last);
         Paths.Add(path);
      }

      /// <summary>
      ///    Add square to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      public void AddSquare(Point first, Point last)
      {
         var path = CreateSquare(first, last);
         Paths.Add(path);
      }

      /// <summary>
      ///    Add circle to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      public void AddCircle(Point first, Point last)
      {
         var path = CreateCircle(first, last);
         Paths.Add(path);
      }

      /// <summary>
      ///    Add ellipse to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      public void AddEllipse(Point first, Point last)
      {
         var path = CreateEllipse(first, last);
         Paths.Add(path);
      }

      /// <summary>
      ///    Add line to path-collection
      /// </summary>
      /// <param name="first">First point</param>
      /// <param name="last">Last point</param>
      public void AddLine(Point first, Point last)
      {
         var path = CreateLine(first, last);
         Paths.Add(path);
      }

      /// <summary>
      ///    Add path to path-collection
      /// </summary>
      /// <param name="path">Shape path</param>
      public void AddPath(Path path) => Paths.Add(path);

      /// <summary>
      ///    Remove last added path
      /// <remarks>Can be null, if no items in <see cref="Paths"/></remarks>
      /// </summary>
      public Path RemoveLast()
      {
         if (Paths.Count > 0)
         {
            var pathToRemove = Paths[Paths.Count - 1];
            Paths.Remove(pathToRemove);
            return pathToRemove;
         }

         return null;
      }
   }
}