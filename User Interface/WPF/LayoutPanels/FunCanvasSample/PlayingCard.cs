using System.Windows;
using System.Windows.Controls.Primitives;

namespace FunCanvasSample
{
   public sealed class PlayingCard : ToggleButton
   {
      public static DependencyProperty FaceProperty;

      static PlayingCard()
      {
         // Override style
         DefaultStyleKeyProperty.OverrideMetadata(typeof(PlayingCard),
            new FrameworkPropertyMetadata(typeof(PlayingCard)));

         // Register Face dependency property
         FaceProperty = DependencyProperty.Register("Face",
            typeof(string), typeof(PlayingCard));
      }

      public string Face
      {
         get { return (string) GetValue(FaceProperty); }
         set { SetValue(FaceProperty, value); }
      }
   }
}