using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DataBinding
{   
   public partial class DirectoryTreeView
   {
      public DirectoryTreeView()
      {
         InitializeComponent();
         BuildTree();
      }

      private void BuildTree()
      {
         FileSystemTreeView.Items.Clear();
         var driveItems = DriveInfo.GetDrives().Select(BuildTreeItem).ToList();
         driveItems.ForEach(item => FileSystemTreeView.Items.Add(item));
      }

      private static TreeViewItem BuildTreeItem(DriveInfo driveInfo)
      {
         var item = new TreeViewItem
         {
            Tag = driveInfo,
            Header = driveInfo.ToString()
         };
         item.Items.Add("*"); // This placeholder string is never shown, because the node begins in collapsed state.

         return item;
      }

      private void OnExpanded(object sender, RoutedEventArgs e)
      {
         var item = (TreeViewItem) e.OriginalSource;

         // Perform a refresh every time item is expanded.
         // Optionally, you could perform this only the first
         // time, when the placeholder is found (less refreshes),
         // every time an item is selected (more refreshes)
         // or when a message is received by the FileSystemWatcher
         // (intelligent refreshes, requiring the most overhead).
         item.Items.Clear();

         DirectoryInfo dir;
         var tag = item.Tag as DriveInfo;
         if (tag != null)
         {
            var drive = tag;
            dir = drive.RootDirectory;
         }
         else
         {
            dir = (DirectoryInfo) item.Tag;
         }

         try
         {
            foreach (var subDir in dir.GetDirectories())
            {
               var newItem = new TreeViewItem
               {
                  Tag = subDir,
                  Header = subDir.ToString()
               };
               newItem.Items.Add("*");
               item.Items.Add(newItem);
            }
         }
         catch
         {
            // An exception could be thrown in this code if you don't
            // have sufficient security permissions for a file or directory.
            // You can catch and then ignore this exception.
         }
      }
   }
}