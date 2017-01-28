using System;

namespace RoutedEvents
{
   public partial class Focus
   {
      public Focus()
      {
         InitializeComponent();
      }

      protected override void OnActivated(EventArgs e)
      {
         base.OnActivated(e);
         FocusCmd.Focus();
      }
   }
}