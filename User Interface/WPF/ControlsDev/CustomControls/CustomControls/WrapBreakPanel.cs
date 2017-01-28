using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
   /// <summary>
   ///   Элемент управления для размещения дочерних элементов
   /// </summary>
   public class WrapBreakPanel : Panel
   {
      public static readonly DependencyProperty LineBreakBeforeProperty;

      static WrapBreakPanel()
      {
         var metadata = new FrameworkPropertyMetadata
         {
            AffectsArrange = true,
            AffectsMeasure = true
         };
         LineBreakBeforeProperty =
            DependencyProperty.RegisterAttached("LineBreakBefore", typeof (bool), typeof (WrapBreakPanel), metadata);
      }

      public static void SetLineBreakBefore(UIElement element, bool value)
      {
         element.SetValue(LineBreakBeforeProperty, value);
      }

      public static bool GetLineBreakBefore(UIElement element)
      {
         return (bool) element.GetValue(LineBreakBeforeProperty);
      }

      protected override Size MeasureOverride(Size constraint)
      {
         var currentLineSize = new Size();
         var panelSize = new Size();

         foreach (var element in InternalChildren.Cast<UIElement>())
         {
            element.Measure(constraint);
            var desiredSize = element.DesiredSize;

            if (GetLineBreakBefore(element) ||
                currentLineSize.Width + desiredSize.Width > constraint.Width)
            {
               // Перейти на новую строку (либо потому, что элемент требует,
               // либо потому, что закончилось место в текущей строке).
               panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
               panelSize.Height += currentLineSize.Height;
               currentLineSize = desiredSize;

               // Если элемент слишком широк, чтобы поместиться в ширину
               // строки, просто выделить ему отдельную строку.
               if (desiredSize.Width > constraint.Width)
               {
                  panelSize.Width = Math.Max(desiredSize.Width, panelSize.Width);
                  panelSize.Height += desiredSize.Height;
                  currentLineSize = new Size();
               }
            }
            else
            {
               // Продолжать добавление в текущую строку.
               currentLineSize.Width += desiredSize.Width;

               // Установить высоту строки по высоте ее максимального элемента.
               currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
            }
         }

         // Вернуть размер, необходимый для размещения всех элементов. Обычно это будет
         // ширина ограничения, а высота базируется на размерах элементов. Однако если элемент
         // шире, чем ширина панели, то желаемая ширина будет шириной строки.
         panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
         panelSize.Height += currentLineSize.Height;

         return panelSize;
      }

      protected override Size ArrangeOverride(Size arrangeBounds)
      {
         var firstInLine = 0;
         var currentLineSize = new Size();
         double accumulatedHeight = 0;

         var elements = InternalChildren;
         for (var i = 0; i < elements.Count; i++)
         {
            var desiredSize = elements[i].DesiredSize;

            if (GetLineBreakBefore(elements[i]) || currentLineSize.Width + desiredSize.Width > arrangeBounds.Width)
               //need to switch to another line
            {
               ArrangeLine(accumulatedHeight, currentLineSize.Height, firstInLine, i);

               accumulatedHeight += currentLineSize.Height;
               currentLineSize = desiredSize;

               if (desiredSize.Width > arrangeBounds.Width)
                  //the element is wider then the constraint - give it a separate line                    
               {
                  ArrangeLine(accumulatedHeight, desiredSize.Height, i, ++i);
                  accumulatedHeight += desiredSize.Height;
                  currentLineSize = new Size();
               }
               firstInLine = i;
            }
            else //continue to accumulate a line
            {
               currentLineSize.Width += desiredSize.Width;
               currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
            }
         }

         if (firstInLine < elements.Count)
            ArrangeLine(accumulatedHeight, currentLineSize.Height, firstInLine, elements.Count);

         return arrangeBounds;
      }

      void ArrangeLine(double y, double lineHeight, int start, int end)
      {
         double x = 0;
         var children = InternalChildren;
         for (var i = start; i < end; i++)
         {
            var child = children[i];
            child.Arrange(new Rect(x, y, child.DesiredSize.Width, lineHeight));
            x += child.DesiredSize.Width;
         }
      }
   }
}