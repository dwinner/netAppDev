using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControls
{
   /// <summary>
   ///    Простейший способ создания элемента управления с внешним видом
   /// </summary>
   public partial class ColorPickerUserControl
   {
      #region Свойства зависимости

      public static readonly DependencyProperty ColorProperty;
      public static readonly DependencyProperty RedProperty;
      public static readonly DependencyProperty GreenProperty;
      public static readonly DependencyProperty BlueProperty;

      #endregion


      #region Маршрутизируемые события

      public static readonly RoutedEvent ColorChangedEvent =
         EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
            typeof (RoutedPropertyChangedEventHandler<Color>), typeof (ColorPickerUserControl));

      #endregion


      Color? previousColor;

      #region Конструкторы

      static ColorPickerUserControl()
      {
         ColorProperty = DependencyProperty.Register("Color", typeof (Color),
            typeof (ColorPickerUserControl),
            new FrameworkPropertyMetadata(Colors.Black, OnColorChanged));

         RedProperty = DependencyProperty.Register("Red", typeof (byte),
            typeof (ColorPickerUserControl),
            new FrameworkPropertyMetadata(OnColorRgbChanged));

         GreenProperty = DependencyProperty.Register("Green", typeof (byte),
            typeof (ColorPickerUserControl),
            new FrameworkPropertyMetadata(OnColorRgbChanged));

         BlueProperty = DependencyProperty.Register("Blue", typeof (byte),
            typeof (ColorPickerUserControl),
            new FrameworkPropertyMetadata(OnColorRgbChanged));

         CommandManager.RegisterClassCommandBinding(typeof (ColorPickerUserControl),
            new CommandBinding(ApplicationCommands.Undo,
               OnUndoCommandExecuted, OnUndoCommandCanExecute));
      }

      public ColorPickerUserControl()
      {
         InitializeComponent();
         //SetUpCommands();
      }

      #endregion


      #region Свойства

      public Color Color
      {
         get { return (Color) GetValue(ColorProperty); }
         set { SetValue(ColorProperty, value); }
      }

      public byte Red
      {
         get { return (byte) GetValue(RedProperty); }
         set { SetValue(RedProperty, value); }
      }

      public byte Green
      {
         get { return (byte) GetValue(GreenProperty); }
         set { SetValue(GreenProperty, value); }
      }

      public byte Blue
      {
         get { return (byte) GetValue(BlueProperty); }
         set { SetValue(BlueProperty, value); }
      }

      #endregion


/*
      void SetUpCommands()
      {
         // Set up command bindings.
         CommandBinding binding = new CommandBinding(ApplicationCommands.Undo,
          OnUndoCommandExecuted, OnUndoCommandCanExecute);
         CommandBindings.Add(binding);
      }
*/

      static void OnUndoCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
      {
         var colorPicker = (ColorPickerUserControl) sender;
         e.CanExecute = colorPicker.previousColor.HasValue;
      }

      static void OnUndoCommandExecuted(object sender, ExecutedRoutedEventArgs e)
      {
         // Use simple reverse-or-redo Undo (like Notepad).
         var colorPicker = (ColorPickerUserControl) sender;
         Debug.Assert(colorPicker.previousColor != null, "colorPicker.previousColor != null");
         colorPicker.Color = (Color) colorPicker.previousColor;
      }

      static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
      {
         var colorPicker = (ColorPickerUserControl) sender;

         var oldColor = (Color) e.OldValue;
         var newColor = (Color) e.NewValue;
         colorPicker.Red = newColor.R;
         colorPicker.Green = newColor.G;
         colorPicker.Blue = newColor.B;

         colorPicker.previousColor = oldColor;
         colorPicker.OnColorChanged(oldColor, newColor);
      }

      static void OnColorRgbChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
      {
         var colorPicker = (ColorPickerUserControl) sender;
         var color = colorPicker.Color;
         if (e.Property == RedProperty)
            color.R = (byte) e.NewValue;
         else if (e.Property == GreenProperty)
            color.G = (byte) e.NewValue;
         else if (e.Property == BlueProperty)
            color.B = (byte) e.NewValue;

         colorPicker.Color = color;
      }

      public event RoutedPropertyChangedEventHandler<Color> ColorChanged
      {
         add { AddHandler(ColorChangedEvent, value); }
         remove { RemoveHandler(ColorChangedEvent, value); }
      }

      void OnColorChanged(Color oldValue, Color newValue)
      {
         var args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue) { RoutedEvent = ColorChangedEvent };
         RaiseEvent(args);
      }
   }
}