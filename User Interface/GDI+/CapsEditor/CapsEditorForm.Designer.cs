namespace CapsEditor
{
   partial class CapsEditorForm
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
         this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
         this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.PrintPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.MainMenuStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // MainMenuStrip
         // 
         this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
         this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
         this.MainMenuStrip.Name = "MainMenuStrip";
         this.MainMenuStrip.Size = new System.Drawing.Size(284, 24);
         this.MainMenuStrip.TabIndex = 0;
         this.MainMenuStrip.Text = "menuStrip1";
         // 
         // FileToolStripMenuItem
         // 
         this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.ExitToolStripMenuItem,
            this.PrintToolStripMenuItem,
            this.PrintPreviewToolStripMenuItem});
         this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
         this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.FileToolStripMenuItem.Text = "File";
         // 
         // OpenToolStripMenuItem
         // 
         this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
         this.OpenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.OpenToolStripMenuItem.Text = "Open";
         this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
         // 
         // ExitToolStripMenuItem
         // 
         this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
         this.ExitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.ExitToolStripMenuItem.Text = "Exit";
         this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
         // 
         // PrintToolStripMenuItem
         // 
         this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
         this.PrintToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.PrintToolStripMenuItem.Text = "Print";
         this.PrintToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripMenuItem_Click);
         // 
         // PrintPreviewToolStripMenuItem
         // 
         this.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem";
         this.PrintPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.PrintPreviewToolStripMenuItem.Text = "Print Preview";
         this.PrintPreviewToolStripMenuItem.Click += new System.EventHandler(this.PrintPreviewToolStripMenuItem_Click);
         // 
         // CapsEditorForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 261);
         this.Controls.Add(this.MainMenuStrip);
         this.Name = "CapsEditorForm";
         this.Text = "Caps editor form";
         this.MainMenuStrip.ResumeLayout(false);
         this.MainMenuStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip MainMenuStrip;
      private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem PrintToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem PrintPreviewToolStripMenuItem;
   }
}

