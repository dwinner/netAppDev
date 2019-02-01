namespace EdmWinFormBinding
{
   partial class InventoryForm
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
         this.components = new System.ComponentModel.Container();
         this.inventoryDataGridView = new System.Windows.Forms.DataGridView();
         this.inventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.updateButton = new System.Windows.Forms.Button();
         this.carIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.makeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.petNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource)).BeginInit();
         this.SuspendLayout();
         // 
         // inventoryDataGridView
         // 
         this.inventoryDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.inventoryDataGridView.AutoGenerateColumns = false;
         this.inventoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.inventoryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
         this.inventoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.inventoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.carIdDataGridViewTextBoxColumn,
            this.makeDataGridViewTextBoxColumn,
            this.colorDataGridViewTextBoxColumn,
            this.petNameDataGridViewTextBoxColumn});
         this.inventoryDataGridView.DataSource = this.inventoryBindingSource;
         this.inventoryDataGridView.Location = new System.Drawing.Point(12, 12);
         this.inventoryDataGridView.Name = "inventoryDataGridView";
         this.inventoryDataGridView.Size = new System.Drawing.Size(431, 150);
         this.inventoryDataGridView.TabIndex = 0;
         // 
         // inventoryBindingSource
         // 
         this.inventoryBindingSource.DataSource = typeof(AutoLotEntityLibrary.Inventory);
         // 
         // updateButton
         // 
         this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.updateButton.Location = new System.Drawing.Point(367, 227);
         this.updateButton.Name = "updateButton";
         this.updateButton.Size = new System.Drawing.Size(75, 23);
         this.updateButton.TabIndex = 1;
         this.updateButton.Text = "Update";
         this.updateButton.UseVisualStyleBackColor = true;
         this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
         // 
         // carIdDataGridViewTextBoxColumn
         // 
         this.carIdDataGridViewTextBoxColumn.DataPropertyName = "CarId";
         this.carIdDataGridViewTextBoxColumn.HeaderText = "CarId";
         this.carIdDataGridViewTextBoxColumn.Name = "carIdDataGridViewTextBoxColumn";
         // 
         // makeDataGridViewTextBoxColumn
         // 
         this.makeDataGridViewTextBoxColumn.DataPropertyName = "Make";
         this.makeDataGridViewTextBoxColumn.HeaderText = "Make";
         this.makeDataGridViewTextBoxColumn.Name = "makeDataGridViewTextBoxColumn";
         // 
         // colorDataGridViewTextBoxColumn
         // 
         this.colorDataGridViewTextBoxColumn.DataPropertyName = "Color";
         this.colorDataGridViewTextBoxColumn.HeaderText = "Color";
         this.colorDataGridViewTextBoxColumn.Name = "colorDataGridViewTextBoxColumn";
         // 
         // petNameDataGridViewTextBoxColumn
         // 
         this.petNameDataGridViewTextBoxColumn.DataPropertyName = "PetName";
         this.petNameDataGridViewTextBoxColumn.HeaderText = "PetName";
         this.petNameDataGridViewTextBoxColumn.Name = "petNameDataGridViewTextBoxColumn";
         // 
         // InventoryForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(455, 262);
         this.Controls.Add(this.updateButton);
         this.Controls.Add(this.inventoryDataGridView);
         this.Name = "InventoryForm";
         this.Text = "Inventory form";
         this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InvForm_Closed);
         this.Load += new System.EventHandler(this.InvForm_Load);
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView inventoryDataGridView;
      private System.Windows.Forms.BindingSource inventoryBindingSource;
      private System.Windows.Forms.Button updateButton;
      private System.Windows.Forms.DataGridViewTextBoxColumn carIdDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn makeDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn colorDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn petNameDataGridViewTextBoxColumn;
   }
}

