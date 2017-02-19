using System.Windows;
using System.Windows.Controls.Primitives;

namespace PlayingCardSample
{
   public sealed class PlayingCardControl : ToggleButton
   {
      public static readonly DependencyProperty FaceProperty = DependencyProperty.Register(
         "Face", typeof(string), typeof(PlayingCardControl), new PropertyMetadata(default(string)));

      static PlayingCardControl()
      {
         DefaultStyleKeyProperty.OverrideMetadata(typeof(PlayingCardControl),
            new FrameworkPropertyMetadata(typeof(PlayingCardControl)));
      }

      public string Face
      {
         get { return (string) GetValue(FaceProperty); }
         set { SetValue(FaceProperty, value); }
      }
   }
}