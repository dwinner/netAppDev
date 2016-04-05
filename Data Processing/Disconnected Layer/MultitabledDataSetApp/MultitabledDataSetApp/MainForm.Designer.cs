namespace MultitabledDataSetApp
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
         this.customersDataGridView = new System.Windows.Forms.DataGridView();
         this.ordersDataGridView = new System.Windows.Forms.DataGridView();
         this.updateDatabaseButton = new System.Windows.Forms.Button();
         this.inventoryDataGridView = new System.Windows.Forms.DataGridView();
         this.relationsGroupBox = new System.Windows.Forms.GroupBox();
         this.customerIdLabel = new System.Windows.Forms.Label();
         this.custIdTextBox = new System.Windows.Forms.TextBox();
         this.retrieveOrderDetailsButton = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.customersDataGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.ordersDataGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGridView)).BeginInit();
         this.relationsGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // customersDataGridView
         // 
         this.customersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.customersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.customersDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
         this.customersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.customersDataGridView.Location = new System.Drawing.Point(12, 137);
         this.customersDataGridView.Name = "customersDataGridView";
         this.customersDataGridView.Size = new System.Drawing.Size(714, 150);
         this.customersDataGridView.TabIndex = 1;
         // 
         // ordersDataGridView
         // 
         this.ordersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.ordersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.ordersDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
         this.ordersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.ordersDataGridView.Location = new System.Drawing.Point(12, 293);
         this.ordersDataGridView.Name = "ordersDataGridView";
         this.ordersDataGridView.Size = new System.Drawing.Size(714, 150);
         this.ordersDataGridView.TabIndex = 2;
         // 
         // updateDatabaseButton
         // 
         this.updateDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.updateDatabaseButton.Location = new System.Drawing.Point(651, 567);
         this.updateDatabaseButton.Name = "updateDatabaseButton";
         this.updateDatabaseButton.Size = new System.Drawing.Size(75, 23);
         this.updateDatabaseButton.TabIndex = 3;
         this.updateDatabaseButton.Text = "Update Database";
         this.updateDatabaseButton.UseVisualStyleBackColor = true;
         this.updateDatabaseButton.Click += new System.EventHandler(this.updateDatabaseButton_Click);
         // 
         // inventoryDataGridView
         // 
         this.inventoryDataGridView.AllowUserToOrderColumns = true;
         this.inventoryDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.inventoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.inventoryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
         this.inventoryDataGridView.Location = new System.Drawing.Point(12, 3);
         this.inventoryDataGridView.Name = "inventoryDataGridView";
         this.inventoryDataGridView.Size = new System.Drawing.Size(714, 128);
         this.inventoryDataGridView.TabIndex = 4;
         // 
         // relationsGroupBox
         // 
         this.relationsGroupBox.Controls.Add(this.retrieveOrderDetailsButton);
         this.relationsGroupBox.Controls.Add(this.custIdTextBox);
         this.relationsGroupBox.Controls.Add(this.customerIdLabel);
         this.relationsGroupBox.Location = new System.Drawing.Point(12, 490);
         this.relationsGroupBox.Name = "relationsGroupBox";
         this.relationsGroupBox.Size = new System.Drawing.Size(292, 100);
         this.relationsGroupBox.TabIndex = 5;
         this.relationsGroupBox.TabStop = false;
         this.relationsGroupBox.Text = "Look up Customer Order";
         // 
         // customerIdLabel
         // 
         this.customerIdLabel.AutoSize = true;
         this.customerIdLabel.Location = new System.Drawing.Point(7, 20);
         this.customerIdLabel.Name = "customerIdLabel";
         this.customerIdLabel.Size = new System.Drawing.Size(63, 13);
         this.customerIdLabel.TabIndex = 0;
         this.customerIdLabel.Text = "Customer Id";
         // 
         // custIdTextBox
         // 
         this.custIdTextBox.Location = new System.Drawing.Point(135, 12);
         this.custIdTextBox.Name = "custIdTextBox";
         this.custIdTextBox.Size = new System.Drawing.Size(100, 20);
         this.custIdTextBox.TabIndex = 1;
         // 
         // retrieveOrderDetailsButton
         // 
         this.retrieveOrderDetailsButton.Location = new System.Drawing.Point(135, 67);
         this.retrieveOrderDetailsButton.Name = "retrieveOrderDetailsButton";
         this.retrieveOrderDetailsButton.Size = new System.Drawing.Size(99, 23);
         this.retrieveOrderDetailsButton.TabIndex = 2;
         this.retrieveOrderDetailsButton.Text = "Get Order Details";
         this.retrieveOrderDetailsButton.UseVisualStyleBackColor = true;
         this.retrieveOrderDetailsButton.Click += new System.EventHandler(this.retrieveOrderDetailsButton_Click);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(738, 628);
         this.Controls.Add(this.relationsGroupBox);
         this.Controls.Add(this.inventoryDataGridView);
         this.Controls.Add(this.updateDatabaseButton);
         this.Controls.Add(this.ordersDataGridView);
         this.Controls.Add(this.customersDataGridView);
         this.Name = "MainForm";
         this.Text = "MainForm";
         ((System.ComponentModel.ISupportInitialize)(this.customersDataGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.ordersDataGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGridView)).EndInit();
         this.relationsGroupBox.ResumeLayout(false);
         this.relationsGroupBox.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView customersDataGridView;
      private System.Windows.Forms.DataGridView ordersDataGridView;
      private System.Windows.Forms.Button updateDatabaseButton;
      private System.Windows.Forms.DataGridView inventoryDataGridView;
      private System.Windows.Forms.GroupBox relationsGroupBox;
      private System.Windows.Forms.Label customerIdLabel;
      private System.Windows.Forms.Button retrieveOrderDetailsButton;
      private System.Windows.Forms.TextBox custIdTextBox;
   }
}

