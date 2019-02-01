using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ToolAndStatusAndSplit
{
   public class Form1 : Form
   {      
      private readonly IContainer components = null;

      private ToolStripMenuItem _copyToolStripMenuItem;
      private ToolStripMenuItem editToolStripMenuItem;
      private ToolStripMenuItem exitToolStripMenuItem;
      private ToolStripMenuItem fileToolStripMenuItem;
      private MenuStrip menuStrip;
      private SplitContainer splitContainer;
      private StatusStrip statusStrip;
      private TextBox textView;
      private ToolStrip toolStrip;
      private ToolStripButton toolStripButtonExit;
      private ToolStripComboBox toolStripComboBoxDrives;
      private ToolStripContainer toolStripContainer;
      private ToolStripProgressBar toolStripProgressBar;
      private ToolStripSeparator toolStripSeparator1;
      private ToolStripSplitButton toolStripSplitButton1;
      private ToolStripStatusLabel toolStripStatusLabel;
      private ToolStripStatusLabel toolStripStatusLabel1;
      private TreeView treeView;

      public Form1()
      {
         InitializeComponent();
      }

      /// <summary>
      ///    Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region initialization

      /// <summary>
      ///    Required method for Designer support - do not modify
      ///    the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
         this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
         this.statusStrip = new System.Windows.Forms.StatusStrip();
         this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
         this.splitContainer = new System.Windows.Forms.SplitContainer();
         this.treeView = new System.Windows.Forms.TreeView();
         this.textView = new System.Windows.Forms.TextBox();
         this.menuStrip = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this._copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStrip = new System.Windows.Forms.ToolStrip();
         this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
         this.toolStripComboBoxDrives = new System.Windows.Forms.ToolStripComboBox();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
         this.toolStripContainer.ContentPanel.SuspendLayout();
         this.toolStripContainer.TopToolStripPanel.SuspendLayout();
         this.toolStripContainer.SuspendLayout();
         this.statusStrip.SuspendLayout();
         this.splitContainer.Panel1.SuspendLayout();
         this.splitContainer.Panel2.SuspendLayout();
         this.splitContainer.SuspendLayout();
         this.menuStrip.SuspendLayout();
         this.toolStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripContainer
         // 
         // 
         // toolStripContainer.BottomToolStripPanel
         // 
         this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
         // 
         // toolStripContainer.ContentPanel
         // 
         this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
         this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(840, 369);
         this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
         this.toolStripContainer.Name = "toolStripContainer";
         this.toolStripContainer.Size = new System.Drawing.Size(840, 440);
         this.toolStripContainer.TabIndex = 0;
         // 
         // toolStripContainer.TopToolStripPanel
         // 
         this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
         this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
         // 
         // statusStrip
         // 
         this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
         this.statusStrip.Location = new System.Drawing.Point(0, 0);
         this.statusStrip.Name = "statusStrip";
         this.statusStrip.Size = new System.Drawing.Size(840, 22);
         this.statusStrip.TabIndex = 0;
         this.statusStrip.Text = "statusStrip";
         // 
         // toolStripStatusLabel
         // 
         this.toolStripStatusLabel.Name = "toolStripStatusLabel";
         this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
         this.toolStripStatusLabel.Text = "Ready";
         // 
         // toolStripProgressBar
         // 
         this.toolStripProgressBar.Name = "toolStripProgressBar";
         this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
         this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
         // 
         // splitContainer
         // 
         this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer.Location = new System.Drawing.Point(0, 0);
         this.splitContainer.Name = "splitContainer";
         // 
         // splitContainer.Panel1
         // 
         this.splitContainer.Panel1.Controls.Add(this.treeView);
         // 
         // splitContainer.Panel2
         // 
         this.splitContainer.Panel2.Controls.Add(this.textView);
         this.splitContainer.Size = new System.Drawing.Size(840, 369);
         this.splitContainer.SplitterDistance = 280;
         this.splitContainer.TabIndex = 2;
         // 
         // treeView
         // 
         this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.treeView.Location = new System.Drawing.Point(0, 0);
         this.treeView.Name = "treeView";
         this.treeView.Size = new System.Drawing.Size(280, 369);
         this.treeView.TabIndex = 0;
         // 
         // textView
         // 
         this.textView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textView.Location = new System.Drawing.Point(0, 0);
         this.textView.Multiline = true;
         this.textView.Name = "textView";
         this.textView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.textView.Size = new System.Drawing.Size(556, 369);
         this.textView.TabIndex = 0;
         // 
         // menuStrip
         // 
         this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
         this.menuStrip.Location = new System.Drawing.Point(0, 0);
         this.menuStrip.Name = "menuStrip";
         this.menuStrip.Size = new System.Drawing.Size(840, 24);
         this.menuStrip.TabIndex = 0;
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.exitToolStripMenuItem.Text = "E&xit";
         // 
         // editToolStripMenuItem
         // 
         this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._copyToolStripMenuItem});
         this.editToolStripMenuItem.Name = "editToolStripMenuItem";
         this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
         this.editToolStripMenuItem.Text = "&Edit";
         // 
         // _copyToolStripMenuItem
         // 
         this._copyToolStripMenuItem.Name = "_copyToolStripMenuItem";
         this._copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this._copyToolStripMenuItem.Text = "&Copy";
         // 
         // toolStrip
         // 
         this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExit,
            this.toolStripComboBoxDrives});
         this.toolStrip.Location = new System.Drawing.Point(3, 24);
         this.toolStrip.Name = "toolStrip";
         this.toolStrip.Size = new System.Drawing.Size(158, 25);
         this.toolStrip.TabIndex = 1;
         // 
         // toolStripButtonExit
         // 
         this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExit.Image")));
         this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButtonExit.Name = "toolStripButtonExit";
         this.toolStripButtonExit.Size = new System.Drawing.Size(23, 22);
         this.toolStripButtonExit.Text = "Exit";
         // 
         // toolStripComboBoxDrives
         // 
         this.toolStripComboBoxDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.toolStripComboBoxDrives.Name = "toolStripComboBoxDrives";
         this.toolStripComboBoxDrives.Size = new System.Drawing.Size(121, 25);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // Form1
         // 
         this.ClientSize = new System.Drawing.Size(840, 440);
         this.Controls.Add(this.toolStripContainer);
         this.Name = "Form1";
         this.Text = "Menu, tool, status, and split demo";
         this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
         this.toolStripContainer.BottomToolStripPanel.PerformLayout();
         this.toolStripContainer.ContentPanel.ResumeLayout(false);
         this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
         this.toolStripContainer.TopToolStripPanel.PerformLayout();
         this.toolStripContainer.ResumeLayout(false);
         this.toolStripContainer.PerformLayout();
         this.statusStrip.ResumeLayout(false);
         this.statusStrip.PerformLayout();
         this.splitContainer.Panel1.ResumeLayout(false);
         this.splitContainer.Panel2.ResumeLayout(false);
         this.splitContainer.Panel2.PerformLayout();
         this.splitContainer.ResumeLayout(false);
         this.menuStrip.ResumeLayout(false);
         this.menuStrip.PerformLayout();
         this.toolStrip.ResumeLayout(false);
         this.toolStrip.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      protected override void OnLoad(EventArgs e)
      {
         base.OnLoad(e);

         FillDriveCombo();
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Application.Exit();
      }

      private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
      {
         _copyToolStripMenuItem.Enabled = (textView.SelectionLength > 0);
      }

      private void copyToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (textView.SelectionLength > 0)
         {
            textView.Copy();
         }
      }

      private void toolStripButtonExit_Click(object sender, EventArgs e)
      {
         Application.Exit();
      }

      #region Combo-box related

      private void FillDriveCombo()
      {
         toolStripComboBoxDrives.Items.Clear();
         var drives = DriveInfo.GetDrives();
         foreach (var info in drives)
         {
            var name = string.Format("{0} - {1}", info.Name, info.IsReady ? info.VolumeLabel : "<unknown>");
            toolStripComboBoxDrives.Items.Add(name);
         }
      }

      private void SelectSystemDrive()
      {
         var sysDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
         var index = toolStripComboBoxDrives.FindString(sysDrive);
         if (index >= 0)
         {
            toolStripComboBoxDrives.SelectedIndex = index;
         }
      }

      private void toolStripComboBoxDrives_SelectedIndexChanged(object sender, EventArgs e)
      {
         var thread = new Thread(FillFolders);
         thread.Start(GetSelectedDrive());
         thread.IsBackground = true;
         toolStripComboBoxDrives.Enabled = false;
      }

      private string GetSelectedDrive()
      {
         var parts = (toolStripComboBoxDrives.SelectedItem as string).Split(' ', '-');
         if (parts.Length > 0)
         {
            return parts[0];
         }
         return null;
      }

      #endregion

      #region Tree-related

      private void FillFolders(object param)
      {
         var drive = param as string;
         if (string.IsNullOrEmpty(drive))
            return;

         var root = new TreeNode(drive);
         if (treeView.InvokeRequired)
         {
            treeView.Invoke(new MethodInvoker(delegate
            {
               treeView.Nodes.Clear();
               toolStripProgressBar.Style = ProgressBarStyle.Marquee;
               this.toolStripStatusLabel.Text = "Reading folders...";
               treeView.Nodes.Add(root);
            }));
         }

         FillFolders(root, drive, 1);
         if (treeView.InvokeRequired)
         {
            treeView.Invoke(new MethodInvoker(delegate
            {
               root.Expand();
               toolStripComboBoxDrives.Enabled = true;
               toolStripProgressBar.Style = ProgressBarStyle.Continuous;
               toolStripStatusLabel.Text = "Ready";
            }));
         }
      }

      private const int MaxDepth = 5;

      private void FillFolders(TreeNode parent, string path, int depth)
      {
         if (depth > MaxDepth)
            return;
         try
         {
            var directories = Directory.GetDirectories(path);
            foreach (var dir in directories)
            {
               var node = new TreeNode(Path.GetFileName(dir));
               node.Tag = dir;
               if (treeView.InvokeRequired)
               {
                  treeView.Invoke(new MethodInvoker(
                     delegate { parent.Nodes.Add(node); }));
               }

               FillFolders(node, dir, depth + 1);
            }
         }
         catch (UnauthorizedAccessException)
         {
         }
         catch (IOException)
         {
         }
      }

      private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
      {
         var fullPath = e.Node.Tag as string;

         if (!string.IsNullOrEmpty(fullPath))
         {
            textView.Clear();
            try
            {
               var files = Directory.GetFiles(fullPath);
               foreach (var file in files)
               {
                  var info = new FileInfo(file);
                  var line = string.Format("{0}\t\t{1:N0}KB\t\t{2}", Path.GetFileName(file), info.Length/1024,
                     info.LastWriteTime);
                  textView.AppendText(line + Environment.NewLine);
               }
            }
            catch (UnauthorizedAccessException ex)
            {
               textView.Text = ex.Message;
            }
         }
      }

      #endregion
   }
}