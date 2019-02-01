using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NotifyIconDemo.Properties;

namespace NotifyIconDemo
{
   public class Form1 : Form
   {
      private IContainer components;
      private ToolStripMenuItem _exitToolStripMenuItem;
      private ContextMenuStrip _menu;
      private NotifyIcon _notifyIcon;
      private ToolStripMenuItem _sayHelloToolStripMenuItem;
      private ToolStripSeparator _toolStripSeparator1;

      public Form1()
      {
         InitializeComponent();
      }

      private void sayHelloToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoDefaultAction();
      }

      private void DoDefaultAction()
      {
         if (WindowState == FormWindowState.Minimized)
         {
            WindowState = FormWindowState.Normal;
         }
         MessageBox.Show(Resources.Hello);
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Close();
      }

      private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         DoDefaultAction();
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
         components = new Container();
         var resources = new ComponentResourceManager(typeof (Form1));
         _menu = new ContextMenuStrip(components);
         _notifyIcon = new NotifyIcon(components);
         _sayHelloToolStripMenuItem = new ToolStripMenuItem();
         _exitToolStripMenuItem = new ToolStripMenuItem();
         _toolStripSeparator1 = new ToolStripSeparator();
         _menu.SuspendLayout();
         SuspendLayout();
         // 
         // menu
         // 
         _menu.Items.AddRange(new ToolStripItem[]
         {
            _sayHelloToolStripMenuItem,
            _toolStripSeparator1,
            _exitToolStripMenuItem
         });
         _menu.Name = "_menu";
         _menu.Size = new Size(153, 76);
         // 
         // notifyIcon
         // 
         _notifyIcon.ContextMenuStrip = _menu;
         _notifyIcon.Icon = Resources.DemoIcon;
         _notifyIcon.Visible = true;
         _notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
         // 
         // sayHelloToolStripMenuItem
         // 
         _sayHelloToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
         _sayHelloToolStripMenuItem.Image = ((Image) (resources.GetObject("_sayHelloToolStripMenuItem.Image")));
         _sayHelloToolStripMenuItem.Name = "_sayHelloToolStripMenuItem";
         _sayHelloToolStripMenuItem.Size = new Size(152, 22);
         _sayHelloToolStripMenuItem.Text = Resources.SayHello;
         _sayHelloToolStripMenuItem.Click += sayHelloToolStripMenuItem_Click;
         // 
         // exitToolStripMenuItem
         // 
         _exitToolStripMenuItem.Name = "_exitToolStripMenuItem";
         _exitToolStripMenuItem.Size = new Size(152, 22);
         _exitToolStripMenuItem.Text = Resources.Exit;
         _exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
         // 
         // toolStripSeparator1
         // 
         _toolStripSeparator1.Name = "_toolStripSeparator1";
         _toolStripSeparator1.Size = new Size(149, 6);
         // 
         // Form1
         // 
         AutoScaleDimensions = new SizeF(6F, 13F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(399, 191);
         Name = "Form1";
         Text = Resources.NotifyIcon;
         _menu.ResumeLayout(false);
         ResumeLayout(false);
      }
   }
}