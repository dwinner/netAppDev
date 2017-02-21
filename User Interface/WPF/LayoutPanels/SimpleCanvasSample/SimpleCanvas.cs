using PostSharp.Patterns.Contracts;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SimpleCanvasSample
{
   public sealed class SimpleCanvas : Panel
   {
      public static readonly DependencyProperty LeftProperty = DependencyProperty.RegisterAttached("Left",
         typeof(double), typeof(SimpleCanvas),
         new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsParentArrange));

      public static readonly DependencyProperty TopProperty = DependencyProperty.RegisterAttached("Top",
         typeof(double), typeof(SimpleCanvas),
         new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsParentArrange));

      [TypeConverter(typeof(LengthConverter))]
      [AttachedPropertyBrowsableForChildren]
      public static void SetTop([Required] DependencyObject element, double value)
      {
         element.SetValue(TopProperty, value);
      }

      [TypeConverter(typeof(LengthConverter))]
      [AttachedPropertyBrowsableForChildren]
      public static double GetTop([Required] DependencyObject element)
      {
         return (double)element.GetValue(TopProperty);
      }

      [TypeConverter(typeof(LengthConverter))]
      [AttachedPropertyBrowsableForChildren]
      public static void SetLeft([Required] DependencyObject element, double value)
      {
         element.SetValue(LeftProperty, value);
      }

      [TypeConverter(typeof(LengthConverter))]
      [AttachedPropertyBrowsableForChildren]
      public static double GetLeft([Required] DependencyObject element)
      {
         return (double)element.GetValue(LeftProperty);
      }

      protected override Size MeasureOverride(Size availableSize)
      {
         foreach (var child in Children.Cast<UIElement>().Where(element => element != null))
            child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

         // Для самой панели SimpleCanvas никакого места не нужно
         return new Size(0, 0);
      }

      protected override Size ArrangeOverride(Size finalSize)
      {
         foreach (var child in Children.Cast<UIElement>().Where(element => element != null))
         {
            double x = 0;
            double y = 0;

            // Если присоединенные свойства Left или Top заданы, учтем их,
            // иначе поместим дочерний элемент в точку (0,0)
            var left = GetLeft(child);
            var top = GetTop(child);

            if (!double.IsNaN(left))
               x = left;

            if (!double.IsNaN(top))
               y = top;

            // Поместим в выбранную точку (x,y) с размером DesiredSize
            child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
         }

         // Устроит любой выделенный размер
         return finalSize;
      }
   }
}