using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControls
{
   /// <summary>
   ///    Расширение существующего элемента управления
   /// </summary>
   public class MaskedTextBox : TextBox
   {
      public static readonly DependencyProperty MaskProperty;

      static MaskedTextBox()
      {
         MaskProperty = DependencyProperty.Register("Mask", typeof (string), typeof (MaskedTextBox),
            new FrameworkPropertyMetadata(MaskChanged));
         var metadata = new FrameworkPropertyMetadata { CoerceValueCallback = CoerceText };
         TextProperty.OverrideMetadata(typeof (MaskedTextBox), metadata);
         CommandManager.RegisterClassCommandBinding(typeof (MaskedTextBox), new CommandBinding(ApplicationCommands.Paste, null));
      }

      public MaskedTextBox()
      {
         var commandBinding1 = new CommandBinding(ApplicationCommands.Paste, null, SuppressCommand);
         CommandBindings.Add(commandBinding1);
         var commandBinding2 = new CommandBinding(ApplicationCommands.Cut, null, SuppressCommand);
         CommandBindings.Add(commandBinding2);
      }

      public string Mask
      {
         get { return (string) GetValue(MaskProperty); }
         set { SetValue(MaskProperty, value); }
      }

      public bool MaskCompleted
      {
         get
         {
            var maskProvider = GetMaskProvider();
            return maskProvider.MaskCompleted;
         }
      }

      static void MaskChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var textBox = (MaskedTextBox) d;
         d.CoerceValue(TextProperty);
      }

      static object CoerceText(DependencyObject d, object value)
      {
         var textBox = (MaskedTextBox) d;
         var maskProvider = new MaskedTextProvider(textBox.Mask);
         maskProvider.Set((string) value);
         return maskProvider.ToDisplayString();
      }

      void SuppressCommand(object sender, CanExecuteRoutedEventArgs e)
      {
         e.CanExecute = false;
         e.Handled = true;
      }

      protected override void OnPreviewKeyDown(KeyEventArgs e)
      {
         OnKeyDown(e);

         var maskProvider = GetMaskProvider();
         var pos = SelectionStart;

         // Deleting a character (Delete key).
         // Currently this does nothing if you try to delete
         // a format character (unliked MaskedTextBox, which
         // deletes the next input character).
         // Could use our private SkipToEditableCharacter
         // method to change this behavior.
         if (e.Key == Key.Delete && pos < Text.Length)
         {
            if (maskProvider.RemoveAt(pos))
            {
               RefreshText(maskProvider, pos);
            }

            e.Handled = true;
         }

         // Deleting a character (backspace).
         // Currently this steps over a format character
         // (unliked MaskedTextBox, which steps over and
         // deletes the next input character).
         // Could use our private SkipToEditableCharacter
         // method to change this behavior.
         else if (e.Key == Key.Back)
         {
            if (pos > 0)
            {
               pos--;
               if (maskProvider.RemoveAt(pos))
               {
                  RefreshText(maskProvider, pos);
               }
            }

            e.Handled = true;
         }
      }

      protected override void OnPreviewTextInput(TextCompositionEventArgs e)
      {
         var maskProvider = GetMaskProvider();
         var pos = SelectionStart;

         // Adding a character.
         if (pos < Text.Length)
         {
            pos = SkipToEditableCharacter(pos);

            // Overwrite mode is on.
            if (Keyboard.IsKeyToggled(Key.Insert))
            {
               if (maskProvider.Replace(e.Text, pos))
               {
                  pos++;
               }
            }
            // Insert mode is on.
            else
            {
               if (maskProvider.InsertAt(e.Text, pos))
               {
                  pos++;
               }
            }

            // Find the new cursor position.
            pos = SkipToEditableCharacter(pos);
         }

         RefreshText(maskProvider, pos);
         e.Handled = true;

         base.OnPreviewTextInput(e);
      }

      void RefreshText(MaskedTextProvider maskProvider, int pos)
      {
         // Refresh string.            
         Text = maskProvider.ToDisplayString();

         // Position cursor.
         SelectionStart = pos;
      }

      MaskedTextProvider GetMaskProvider()
      {
         var maskProvider = new MaskedTextProvider(Mask);
         maskProvider.Set(Text);
         return maskProvider;
      }

      // Finds the next non-mask character.
      int SkipToEditableCharacter(int startPos)
      {
         var maskProvider = GetMaskProvider();
         var newPos = maskProvider.FindEditPositionFrom(startPos, true);
         return newPos == -1 ? startPos : newPos;
      }
   }
}