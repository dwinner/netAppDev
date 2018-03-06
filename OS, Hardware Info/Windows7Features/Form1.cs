using Microsoft.WindowsAPICodePack.ApplicationServices;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Windows7Features
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void buttonStdOFD_Click(object sender, EventArgs e)
      {
         var fileDialog = new OpenFileDialog();
         fileDialog.ShowDialog();
      }

      private void buttonWin7OFD_Click(object sender, EventArgs e)
      {
         var commonOpenFileDialog = new CommonOpenFileDialog
         {
            AddToMostRecentlyUsedList = true,
            IsFolderPicker = true,
            AllowNonFileSystemItems = true
         };
         // Позволяет обращаться к панели управления и библиотекам

         if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
         {
            var selectedFileNames = commonOpenFileDialog.FileNames.ToArray();
            if (selectedFileNames.Length > 0)
            {
               Array.ForEach(selectedFileNames, file => textBoxInfo.AppendText(string.Format("\t{0}", file)));
            }

            var shellObj = commonOpenFileDialog.FileAsShellObject;
            var library = shellObj as ShellLibrary;
            if (library != null)
            {
               textBoxInfo.AppendText(string.Format("You picked a library: {0}, Type: {1}", library.Name,
                  library.LibraryType));
               foreach (var folder in (ShellLibrary)shellObj)
                  textBoxInfo.AppendText("\t" + folder.Path);
            }

            textBoxInfo.AppendText(Environment.NewLine);
         }
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         GetPowerInfo();
      }

      private void GetPowerInfo()
      {
         labelPowerPersonality.Text = PowerManager.PowerPersonality.ToString();
         checkBoxBatteryPresent.Checked = PowerManager.IsBatteryPresent;
         checkBoxMonitorOn.Checked = PowerManager.IsMonitorOn;
         checkBoxUpsPresent.Checked = PowerManager.IsUpsPresent;
         labelPowerSource.Text = PowerManager.PowerSource.ToString();
      }
   }
}