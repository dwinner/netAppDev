namespace SimplePaintProgram
{
   partial class PaintForm
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
         this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.pickShapesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.pickColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.clearSurfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.TopMenuStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // TopMenuStrip
         // 
         this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
         this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
         this.TopMenuStrip.Name = "TopMenuStrip";
         this.TopMenuStrip.Size = new System.Drawing.Size(445, 24);
         this.TopMenuStrip.TabIndex = 0;
         this.TopMenuStrip.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.ShortcutKeyDisplayString = "F";
         this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.saveToolStripMenuItem.Text = "Save";
         this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSave);
         // 
         // loadToolStripMenuItem
         // 
         this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
         this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
         this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.loadToolStripMenuItem.Text = "Load";
         this.loadToolStripMenuItem.Click += new System.EventHandler(this.OnLoad);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.exitToolStripMenuItem.Text = "Exit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
         // 
         // toolsToolStripMenuItem
         // 
         this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pickShapesToolStripMenuItem,
            this.pickColorToolStripMenuItem,
            this.clearSurfaceToolStripMenuItem});
         this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
         this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
         this.toolsToolStripMenuItem.Text = "&Tools";
         // 
         // pickShapesToolStripMenuItem
         // 
         this.pickShapesToolStripMenuItem.Name = "pickShapesToolStripMenuItem";
         this.pickShapesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
         this.pickShapesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
         this.pickShapesToolStripMenuItem.Text = "Pick Shapes";
         this.pickShapesToolStripMenuItem.Click += new System.EventHandler(this.OnPickShapes);
         // 
         // pickColorToolStripMenuItem
         // 
         this.pickColorToolStripMenuItem.Name = "pickColorToolStripMenuItem";
         this.pickColorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
         this.pickColorToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
         this.pickColorToolStripMenuItem.Text = "Pick Color";
         this.pickColorToolStripMenuItem.Click += new System.EventHandler(this.OnPickColor);
         // 
         // clearSurfaceToolStripMenuItem
         // 
         this.clearSurfaceToolStripMenuItem.Name = "clearSurfaceToolStripMenuItem";
         this.clearSurfaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
         this.clearSurfaceToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
         this.clearSurfaceToolStripMenuItem.Text = "Clear Surface";
         this.clearSurfaceToolStripMenuItem.Click += new System.EventHandler(this.OnClearSurface);
         // 
         // PaintForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(445, 295);
         this.Controls.Add(this.TopMenuStrip);
         this.MainMenuStrip = this.TopMenuStrip;
         this.Name = "PaintForm";
         this.Text = "Simple paint";
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintForm_Paint);
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PaintForm_MouseDown);
         this.TopMenuStrip.ResumeLayout(false);
         this.TopMenuStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip TopMenuStrip;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem pickShapesToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem pickColorToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem clearSurfaceToolStripMenuItem;
   }
}

