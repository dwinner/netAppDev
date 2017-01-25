using System;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

namespace Windows7_TaskBar
{
   public partial class ThumbnailButtons
   {
      public ThumbnailButtons()
      {
         InitializeComponent();
      }

      private void OnPlay(object sender, EventArgs e)
      {
         TaskBarItem.ProgressState = TaskbarItemProgressState.Indeterminate;
         TaskBarItem.Overlay = new BitmapImage(new Uri("pack://application:,,,/play.png"));
      }

      private void OnPause(object sender, EventArgs e)
      {
         TaskBarItem.ProgressState = TaskbarItemProgressState.None;
         TaskBarItem.Overlay = new BitmapImage(new Uri("pack://application:,,,/pause.png"));
      }
   }
}