using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExtendedButton
{
   public class ImageButton : Button
   {
      static ImageButton() =>
         DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton),
            new FrameworkPropertyMetadata(typeof(ImageButton)));

      #region Properties

      public ImageSource Image
      {
         get => (ImageSource) GetValue(ImageProperty);
         set => SetValue(ImageProperty, value);
      }

      public static readonly DependencyProperty ImageProperty =
         DependencyProperty.Register(nameof(Image), typeof(ImageSource), typeof(ImageButton),
            new PropertyMetadata(null));

      public double ImageHeight
      {
         get => (double) GetValue(ImageHeightProperty);
         set => SetValue(ImageHeightProperty, value);
      }

      public static readonly DependencyProperty ImageHeightProperty =
         DependencyProperty.Register(nameof(ImageHeight), typeof(double), typeof(ImageButton),
            new PropertyMetadata(double.NaN));

      public double ImageWidth
      {
         get => (double) GetValue(ImageWidthProperty);
         set => SetValue(ImageWidthProperty, value);
      }

      public static readonly DependencyProperty ImageWidthProperty =
         DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(ImageButton),
            new PropertyMetadata(double.NaN));

      #endregion
   }
}