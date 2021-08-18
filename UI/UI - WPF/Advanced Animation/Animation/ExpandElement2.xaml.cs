using System;
using System.Windows;

namespace Animation
{
   public partial class ExpandElement2
   {
      public ExpandElement2()
      {
         InitializeComponent();
      }

      private void StoryboardCompleted(object sender, EventArgs e)
      {
         Rectangle.Visibility = Visibility.Collapsed;
      }
   }
}