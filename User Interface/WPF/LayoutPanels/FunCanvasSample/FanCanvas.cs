using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FunCanvasSample
{
   public sealed class FanCanvas : Panel
   {
      public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
         "Orientation", typeof(Orientation), typeof(FanCanvas),
         new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsArrange));

      public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
         "Spacing", typeof(double),
         typeof(FanCanvas), new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsArrange));

      public static readonly DependencyProperty AngleIncrementProperty = DependencyProperty.Register(
         "AngleIncrement", typeof(double), typeof(FanCanvas),
         new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsArrange));

      public double AngleIncrement
      {
         get { return (double) GetValue(AngleIncrementProperty); }
         set { SetValue(AngleIncrementProperty, value); }
      }

      public Orientation Orientation
      {
         get { return (Orientation) GetValue(OrientationProperty); }
         set { SetValue(OrientationProperty, value); }
      }

      public double Spacing
      {
         get { return (double) GetValue(SpacingProperty); }
         set { SetValue(SpacingProperty, value); }
      }

      protected override Size MeasureOverride(Size availableSize)
      {
         foreach (var child in Children.Cast<UIElement>().Where(element => element != null))
            child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

         // Для FanCanvas места не нужно
         return new Size(0, 0);
      }

      protected override Size ArrangeOverride(Size finalSize)
      {
         // Центрируем дочерние элементы
         var location = new Point(0, 0);
         var angle = GetStartingAngle();

         foreach (var child in Children.Cast<UIElement>().Where(element => element != null))
         {
            // Выделяем потомку область предпочтительного размера
            child.Arrange(new Rect(location, child.DesiredSize));

            // NOTE: Подменяем заданное RenderTransform преобразованием, которое располагает потомков в виде веера
            child.RenderTransform = new RotateTransform(angle, child.RenderSize.Width / 2, child.RenderSize.Height);

            // Подготавливаем смещение и угол для следующего потомка
            if (Orientation == Orientation.Vertical)
               location.Y += Spacing;
            else
               location.X += Spacing;

            angle += AngleIncrement;
         }

         // Забираем себе все предоставленное нам место
         return finalSize;
      }

      private double GetStartingAngle()
      {
         var angle = Children.Count % 2 != 0
            // Нечетное, значит, угол среднего потомка равен 0 
            ? -AngleIncrement * ((double) Children.Count / 2)
            // Четное, два средних потомка отстоят на половину AngleIncrement по разные стороны от 0
            : -AngleIncrement * ((double) Children.Count / 2) + AngleIncrement / 2;

         // Поворачиваем на 90 градусов, если ориентация вертикальная
         if (Orientation == Orientation.Vertical)
            angle += 90;

         return angle;
      }
   }
}