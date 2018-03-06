using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DerivedForms
{
   public class BaseForm : Form
   {
      private readonly IContainer components = null;
      private Label label1;
      private Label label2;
      protected ListView listView1;
      private TextBox textBox1;

      public BaseForm()
      {
         InitializeComponent();
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
         var listViewItem3 = new ListViewItem("Item1");
         var listViewItem4 = new ListViewItem("Item2");
         label1 = new Label();
         textBox1 = new TextBox();
         listView1 = new ListView();
         label2 = new Label();
         SuspendLayout();
         // label1
         label1.AutoSize = true;
         label1.Location = new Point(13, 13);
         label1.Name = "label1";
         label1.Size = new Size(77, 13);
         label1.TabIndex = 0;
         label1.Text = "My Base Label";
         // textBox1
         textBox1.Location = new Point(96, 10);
         textBox1.Name = "textBox1";
         textBox1.Size = new Size(120, 20);
         textBox1.TabIndex = 1;
         textBox1.Text = "My Base Textbox";
         // listView1
         listView1.Items.AddRange(new[]
         {
            listViewItem3,
            listViewItem4
         });
         listView1.Location = new Point(16, 56);
         listView1.Name = "listView1";
         listView1.Size = new Size(215, 62);
         listView1.TabIndex = 2;
         listView1.UseCompatibleStateImageBehavior = false;
         listView1.View = View.List;
         // label2
         label2.AutoSize = true;
         label2.Location = new Point(16, 39);
         label2.Name = "label2";
         label2.Size = new Size(97, 13);
         label2.TabIndex = 3;
         label2.Text = "protected ListView:";
         // BaseForm
         AutoScaleDimensions = new SizeF(6F, 13F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(243, 130);
         Controls.Add(label2);
         Controls.Add(listView1);
         Controls.Add(textBox1);
         Controls.Add(label1);
         Name = "BaseForm";
         StartPosition = FormStartPosition.CenterParent;
         Text = "BaseForm";
         ResumeLayout(false);
         PerformLayout();
      }
   }
}