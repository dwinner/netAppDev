using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Multitouch
{   
   public partial class RawTouch
   {
      // Карта активных эллипсов, идентифицируемая с помощью идентификаторов устройств касания
      private readonly Dictionary<int, Ellipse> _movingEllipses = new Dictionary<int, Ellipse>();

      public RawTouch()
      {
         InitializeComponent();
      }

      private void Canvas_OnTouchDown(object sender, TouchEventArgs e)
      {
         // Создать эллипс для прорисовки в новой точке касания.
         var ellipse = new Ellipse
         {
            Width = 30,
            Height = 30,
            Stroke = Brushes.White,
            Fill = Brushes.Green
         };

         // Позиционировать эллипс в точке касания.
         var touchPoint = e.GetTouchPoint(Canvas);
         Canvas.SetTop(ellipse, touchPoint.Bounds.Top);
         Canvas.SetLeft(ellipse, touchPoint.Bounds.Left);

         // Сохранить эллипс в активной коллекции.
         _movingEllipses[e.TouchDevice.Id] = ellipse;

         // Добавить эллипс на холст.
         Canvas.Children.Add(ellipse);
      }

      private void Canvas_OnTouchMove(object sender, TouchEventArgs e)
      {
         // Получить эллипс, соответствующий текущей точке касания.
         var ellipse = _movingEllipses[e.TouchDevice.Id];

         // Переместить эллипс в новую точку касания.
         var touchPoint = e.GetTouchPoint(Canvas);
         Canvas.SetTop(ellipse, touchPoint.Bounds.Top);
         Canvas.SetLeft(ellipse, touchPoint.Bounds.Left);
      }

      private void Canvas_OnTouchUp(object sender, TouchEventArgs e)
      {
         // Удалить эллипс с холста.
         _movingEllipses.Remove(e.TouchDevice.Id);         
      }
   }
}