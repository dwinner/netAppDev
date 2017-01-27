using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Styles
{   
   public partial class EventSetter
   {
      public EventSetter()
      {
         InitializeComponent();
      }

      private void OnMouseEnter(object sender, MouseEventArgs e)
      {
         ((TextBlock)sender).Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
      }
      private void OnMouseLeave(object sender, MouseEventArgs e)
      {
         ((TextBlock)sender).Background = null;
      }
   }
}