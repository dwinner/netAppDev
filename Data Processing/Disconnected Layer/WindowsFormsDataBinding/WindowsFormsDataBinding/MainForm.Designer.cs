namespace WindowsFormsDataBinding
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
         this.carInventoryGridView = new System.Windows.Forms.DataGridView();
         this.stockLabel = new System.Windows.Forms.Label();
         this.deleteGroupBox = new System.Windows.Forms.GroupBox();
         this.viewGroupBox = new System.Windows.Forms.GroupBox();
         this.deleteIdTextBox = new System.Windows.Forms.TextBox();
         this.deleteButton = new System.Windows.Forms.Button();
         this.makeTextBox = new System.Windows.Forms.TextBox();
         this.viewButton = new System.Windows.Forms.Button();
         this.changeMakeButton = new System.Windows.Forms.Button();
         this.coltsOnlyDataGridView = new System.Windows.Forms.DataGridView();
         this.yugoLabel = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.carInventoryGridView)).BeginInit();
         this.deleteGroupBox.SuspendLayout();
         this.viewGroupBox.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.coltsOnlyDataGridView)).BeginInit();
         this.SuspendLayout();
         // 
         // carInventoryGridView
         // 
         this.carInventoryGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.carInventoryGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.carInventoryGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
         this.carInventoryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.carInventoryGridView.Location = new System.Drawing.Point(12, 43);
         this.carInventoryGridView.Name = "carInventoryGridView";
         this.carInventoryGridView.Size = new System.Drawing.Size(497, 202);
         this.carInventoryGridView.TabIndex = 0;
         // 
         // stockLabel
         // 
         this.stockLabel.AutoSize = true;
         this.stockLabel.Font = new System.Drawing.Font("Arial Narrow", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.stockLabel.Location = new System.Drawing.Point(12, 9);
         this.stockLabel.Name = "stockLabel";
         this.stockLabel.Size = new System.Drawing.Size(192, 20);
         this.stockLabel.TabIndex = 1;
         this.stockLabel.Text = "Here is what we have in stock";
         // 
         // deleteGroupBox
         // 
         this.deleteGroupBox.Controls.Add(this.deleteButton);
         this.deleteGroupBox.Controls.Add(this.deleteIdTextBox);
         this.deleteGroupBox.Location = new System.Drawing.Point(12, 262);
         this.deleteGroupBox.Name = "deleteGroupBox";
         this.deleteGroupBox.Size = new System.Drawing.Size(238, 73);
         this.deleteGroupBox.TabIndex = 2;
         this.deleteGroupBox.TabStop = false;
         this.deleteGroupBox.Text = "Enter Car Id To Delete";
         // 
         // viewGroupBox
         // 
         this.viewGroupBox.Controls.Add(this.viewButton);
         this.viewGroupBox.Controls.Add(this.makeTextBox);
         this.viewGroupBox.Location = new System.Drawing.Point(274, 262);
         this.viewGroupBox.Name = "viewGroupBox";
         this.viewGroupBox.Size = new System.Drawing.Size(235, 73);
         this.viewGroupBox.TabIndex = 3;
         this.viewGroupBox.TabStop = false;
         this.viewGroupBox.Text = "Enter make to view";
         // 
         // deleteIdTextBox
         // 
         this.deleteIdTextBox.Location = new System.Drawing.Point(7, 20);
         this.deleteIdTextBox.Name = "deleteIdTextBox";
         this.deleteIdTextBox.Size = new System.Drawing.Size(100, 20);
         this.deleteIdTextBox.TabIndex = 0;
         // 
         // deleteButton
         // 
         this.deleteButton.Location = new System.Drawing.Point(117, 20);
         this.deleteButton.Name = "deleteButton";
         this.deleteButton.Size = new System.Drawing.Size(75, 23);
         this.deleteButton.TabIndex = 1;
         this.deleteButton.Text = "Delete";
         this.deleteButton.UseVisualStyleBackColor = true;
         this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
         // 
         // makeTextBox
         // 
         this.makeTextBox.Location = new System.Drawing.Point(7, 20);
         this.makeTextBox.Name = "makeTextBox";
         this.makeTextBox.Size = new System.Drawing.Size(100, 20);
         this.makeTextBox.TabIndex = 0;
         // 
         // viewButton
         // 
         this.viewButton.Location = new System.Drawing.Point(113, 20);
         this.viewButton.Name = "viewButton";
         this.viewButton.Size = new System.Drawing.Size(75, 23);
         this.viewButton.TabIndex = 1;
         this.viewButton.Text = "View";
         this.viewButton.UseVisualStyleBackColor = true;
         this.viewButton.Click += new System.EventHandler(this.viewButton_Click);
         // 
         // changeMakeButton
         // 
         this.changeMakeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.changeMakeButton.Location = new System.Drawing.Point(434, 13);
         this.changeMakeButton.Name = "changeMakeButton";
         this.changeMakeButton.Size = new System.Drawing.Size(75, 23);
         this.changeMakeButton.TabIndex = 4;
         this.changeMakeButton.Text = "Change";
         this.changeMakeButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         this.changeMakeButton.UseVisualStyleBackColor = true;
         this.changeMakeButton.Click += new System.EventHandler(this.changeMakeButton_Click);
         // 
         // coltsOnlyDataGridView
         // 
         this.coltsOnlyDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.coltsOnlyDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.coltsOnlyDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
         this.coltsOnlyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.coltsOnlyDataGridView.Location = new System.Drawing.Point(12, 395);
         this.coltsOnlyDataGridView.Name = "coltsOnlyDataGridView";
         this.coltsOnlyDataGridView.Size = new System.Drawing.Size(497, 150);
         this.coltsOnlyDataGridView.TabIndex = 5;
         // 
         // yugoLabel
         // 
         this.yugoLabel.AutoSize = true;
         this.yugoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.yugoLabel.Location = new System.Drawing.Point(12, 360);
         this.yugoLabel.Name = "yugoLabel";
         this.yugoLabel.Size = new System.Drawing.Size(90, 20);
         this.yugoLabel.TabIndex = 6;
         this.yugoLabel.Text = "Only Yugos";
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(521, 557);
         this.Controls.Add(this.yugoLabel);
         this.Controls.Add(this.coltsOnlyDataGridView);
         this.Controls.Add(this.changeMakeButton);
         this.Controls.Add(this.viewGroupBox);
         this.Controls.Add(this.deleteGroupBox);
         this.Controls.Add(this.stockLabel);
         this.Controls.Add(this.carInventoryGridView);
         this.Name = "MainForm";
         this.Text = "MainForm";
         ((System.ComponentModel.ISupportInitialize)(this.carInventoryGridView)).EndInit();
         this.deleteGroupBox.ResumeLayout(false);
         this.deleteGroupBox.PerformLayout();
         this.viewGroupBox.ResumeLayout(false);
         this.viewGroupBox.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.coltsOnlyDataGridView)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.DataGridView carInventoryGridView;
      private System.Windows.Forms.Label stockLabel;
      private System.Windows.Forms.GroupBox deleteGroupBox;
      private System.Windows.Forms.GroupBox viewGroupBox;
      private System.Windows.Forms.Button deleteButton;
      private System.Windows.Forms.TextBox deleteIdTextBox;
      private System.Windows.Forms.Button viewButton;
      private System.Windows.Forms.TextBox makeTextBox;
      private System.Windows.Forms.Button changeMakeButton;
      private System.Windows.Forms.DataGridView coltsOnlyDataGridView;
      private System.Windows.Forms.Label yugoLabel;
   }
}

