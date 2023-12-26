using System;
using System.IO;
using System.Windows.Forms;

namespace FileProperties
{
   public partial class Form1 : Form
   {
      private string _currentFolderPath;

      public Form1()
      {
         InitializeComponent();
      }

      protected void ClearAllFields()
      {
         FoldersListBox.Items.Clear();
         FilesListBox.Items.Clear();
         FolderTextBox.Text = "";
         FileNameTextBox.Text = "";
         CreationTimeTextBox.Text = "";
         LastAccessTimeTextBox.Text = "";
         LastWriteTimeTextBox.Text = "";
         FileSizeTextBox.Text = "";
      }

      protected void DisplayFileInfo(string fileFullName)
      {
         var theFile = new FileInfo(fileFullName);
         if (!theFile.Exists) // Файл не найден
         {
            throw new FileNotFoundException(string.Format("File not found: {0}", fileFullName));
         }

         FileNameTextBox.Text = theFile.Name;
         CreationTimeTextBox.Text = theFile.CreationTime.ToLongTimeString();
         LastAccessTimeTextBox.Text = theFile.LastAccessTime.ToLongDateString();
         LastWriteTimeTextBox.Text = theFile.LastWriteTime.ToLongDateString();
         FileSizeTextBox.Text = string.Format("{0} bytes", theFile.Length);
      }

      protected void DisplayFolderList(string folderFullName)
      {
         var theFolder = new DirectoryInfo(folderFullName);
         if (!theFolder.Exists)  // Папка не найдена
         {
            throw new DirectoryNotFoundException(string.Format("Folder not found: {0}", folderFullName));
         }

         ClearAllFields();
         FolderTextBox.Text = theFolder.FullName;
         _currentFolderPath = theFolder.FullName;

         // Отображение списка всех подпапок, имеющихся внутри данной папки
         foreach (DirectoryInfo nextFolder in theFolder.GetDirectories())
         {
            FoldersListBox.Items.Add(nextFolder.Name);
         }

         // Отображение списка всех файлов, имеющихся внутри данной папки
         foreach (FileInfo nextFile in theFolder.GetFiles())
         {
            FilesListBox.Items.Add(nextFile.Name);
         }
      }

      protected void OnDisplayButtonClick(object sender, EventArgs e)
      {
         try
         {
            string folderPath = InputTextBox.Text;
            var theFolder = new DirectoryInfo(folderPath);
            if (theFolder.Exists)
            {
               DisplayFolderList(theFolder.FullName);
               return;
            }

            var theFile = new FileInfo(folderPath);
            if (theFile.Exists)
            {
               if (theFile.Directory != null)
               {
                  DisplayFolderList(theFile.Directory.FullName);
               }

               int index = FilesListBox.Items.IndexOf(theFile.Name);
               FilesListBox.SetSelected(index, true);
               return;
            }

            throw new FileNotFoundException(string.Format("There is no file or folder with this name: {0}",
               InputTextBox.Text));
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }

      protected void OnListBoxFilesSelected(object sender, EventArgs e)
      {
         try
         {
            string selectedString = FilesListBox.SelectedItem.ToString();
            string fullFileName = Path.Combine(_currentFolderPath, selectedString);
            DisplayFileInfo(fullFileName);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }

      protected void OnListBoxFoldersSelected(object sender, EventArgs e)
      {
         try
         {
            string selectedString = FoldersListBox.SelectedItem.ToString();
            string fullPathName = Path.Combine(_currentFolderPath, selectedString);
            DisplayFolderList(fullPathName);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }

      protected void OnUpButtonClick(object sender, EventArgs e)
      {
         try
         {
            string folderPath = new FileInfo(_currentFolderPath).DirectoryName;
            DisplayFolderList(folderPath);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }
   }
}