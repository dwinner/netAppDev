namespace MyExtendableApp
{
   partial class MainForm
   {
      /// <summary>
      /// Требуется переменная конструктора.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Освободить все используемые ресурсы.
      /// </summary>
      /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Код, автоматически созданный конструктором форм Windows

      /// <summary>
      /// Обязательный метод для поддержки конструктора - не изменяйте
      /// содержимое данного метода при помощи редактора кода.
      /// </summary>
      private void InitializeComponent()
      {
         this.snapInListBox = new System.Windows.Forms.ListBox();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.snapInModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // SnapInListBox
         // 
         this.snapInListBox.FormattingEnabled = true;
         this.snapInListBox.Location = new System.Drawing.Point(12, 34);
         this.snapInListBox.Name = "SnapInListBox";
         this.snapInListBox.Size = new System.Drawing.Size(260, 199);
         this.snapInListBox.TabIndex = 1;         
         // 
         // FileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snapInModuleToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "FileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // snapInModuleToolStripMenuItem
         // 
         this.snapInModuleToolStripMenuItem.Name = "snapInModuleToolStripMenuItem";
         this.snapInModuleToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
         this.snapInModuleToolStripMenuItem.Text = "Snap In Module";
         this.snapInModuleToolStripMenuItem.Click += new System.EventHandler(this.snapInModuleToolStripMenuItem_Click);
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(284, 24);
         this.menuStrip1.TabIndex = 0;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 262);
         this.Controls.Add(this.snapInListBox);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "MainForm";
         this.Text = "Extendable  App";
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      #endregion

      private System.Windows.Forms.ListBox snapInListBox;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem snapInModuleToolStripMenuItem;
      private System.Windows.Forms.MenuStrip menuStrip1;

   }
}

