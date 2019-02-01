using System;
using System.IO;
using System.Windows.Forms;

namespace DriveViewer
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         foreach (var itemDrive in DriveInfo.GetDrives())
         {
            listBox1.Items.Add(itemDrive.Name);
         }
      }

      private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
      {
         var selectedDriveInfo = new DriveInfo(listBox1.SelectedItem.ToString());
         MessageBox.Show(
            string.Format(
               "Available Free Space: {0}\n" + "Drive Format: {1}\n" + "Drive Type: {2}\n" + "Is Ready: {3}\n" +
               "Name: {4}\n" + "Root Directory: {5}\n" + "ToString() Value: {6}\n" + "Total Free Space: {7}\n" +
               "Total Size: {8}\n" + "Volume Label: {9}", selectedDriveInfo.AvailableFreeSpace,
               selectedDriveInfo.DriveFormat, selectedDriveInfo.DriveType, selectedDriveInfo.IsReady,
               selectedDriveInfo.Name, selectedDriveInfo.RootDirectory, selectedDriveInfo,
               selectedDriveInfo.TotalFreeSpace, selectedDriveInfo.TotalSize, selectedDriveInfo.VolumeLabel),
            string.Format("{0} DRIVE INFO", selectedDriveInfo.Name));
      }
   }
}