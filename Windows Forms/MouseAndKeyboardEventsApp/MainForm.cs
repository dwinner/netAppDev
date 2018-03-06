using System.Windows.Forms;
using MouseAndKeyboardEventsApp.Properties;

namespace MouseAndKeyboardEventsApp
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      private void MainForm_MouseMove(object sender, MouseEventArgs e)
      {
         Text = string.Format("Mouse position: {0}", e.Location); // Позиция мыши
      }

      private void MainForm_MouseDown(object sender, MouseEventArgs e)
      {
         switch (e.Button)
         {
            case MouseButtons.Left:
               MessageBox.Show(Resources.LeftClick);
               break;

            case MouseButtons.Right:
               MessageBox.Show(Resources.RightClick);
               break;

            case MouseButtons.Middle:
               MessageBox.Show(Resources.MiddleClick);
               break;
         }
      }

      private void MainForm_KeyDown(object sender, KeyEventArgs e)
      {
         Text = string.Format("Key pressed: {0}. Modifiers: {1}", e.KeyCode, e.Modifiers);
      }
   }
}