using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Drawing
{
   public class DrawingCanvas : Panel
   {
      private readonly List<DrawingVisual> _hits = new List<DrawingVisual>();
      private readonly List<Visual> _visuals = new List<Visual>();

      protected override int VisualChildrenCount
      {
         get { return _visuals.Count; }
      }

      protected override Visual GetVisualChild(int index)
      {
         return _visuals[index];
      }

      public void AddVisual(Visual visual)
      {
         _visuals.Add(visual);
         AddVisualChild(visual);
         AddLogicalChild(visual);
      }

      public void DeleteVisual(Visual visual)
      {
         _visuals.Remove(visual);
         RemoveVisualChild(visual);
         RemoveLogicalChild(visual);
      }

      public DrawingVisual GetVisual(Point point)
      {
         var hitResult = VisualTreeHelper.HitTest(this, point);
         return hitResult.VisualHit as DrawingVisual;
      }

      public List<DrawingVisual> GetVisuals(Geometry region)
      {
         // Очистить результаты предыдущей проверки
         _hits.Clear();

         // Подготовить параметры для операции проверки попадания (геометрию и обратный вызов).
         var parameters = new GeometryHitTestParameters(region);
         HitTestResultCallback callback = HitTestCallback;
         VisualTreeHelper.HitTest(this, null, callback, parameters);

         return _hits;
      }

      private HitTestResultBehavior HitTestCallback(HitTestResult result)
      {
         var geometryResult = (GeometryHitTestResult) result;
         var visual = result.VisualHit as DrawingVisual;

         // Попадание фиксируется, только если в точке найден объект
         // DrawingVisual, и он целиком находится в геометрии.
         if (visual != null &&
             geometryResult.IntersectionDetail == IntersectionDetail.FullyInside)
         {
            _hits.Add(visual);
         }

         return HitTestResultBehavior.Continue;
      }
   }
}