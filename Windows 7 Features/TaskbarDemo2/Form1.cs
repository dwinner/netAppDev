using System;
using System.IO;
using System.Windows.Forms;

namespace TaskbarDemo
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
         //RegistrationHelper.RegisterFileAssociation("TaskbarDemo", "abc");
      }

      private void hscrTaskbarProgress_Scroll(object sender, ScrollEventArgs e)
      {
         TaskbarManager.Instance.SetProgressState((TaskbarProgressBarState)Enum.Parse(typeof(TaskbarProgressBarState), this.comboBox1.SelectedText));
         TaskbarManager.Instance.SetProgressValue(hscrTaskbarProgress.Value, hscrTaskbarProgress.Maximum);
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         var enums = Enum.GetNames(typeof(TaskbarProgressBarState));
         this.comboBox1.DataSource = enums;
      }

      private void Form1_Shown(object sender, EventArgs e)
      {
         TaskbarManager.Instance.SetProgressValue(0, hscrTaskbarProgress.Maximum);
         TaskbarManager.Instance.SetOverlayIcon(this.Icon, "Taskbar Demo Application");

         JumpList list = JumpList.CreateJumpList();//JumpList.CreateJumpListForIndividualWindow("TaskbarDemo.Form1", this.Handle);
         JumpListCustomCategory jcategory = new JumpListCustomCategory("My New Category");

         list.ClearAllUserTasks();
         string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
         //jcategory.AddJumpListItems(new JumpListItem(Path.Combine(desktop, "a.abc")));
         list.AddCustomCategories(jcategory);

         string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
         //Add links to Tasks
         list.AddUserTasks(new JumpListLink(Path.Combine(systemFolder, "notepad.exe"), "Open Notepad")
         {
            IconReference = new IconReference(Path.Combine(systemFolder, "notepad.exe"), 0)
         });
         list.AddUserTasks(new JumpListLink(Path.Combine(systemFolder, "calc.exe"), "Open Calculator")
         {
            IconReference = new IconReference(Path.Combine(systemFolder, "calc.exe"), 0)
         });
         list.AddUserTasks(new JumpListSeparator());
         list.AddUserTasks(new JumpListLink(Path.Combine(systemFolder, "mspaint.exe"), "Open Paint")
         {
            IconReference = new IconReference(Path.Combine(systemFolder, "mspaint.exe"), 0)
         });
         //Adding links to RecentItems
         //list.AddToRecent(Path.Combine(systemFolder, "mspaint.exe"));
         list.Refresh();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         Thumbnail th = new Thumbnail();
         th.Show();
      }
   }
}
