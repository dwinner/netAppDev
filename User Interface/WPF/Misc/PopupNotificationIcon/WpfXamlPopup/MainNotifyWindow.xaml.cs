using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using ExtendedWindowsControls;

namespace WpfXamlPopup
{
   public partial class MainNotifyWindow
   {
      private readonly ExtendedNotifyIcon _extendedNotifyIcon;         
      private readonly Storyboard _gridFadeInStoryBoard;
      private readonly Storyboard _gridFadeOutStoryBoard;
      
      public MainNotifyWindow()
      {
         _extendedNotifyIcon = new ExtendedNotifyIcon();
         _extendedNotifyIcon.MouseLeave += OnHideWindow;
         _extendedNotifyIcon.MouseMove += OnShowWindow;
         _extendedNotifyIcon.TargetNotifyIcon.Text = "Popup Text";
         SetNotifyIcon("Red");

         InitializeComponent();
         
         SetWindowToBottomRightOfScreen();
         Opacity = 0;
         UiGridMain.Opacity = 0;

         _gridFadeOutStoryBoard = (Storyboard) TryFindResource("GridFadeOutStoryBoard");
         _gridFadeOutStoryBoard.Completed += OnFadeOutStoryBoardCompleted;
         _gridFadeInStoryBoard = (Storyboard) TryFindResource("GridFadeInStoryBoard");
         _gridFadeInStoryBoard.Completed += OnFadeInStoryBoardCompleted;
      }
      
      private void SetNotifyIcon(string iconPrefix)
      {
         var streamResourceInfo =
            Application.GetResourceStream(new Uri(string.Format("pack://application:,,/Images/{0}Orb.ico", iconPrefix)));
         if (streamResourceInfo != null)
         {
            var iconStream = streamResourceInfo.Stream;
            _extendedNotifyIcon.TargetNotifyIcon.Icon = new Icon(iconStream);
         }
      }
      
      private void SetWindowToBottomRightOfScreen()
      {
         Left = SystemParameters.WorkArea.Width - Width - 10;
         Top = SystemParameters.WorkArea.Height - Height;
      }

      private void OnShowWindow()
      {
         _gridFadeOutStoryBoard.Stop();
         Opacity = 1;
         Topmost = true;            
         if (UiGridMain.Opacity > 0 && UiGridMain.Opacity < 1)            
            UiGridMain.Opacity = 1;
         else if (Math.Abs(UiGridMain.Opacity) < double.Epsilon)
            _gridFadeInStoryBoard.Begin();
      }
      
      private void OnHideWindow()
      {
         if (PinButton.IsChecked == true)
            return;

         _gridFadeInStoryBoard.Stop();
         if (Math.Abs(UiGridMain.Opacity - 1) < double.Epsilon && Math.Abs(Opacity - 1) < double.Epsilon)
         {
            _gridFadeOutStoryBoard.Begin();
         }
         else
         {
            UiGridMain.Opacity = 0;
            Opacity = 0;
         }
      }
      
      private void uiWindowMainNotification_MouseEnter(object sender, MouseEventArgs e)
      {         
         _extendedNotifyIcon.StopMouseLeaveEventFromFiring();
         _gridFadeOutStoryBoard.Stop();
         UiGridMain.Opacity = 1;
      }
      
      private void OnMainNotificationMouseLeave(object sender, MouseEventArgs e)
      {
         OnHideWindow();
      }
      
      private void OnFadeOutStoryBoardCompleted(object sender, EventArgs e)
      {
         Opacity = 0;
      }
      
      private void OnFadeInStoryBoardCompleted(object sender, EventArgs e)
      {
         Opacity = 1;
      }
      
      private void PinButton_Click(object sender, RoutedEventArgs e)
      {
         PinImage.Source = PinButton.IsChecked == true
            ? new BitmapImage(new Uri("pack://application:,,/Images/Pinned.png"))
            : new BitmapImage(new Uri("pack://application:,,/Images/Un-Pinned.png"));
      }

      private void OnColorRadioButtonClick(object sender, RoutedEventArgs e)
      {
         SetNotifyIcon(((RadioButton) sender).Tag.ToString());
      }
      
      private void OnClose(object sender, RoutedEventArgs e)
      {
         _extendedNotifyIcon.Dispose();
         Close();
      }
   }
}