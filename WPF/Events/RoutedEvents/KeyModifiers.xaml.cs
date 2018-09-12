using System;
using System.Windows;
using System.Windows.Input;

namespace RoutedEvents
{
   public partial class KeyModifiers
   {
      public KeyModifiers()
      {
         InitializeComponent();
      }

      private void KeyEvent(object sender, KeyEventArgs e)
      {
         InfoLabel.Text = string.Format("Modifiers: {0}", e.KeyboardDevice.Modifiers);
         if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
         {
            InfoLabel.Text += string.Format("{0}You held the Control key.", Environment.NewLine);
         }
      }

      private void CheckShift(object sender, RoutedEventArgs e)
      {
         InfoLabel.Text = Keyboard.IsKeyDown(Key.LeftShift)
            ? "The left Shift is held down."
            : "The left Shift is not held down.";
      }
   }
}