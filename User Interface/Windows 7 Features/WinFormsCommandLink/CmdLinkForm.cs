using System.Windows.Forms;

namespace WinFormsCommandLink
{
   public partial class CmdLinkForm : Form
   {
      public CmdLinkForm()
      {
         InitializeComponent();
      }

      void commandLink_Click(object sender, System.EventArgs e)
      {
         MessageBox.Show("Ok");
      }
   }
}