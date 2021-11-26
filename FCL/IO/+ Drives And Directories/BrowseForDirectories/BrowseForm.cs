using System;
using System.Windows.Forms;

namespace BrowseForDirectories
{
   public partial class BrowseForm : Form
   {
      public BrowseForm()
      {
         InitializeComponent();
      }

      private void buttonBrowse_Click(object sender, EventArgs e)
      {
         FolderBrowserDialog browserDialog = new FolderBrowserDialog();
         if (browserDialog.ShowDialog() == DialogResult.OK)
         {
            filenameTextBox.Text = browserDialog.SelectedPath;
         }
      }
   }
}
