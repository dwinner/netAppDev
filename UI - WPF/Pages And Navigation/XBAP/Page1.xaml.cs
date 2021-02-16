using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security;
using System.Security.Permissions;
using System.Windows;

namespace XBAP
{
   public partial class Page1
   {
      public Page1()
      {
         InitializeComponent();
      }

      private void OnWrite(object sender, RoutedEventArgs e)
      {
         File.WriteAllText("c:\\test.txt", "This isn't allowed.");
      }

      private void OnSafelyWrite(object sender, RoutedEventArgs e)
      {
         const string content = "This is a test";

         // Create a permission that represents writing to a file.
         const string filePath = "c:\\highscores.txt";
         var permission = new FileIOPermission(FileIOPermissionAccess.Write, filePath);

         // Check for this permission.
         if (CheckPermission(permission))
            try
            {
               using (var fs = File.Create(filePath))
               {
                  WriteHighScores(fs, content);
               }
            }
            catch (Exception err)
            {
               MessageBox.Show(err.Message);
            }
         else
            try
            {
               var store =
                  IsolatedStorageFile.GetUserStoreForApplication();
               using (var fs = new IsolatedStorageFileStream("highscores.txt", FileMode.Create, store))
               {
                  WriteHighScores(fs, content);
               }
            }
            catch (Exception err)
            {
               MessageBox.Show(err.Message);
            }
      }

      private void OnSafelyRead(object sender, RoutedEventArgs e)
      {
         var content = string.Empty;

         // Create a permission that represents writing to a file.
         const string filePath = "c:\\highscores.txt";
         var permission = new FileIOPermission(FileIOPermissionAccess.Read, filePath);

         // Check for this permission.
         if (CheckPermission(permission))
            try
            {
               using (var fs = File.Open(filePath, FileMode.Open))
               {
                  content = ReadHighScores(fs);
               }
            }
            catch (Exception err)
            {
               MessageBox.Show(err.Message);
            }
         else
            try
            {
               var store = IsolatedStorageFile.GetUserStoreForApplication();
               using (var fs = new IsolatedStorageFileStream("highscores.txt", FileMode.Open, store))
               {
                  content = ReadHighScores(fs);
               }
            }
            catch (Exception err)
            {
               MessageBox.Show(err.Message);
            }

         if (content != string.Empty)
            MessageBox.Show(content);
      }

      // A better implementation would cache this information over the lifetime of the application,
      // so the permission only needs to be evaluated once.
      private static bool CheckPermission(IStackWalk requestedPermission)
      {
         try
         {
            // Try to get this permission.
            requestedPermission.Demand();
            return true;
         }
         catch
         {
            return false;
         }
      }

      private static void WriteHighScores(Stream fileStream, string content)
      {
         using (var writer = new StreamWriter(fileStream))
         {
            writer.WriteLine(content);
         }
      }

      private static string ReadHighScores(Stream fileStream)
      {
         string content;
         using (var reader = new StreamReader(fileStream))
         {
            content = reader.ReadToEnd();
         }

         return content;
      }
   }
}