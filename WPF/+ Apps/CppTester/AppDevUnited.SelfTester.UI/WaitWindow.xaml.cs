using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AppDevUnited.SelfTester.UI
{
   public partial class WaitWindow
   {
      private const double DefaultWidthMinPadding = 10;
      private const double DefaultMinHeight = 100;
      private bool _isAltF4Close;
      private bool _isWiden;
      private bool _isAltDown;

      public WaitWindow(string aHeaderText, string aStatusTest)
         : this()
      {
         HeaderTextBlock.Text = aHeaderText;
         StatusTextBlock.Text = aStatusTest;
         var textMeasuring = MeasureString(aStatusTest);
         MinWidth = Math.Ceiling(textMeasuring.Width) + DefaultWidthMinPadding;
         MinHeight = DefaultMinHeight;
         Cursor = Cursors.Wait;
         var storyboard = new Storyboard();
         var animation = new DoubleAnimation
         {
            Duration = TimeSpan.FromSeconds(1),
            RepeatBehavior = RepeatBehavior.Forever,
            From = 0.0,
            To = 1.0
         };
         Storyboard.SetTargetName(animation, StatusTextBlock.Name);
         Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
         storyboard.Children.Add(animation);
         storyboard.Begin(this);
      }

      public WaitWindow()
      {
         InitializeComponent();
         WindowStartupLocation = WindowStartupLocation.CenterScreen;
         ShowInTaskbar = false;
      }

      public WaitWindow(Window ownerWindow)
         : this()
      {
         Owner = ownerWindow;
      }

      private Size MeasureString(string text)
      {
         var formattedText = new FormattedText(text, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
            new Typeface(StatusTextBlock.FontFamily, StatusTextBlock.FontStyle, StatusTextBlock.FontWeight,
               StatusTextBlock.FontStretch), StatusTextBlock.FontSize, Brushes.Black);

         return new Size(formattedText.Width, formattedText.Height);
      }

      private void InitiateWiden(object sender, MouseEventArgs e)
      {
         _isWiden = true;
      }

      private void EndWiden(object sender, MouseEventArgs e)
      {
         _isWiden = false;
         var rectangle = (Rectangle)sender;
         rectangle.ReleaseMouseCapture();
      }

      private void Widen(object sender, MouseEventArgs e)
      {
         var rect = (Rectangle)sender;
         if (_isWiden)
         {
            rect.CaptureMouse();
            var newWidth = e.GetPosition(this).X + 5;
            if (newWidth > 0) Width = newWidth;
         }
      }

      public void ShowDialogAlt()
      {
         ShowDialog();
      }

      private void TitleBar_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }

      private void WaitWindow_OnClosing(object sender, CancelEventArgs e)
      {
         e.Cancel = _isAltF4Close;
         _isAltF4Close = false;
      }

      private void WaitWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
      {
         if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
         {
            _isAltDown = true;
         }
         else if (e.SystemKey == Key.F4 && _isAltDown)
         {
            e.Handled = true;
         }
      }

      private void WaitWindow_OnPreviewKeyUp(object sender, KeyEventArgs e)
      {
         if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
         {
            _isAltDown = false;
         }
      }
   }
}