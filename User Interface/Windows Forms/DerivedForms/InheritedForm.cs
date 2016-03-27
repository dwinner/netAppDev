using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DerivedForms
{
   public class InheritedForm : BaseForm
   {
      private readonly IContainer components = null;
      private CheckBox checkBox1;
      private ColumnHeader columnHeader1;
      private ColumnHeader columnHeader2;

      public InheritedForm()
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
         columnHeader1 = new ColumnHeader();
         columnHeader2 = new ColumnHeader();
         checkBox1 = new CheckBox();
         SuspendLayout();
         // listView1
         listView1.Columns.AddRange(new[]
         {
            columnHeader1,
            columnHeader2
         });
         listView1.Location = new Point(15, 57);
         listView1.View = View.Details;
         // columnHeader1
         columnHeader1.Text = "Items";
         // columnHeader2
         columnHeader2.Text = "Attributes";
         // checkBox1
         checkBox1.AutoSize = true;
         checkBox1.Location = new Point(19, 126);
         checkBox1.Name = "checkBox1";
         checkBox1.Size = new Size(107, 17);
         checkBox1.TabIndex = 4;
         checkBox1.Text = "Added checkbox";
         checkBox1.UseVisualStyleBackColor = true;
         // InheritedForm
         AutoScaleDimensions = new SizeF(6F, 13F);
         ClientSize = new Size(243, 149);
         Controls.Add(checkBox1);
         Name = "InheritedForm";
         Text = "InheritedForm";
         Controls.SetChildIndex(checkBox1, 0);
         Controls.SetChildIndex(listView1, 0);
         ResumeLayout(false);
         PerformLayout();
      }
   }
}