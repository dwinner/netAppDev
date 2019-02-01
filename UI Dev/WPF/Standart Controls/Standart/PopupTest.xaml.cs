using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace Controls
{
   public partial class PopupTest
   {
      public PopupTest()
      {
         InitializeComponent();
      }

      private void run_MouseEnter(object sender, MouseEventArgs e)
      {
         PopLink.IsOpen = true;
      }

      private void lnk_Click(object sender, RoutedEventArgs e)
      {
         Process.Start(((Hyperlink) sender).NavigateUri.ToString());
      }

      void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
      {
         PopLink.IsOpen = false;
      }
   }

   public class PopupAllowKeyboardInput
   {
      public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
          "IsEnabled",
          typeof(bool),
          typeof(PopupAllowKeyboardInput),
          new PropertyMetadata(default(bool), IsEnabledChanged));

      public static bool GetIsEnabled(DependencyObject d)
      {
         return (bool)d.GetValue(IsEnabledProperty);
      }

      public static void SetIsEnabled(DependencyObject d, bool value)
      {
         d.SetValue(IsEnabledProperty, value);
      }

      private static void IsEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs ea)
      {
         EnableKeyboardInput((Popup)sender, (bool)ea.NewValue);
      }

      private static void EnableKeyboardInput(Popup popup, bool enable)
      {
         if (enable)
         {
            IInputElement element = null;
            popup.Loaded += (sender, args) =>
            {
               popup.Child.Focusable = true;
               popup.Child.IsVisibleChanged += (o, ea) =>
               {
                  if (popup.Child.IsVisible)
                  {
                     element = Keyboard.FocusedElement;
                     Keyboard.Focus(popup.Child);
                  }
               };
            };
            
            popup.Closed += (sender, args) => Keyboard.Focus(element);
         }
      }
   }
}