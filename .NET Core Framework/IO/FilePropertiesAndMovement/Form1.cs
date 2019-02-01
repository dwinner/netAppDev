using System;
using System.IO;
using System.Windows.Forms;
using FilePropertiesAndMovement.Properties;

namespace FilePropertiesAndMovement
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
         listBoxFolders.Items.Clear();
         listBoxFiles.Items.Clear();
         textBoxFolder.Text = "";
         textBoxFolder.Text = "";
         textBoxFileName.Text = "";
         textBoxCreationTime.Text = "";
         textBoxLastAccessTime.Text = "";
         textBoxLastWriteTime.Text = "";
         textBoxFileSize.Text = "";
      }

      protected void DisplayFileInfo(string fileFullName)
      {
         var theFile = new FileInfo(fileFullName);
         if (!theFile.Exists)
         {
            throw new FileNotFoundException(string.Format("File not found: {0}", fileFullName));
         }

         textBoxFileName.Text = theFile.Name;
         textBoxCreationTime.Text = theFile.CreationTime.ToLongTimeString();
         textBoxLastAccessTime.Text = theFile.LastAccessTime.ToLongDateString();
         textBoxLastWriteTime.Text = theFile.LastWriteTime.ToLongDateString();
         textBoxFileSize.Text = string.Format("{0} bytes", theFile.Length);

         // Включение кнопок копирования, перемещения и удаления
         textBoxNewPath.Text = theFile.FullName;
         textBoxNewPath.Enabled = true;
         buttonCopyTo.Enabled = true;
         buttonDelete.Enabled = true;
         buttonMoveTo.Enabled = true;
      }

      protected void DisplayFolderList(string folderFullName)
      {
         var theFolder = new DirectoryInfo(folderFullName);
         if (!theFolder.Exists)
         {
            throw new DirectoryNotFoundException(string.Format("Folder not found: {0}", folderFullName));
         }

         ClearAllFields();
         DisableMoveFeatures();
         textBoxFolder.Text = theFolder.FullName;
         _currentFolderPath = theFolder.FullName;

         foreach (var nextFolder in theFolder.GetDirectories())
         {
            listBoxFolders.Items.Add(nextFolder.Name);
         }

         foreach (var nextFile in theFolder.GetFiles())
         {
            listBoxFiles.Items.Add(nextFile.Name);
         }
      }

      protected void OnDisplayButtonClick(object sender, EventArgs e)
      {
         try
         {
            string folderPath = textBoxInput.Text;
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

               int index = listBoxFiles.Items.IndexOf(theFile.Name);
               listBoxFiles.SetSelected(index, true);
               return;
            }

            throw new FileNotFoundException(string.Format("There is no file or folder with this name: {0}",
               textBoxInput.Text));
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }

      void DisableMoveFeatures()
      {
         textBoxNewPath.Text = "";
         textBoxNewPath.Enabled = false;
         buttonCopyTo.Enabled = false;
         buttonDelete.Enabled = false;
         buttonMoveTo.Enabled = false;
      }

      protected void OnListBoxFilesSelected(object sender, EventArgs e)
      {
         try
         {
            string selectedString = listBoxFiles.SelectedItem.ToString();
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
            string selectedString = listBoxFolders.SelectedItem.ToString();
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

      protected void OnDeleteButtonClick(object sender, EventArgs e)
      {
         try
         {
            string filePath = Path.Combine(_currentFolderPath, textBoxFileName.Text);
            string query = string.Format("Really delete the file\n{0}?", filePath);

            // Запрос на удаление файла
            if (MessageBox.Show(query, Resources.DeleteFileQuestion, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               File.Delete(filePath);
               DisplayFolderList(_currentFolderPath);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(string.Format("Unable to delete file. The following exception occurred:\n{0}", ex.Message),
               Resources.FailedMessage);
         }
      }

      protected void OnMoveButtonClick(object sender, EventArgs e)
      {
         try
         {
            string filePath = Path.Combine(_currentFolderPath, textBoxFileName.Text);
            string query = string.Format("Really move the file\n{0}\nto {1}?", filePath, textBoxNewPath.Text);

            // Запрос на перемещение файла
            if (MessageBox.Show(query, Resources.MoveFileQuestion, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               File.Move(filePath, textBoxNewPath.Text);
               DisplayFolderList(_currentFolderPath);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(string.Format("Unable to move file. The following exception occurred:\n{0}", ex.Message),
               Resources.FailedMessage);
         }
      }

      protected void OnCopyButtonClick(object sender, EventArgs e)
      {
         try
         {
            string filePath = Path.Combine(_currentFolderPath, textBoxFileName.Text);
            string query = string.Format("Really copy the file\n{0}\nto {1}?", filePath, textBoxNewPath.Text);

            // Запрос на копирование файла
            if (MessageBox.Show(query, Resources.CopyFileQuestion, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               File.Copy(filePath, textBoxNewPath.Text);
               DisplayFolderList(_currentFolderPath);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(string.Format("Unable to copy file. The following exception occurred:\n{0}", ex.Message),
               Resources.FailedMessage);
         }
      }
   }
}