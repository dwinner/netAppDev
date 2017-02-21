using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OverlapPanelSample
{
   public sealed class OverlapPanel : Panel
   {
      public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
         typeof(Orientation), typeof(OverlapPanel),
         new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

      private double _totalChildrenSize;

      /// <summary>
      ///    Направление роста стопки
      /// </summary>
      public Orientation Orientation
      {
         get { return (Orientation)GetValue(OrientationProperty); }
         set { SetValue(OrientationProperty, value); }
      }

      protected override Size MeasureOverride(Size availableSize)
      {
         var desiredSize = new Size();

         // Разрешаем дочерним элементам неограниченно расти в направлении стопки,
         // для чего подменяем переданное значение.
         if (Orientation == Orientation.Vertical)
            availableSize.Height = double.PositiveInfinity;
         else
            availableSize.Width = double.PositiveInfinity;

         foreach (var child in Children.Cast<UIElement>().Where(element => element != null))
         {
            // Смотрим, какой размер предпочтет потомок,
            // если ему выделить все имеющееся у нас место
            child.Measure(availableSize);

            // Наш предпочтительный размер - это сумма размеров дочерних элементов
            // в направлении роста стопки и размер наибольшего элемента - в другом направлении
            if (Orientation == Orientation.Vertical)
            {
               desiredSize.Width = Math.Max(desiredSize.Width, child.DesiredSize.Width);
               desiredSize.Height += child.DesiredSize.Height;
            }
            else
            {
               desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
               desiredSize.Width += child.DesiredSize.Width;
            }
         }

         _totalChildrenSize = Orientation == Orientation.Vertical ? desiredSize.Height : desiredSize.Width;

         return desiredSize;
      }

      protected override Size ArrangeOverride(Size finalSize)
      {
         double offset = 0;

         // Вычисляем величину перекрытия путем деления недостающего
         // места поровну на всех потомков                 
         var overlap = Orientation == Orientation.Vertical
            ? (finalSize.Height > _totalChildrenSize
            // Если нам выделили места больше, чем _totalChildrenSize, то отрицательное перекрытие означает растяжение
               ? (_totalChildrenSize - finalSize.Height) / Children.Count
            // В этом случае DesiredSize дает фактический меньший размер
               : (_totalChildrenSize - DesiredSize.Height) / Children.Count)
            : (finalSize.Width > _totalChildrenSize
               ? (_totalChildrenSize - finalSize.Width) / Children.Count
               : (_totalChildrenSize - DesiredSize.Width) / Children.Count);

         foreach (var child in Children.Cast<UIElement>().Where(element => element != null))
            if (Orientation == Orientation.Vertical)
            {
               // Переменная offset определяет сдвиг дочернего элемента
               // относительно начала стопки. Выделяем ему столько ширины,
               // сколько нам предоставлено, и столько высоты, сколько он захочет,
               // или больше в случае, когда перекрытие отрицательно
               child.Arrange(new Rect(0, offset, finalSize.Width,
                  child.DesiredSize.Height + (overlap > 0 ? 0 : -overlap)));

               // Обновляем offset для следующего дочернего элемента
               offset += child.DesiredSize.Height - overlap;
            }
            else
            {
               // Переменная offset определяет сдвиг дочернего элемента относительно начала
               // стопки. Выделяем ему столько высоты, сколько нам предоставлено, и столько
               // ширины, сколько он захочет
               // или больше в случае, когда перекрытие отрицательно
               child.Arrange(new Rect(offset, 0, child.DesiredSize.Width + (overlap > 0 ? 0 : -overlap),
                  finalSize.Height));

               // Обновляем offset для следующего дочернего элемента
               offset += child.DesiredSize.Width - overlap;
            }

         // Забираем себе все предоставленное место
         return finalSize;
      }
   }
}