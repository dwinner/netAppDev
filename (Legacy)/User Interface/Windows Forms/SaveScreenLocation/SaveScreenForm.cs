using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SaveScreenLocation
{
   public partial class SaveScreenForm : Form
   {
      private const int DefaultWidth = 300;
      private const int DefaultHeight = 300;

      public SaveScreenForm()
      {
         InitializeComponent();
      }

      protected override void OnLoad(EventArgs e)
      {
         base.OnLoad(e);
         RestoreLocation();
      }

      private void RestoreLocation()
      {
         Point location = Properties.Settings.Default.FormLocation;
         Size size = Properties.Settings.Default.FormSize;

         // Убедимся, что окно приложения находится в пределах монитора
         bool isOnScreen = false;
         foreach (Screen screen in Screen.AllScreens)
         {
            if (screen.WorkingArea.Contains(location))
            {
               isOnScreen = true;
            }
         }

         // Если окно не видно, поместить его на главный монитор
         if (!isOnScreen)
         {
            SetDesktopLocation(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Top);
         }

         // Если размер слишком мал, поместить его на главный монитор
         if (size.Width < 10 || size.Height < 10)
         {
            Size = new Size(DefaultWidth, DefaultHeight);
         }
      }

      private void SaveLocation()
      {
         Properties.Settings.Default.FormLocation = Location;
         Properties.Settings.Default.FormSize = Size;
         Properties.Settings.Default.Save();
      }

      protected override void OnClosing(CancelEventArgs e)
      {
         base.OnClosing(e);
         SaveLocation();
      }

      private void buttonMoveOff_Click(object sender, EventArgs e)
      {
         Location = new Point(10000, 10000);
         Close();
      }

      protected override void OnSizeChanged(EventArgs e)
      {
         base.OnSizeChanged(e);
         UpdateLocation();
      }

      protected override void OnMove(EventArgs e)
      {
         base.OnMove(e);
         UpdateLocation();
      }

      private void UpdateLocation()
      {
         textBoxLocation.Text = string.Format("{0}, {1}", Location.X, Location.Y);
         textBoxSize.Text = string.Format("{0}, {1}", Size.Width, Size.Height);
      }
   }
}
