using System.Windows.Forms;

namespace SimpleWinFormsApp
{
   public class MainWindow : Form
   {
      private readonly ToolStripMenuItem _exitToolStripMenuItem = new ToolStripMenuItem("E&xit");
      private readonly ToolStripMenuItem _fileToolStripMenuItem = new ToolStripMenuItem("&File");
      private readonly MenuStrip _mainMenuStrip = new MenuStrip();

      public MainWindow()
      {
      }

      public MainWindow(string aTitle, int aWidth, int aHeight)
      {
         Text = aTitle;
         Width = aWidth;
         Height = aHeight;
         CenterToScreen();
         BuildMenuSystem();
      }

      public override sealed string Text
      {
         get { return base.Text; }
         set { base.Text = value; }
      }

      private void BuildMenuSystem()
      {
         _mainMenuStrip.Items.Add(_fileToolStripMenuItem);
         _fileToolStripMenuItem.DropDownItems.Add(_exitToolStripMenuItem);
         _exitToolStripMenuItem.Click += (sender, args) => Application.Exit();
         Controls.Add(_mainMenuStrip);
         MainMenuStrip = _mainMenuStrip;
      }
   }
}