namespace WmiProcessManagement
{
   partial class ProcManager
   {
      private System.Windows.Forms.DataGrid processGrid;
      private System.Windows.Forms.Button getProcessListButton;      
      private System.Windows.Forms.Label notepadLabel;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.processGrid = new System.Windows.Forms.DataGrid();
         this.getProcessListButton = new System.Windows.Forms.Button();
         this.notepadLabel = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.processGrid)).BeginInit();
         this.SuspendLayout();
         // 
         // processGrid
         // 
         this.processGrid.DataMember = "";
         this.processGrid.Dock = System.Windows.Forms.DockStyle.Top;
         this.processGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
         this.processGrid.Location = new System.Drawing.Point(0, 0);
         this.processGrid.Name = "processGrid";
         this.processGrid.Size = new System.Drawing.Size(472, 192);
         this.processGrid.TabIndex = 0;
         // 
         // getProcessListButton
         // 
         this.getProcessListButton.Location = new System.Drawing.Point(8, 200);
         this.getProcessListButton.Name = "getProcessListButton";
         this.getProcessListButton.Size = new System.Drawing.Size(136, 23);
         this.getProcessListButton.TabIndex = 1;
         this.getProcessListButton.Text = "Get Process List";
         this.getProcessListButton.Click += new System.EventHandler(this.OnGetProcessesClick);
         // 
         // notepadLabel
         // 
         this.notepadLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.notepadLabel.Location = new System.Drawing.Point(0, 250);
         this.notepadLabel.Name = "notepadLabel";
         this.notepadLabel.Size = new System.Drawing.Size(472, 23);
         this.notepadLabel.TabIndex = 2;
         // 
         // ProcManager
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(472, 273);
         this.Controls.Add(this.notepadLabel);
         this.Controls.Add(this.getProcessListButton);
         this.Controls.Add(this.processGrid);
         this.Name = "ProcManager";
         this.Text = "Process Info";
         ((System.ComponentModel.ISupportInitialize)(this.processGrid)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion
   }
}

