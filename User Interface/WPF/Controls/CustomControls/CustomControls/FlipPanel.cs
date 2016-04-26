using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CustomControls
{
   /// <summary>
   ///   Элемент с поддержкой визуальных состояний
   /// </summary>
   [TemplatePart(Name = "FlipButton", Type = typeof (ToggleButton))]
   [TemplatePart(Name = "FlipButtonAlternate", Type = typeof (ToggleButton))]
   [TemplateVisualState(Name = "Normal", GroupName = "ViewStates")]
   [TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
   public class FlipPanel : Control
   {
      #region Свойства зависимости

      public static readonly DependencyProperty FrontContentProperty =
         DependencyProperty.Register("FrontContent", typeof (object), typeof (FlipPanel), null);

      public static readonly DependencyProperty BackContentProperty =
         DependencyProperty.Register("BackContent", typeof (object), typeof (FlipPanel), null);

      public static readonly DependencyProperty CornerRadiusProperty =
         DependencyProperty.Register("CornerRadius", typeof (CornerRadius), typeof (FlipPanel), null);

      public static readonly DependencyProperty IsFlippedProperty =
         DependencyProperty.Register("IsFlipped", typeof (bool), typeof (FlipPanel), null);

      #endregion

      static FlipPanel()
      {
         DefaultStyleKeyProperty.OverrideMetadata(typeof (FlipPanel), new FrameworkPropertyMetadata(typeof (FlipPanel)));
      }

      #region Свойства

      public object FrontContent
      {
         get { return GetValue(FrontContentProperty); }
         set { SetValue(FrontContentProperty, value); }
      }

      public object BackContent
      {
         get { return GetValue(BackContentProperty); }
         set { SetValue(BackContentProperty, value); }
      }

      public CornerRadius CornerRadius
      {
         get { return (CornerRadius) GetValue(CornerRadiusProperty); }
         set { SetValue(CornerRadiusProperty, value); }
      }

      public bool IsFlipped
      {
         get { return (bool) GetValue(IsFlippedProperty); }
         set
         {
            SetValue(IsFlippedProperty, value);
            ChangeVisualState(true);
         }
      }

      #endregion

      public override void OnApplyTemplate()
      {
         base.OnApplyTemplate();

         // Привязка события ToggleButton.Click
         var flipButton = GetTemplateChild("FlipButton") as ToggleButton;
         if (flipButton != null)
            flipButton.Click += OnFlipButtonClick;

         // При необходимости разрешить две кнопки (по одной на каждую сторону панели).
         var flipButtonAlternate = GetTemplateChild("FlipButtonAlternate") as ToggleButton;
         if (flipButtonAlternate != null)
            flipButtonAlternate.Click += OnFlipButtonClick;

         // Обеспечить соответствие визуальных элементов текущему состоянию
         ChangeVisualState(false);
      }

      void OnFlipButtonClick(object sender, RoutedEventArgs e)
      {
         IsFlipped = !IsFlipped;
      }

      void ChangeVisualState(bool useTransitions)
      {
         VisualStateManager.GoToState(this, !IsFlipped ? "Normal" : "Flipped", useTransitions);
         
         var front = FrontContent as UIElement;
         if (front != null)
         {
            front.Visibility = IsFlipped ? Visibility.Hidden : Visibility.Visible;
         }

         var back = BackContent as UIElement;
         if (back != null)
         {
            back.Visibility = IsFlipped ? Visibility.Visible : Visibility.Hidden;
         }
      }
   }
}