using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SimpleStackPanelSample
{
   public sealed class SimpleStackPanel : Panel
   {
      public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
         typeof(Orientation), typeof(SimpleStackPanel),
         new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

      /// <summary>
      ///    Направление роста стопки
      /// </summary>
      public Orientation Orientation
      {
         get { return (Orientation) GetValue(OrientationProperty); }
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
            // Запрашиваем у первого дочернего элемента предпочтительный размер,
            // предоставляя ему неограниченное место в направлении роста стопки,
            // но в другом направлении лишь столько места, сколько выделили нам.
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

         return desiredSize;
      }

      protected override Size ArrangeOverride(Size finalSize)
      {
         double offset = 0;

         foreach (var child in Children.Cast<UIElement>().Where(element => element != null))
            if (Orientation == Orientation.Vertical)
            {
               // Переменная offset определяет сдвиг дочернего элемента
               // относительно начала стопки. Выделяем ему столько ширины,
               // сколько нам предоставлено, и столько высоты, сколько он захочет.
               child.Arrange(new Rect(0, offset, finalSize.Width, child.DesiredSize.Height));

               // Обновляем offset для следующего дочернего элемента
               offset += child.DesiredSize.Height;
            }
            else
            {
               // Переменная offset определяет сдвиг дочернего элемента относительно начала
               // стопки. Выделяем ему столько высоты, сколько нам предоставлено, и столько
               // ширины, сколько он захочет
               child.Arrange(new Rect(offset, 0, child.DesiredSize.Width, finalSize.Height));

               // Обновляем offset для следующего дочернего элемента
               offset += child.DesiredSize.Width;
            }

         // Забираем себе все предоставленное место
         return finalSize;
      }
   }
}