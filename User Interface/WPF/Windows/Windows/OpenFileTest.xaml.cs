using System;
using System.Windows;
using Microsoft.Win32;

namespace Windows
{
   public partial class OpenFileTest
   {
      public OpenFileTest()
      {
         InitializeComponent();
      }

      private void OnOpenFiles(object sender, RoutedEventArgs e)
      {
         var myDialog = new OpenFileDialog
         {
            Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*",
            CheckFileExists = true,
            Multiselect = true
         };

         if (myDialog.ShowDialog() == true)
         {
            FileListBox.Items.Clear();
            Array.ForEach(myDialog.FileNames, file => FileListBox.Items.Add(file));            
         }
      }
   }
}