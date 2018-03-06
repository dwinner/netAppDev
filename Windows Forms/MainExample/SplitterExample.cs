using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MainExample.Properties;

namespace MainExample
{
   public partial class SplitterExample : Form
   {
      public SplitterExample()
      {
         InitializeComponent();
         var directoryInfo = new DirectoryInfo(@"C:\");
         var root = new TreeNode(directoryInfo.Name) { Tag = directoryInfo };
         treeView1.Nodes.Add(root);
      }

      private void OnFolderSelected(object sender, TreeViewEventArgs treeViewEventArgs)
      {
         var directoryInfo = treeViewEventArgs.Node.Tag as DirectoryInfo;
         if (null == directoryInfo)
            return;

         try
         {
            listView1.BeginUpdate();
            treeView1.BeginUpdate();
            listView1.Items.Clear();

            var directoryGroup = listView1.Groups.Add("Directories", "Directories");
            var fileGroup = listView1.Groups.Add("Files", "Files");
            var addToTree = (0 == treeViewEventArgs.Node.Nodes.Count);

            foreach (var dirInfo in directoryInfo.GetDirectories())
            {
               var viewItem = new ListViewItem(dirInfo.Name) { Tag = dirInfo, Group = directoryGroup };
               listView1.Items.Add(viewItem);
               if (!addToTree)
                  continue;

               var node = new TreeNode(dirInfo.Name) { Tag = dirInfo };
               treeViewEventArgs.Node.Nodes.Add(node);
            }

            foreach (
               var item in
                  directoryInfo.GetFiles()
                     .Select(fileInfo => new ListViewItem(fileInfo.Name) { Group = fileGroup, Tag = fileInfo }))
            {
               listView1.Items.Add(item);
            }
         }
         catch (UnauthorizedAccessException ex)
         {
            MessageBox.Show(
               string.Format("You don't appear to have access to the requested directory or files - {0}", ex),
               Resources.Oops);
         }
         finally
         {
            listView1.EndUpdate();
            treeView1.EndUpdate();
         }
      }
   }
}