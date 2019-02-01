namespace DataGridViewDataDesigner
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
         this.components = new System.ComponentModel.Container();
         this.inventoryDataGridView = new System.Windows.Forms.DataGridView();
         this.carIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.makeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.petNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.inventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.inventoryDataSet = new DataGridViewDataDesigner.InventoryDataSet();
         this.inventoryTableAdapter = new DataGridViewDataDesigner.InventoryDataSetTableAdapters.InventoryTableAdapter();
         this.updateInventoryButton = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataSet)).BeginInit();
         this.SuspendLayout();
         // 
         // inventoryDataGridView
         // 
         this.inventoryDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.inventoryDataGridView.AutoGenerateColumns = false;
         this.inventoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.inventoryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
         this.inventoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.inventoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.carIdDataGridViewTextBoxColumn,
            this.makeDataGridViewTextBoxColumn,
            this.colorDataGridViewTextBoxColumn,
            this.petNameDataGridViewTextBoxColumn});
         this.inventoryDataGridView.DataSource = this.inventoryBindingSource;
         this.inventoryDataGridView.Location = new System.Drawing.Point(50, 12);
         this.inventoryDataGridView.Name = "inventoryDataGridView";
         this.inventoryDataGridView.Size = new System.Drawing.Size(451, 150);
         this.inventoryDataGridView.TabIndex = 0;
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
         // inventoryBindingSource
         // 
         this.inventoryBindingSource.DataMember = "Inventory";
         this.inventoryBindingSource.DataSource = this.inventoryDataSet;
         this.inventoryBindingSource.CurrentChanged += new System.EventHandler(this.inventoryBindingSource_CurrentChanged);
         // 
         // inventoryDataSet
         // 
         this.inventoryDataSet.DataSetName = "InventoryDataSet";
         this.inventoryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
         // 
         // inventoryTableAdapter
         // 
         this.inventoryTableAdapter.ClearBeforeFill = true;
         // 
         // updateInventoryButton
         // 
         this.updateInventoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.updateInventoryButton.Location = new System.Drawing.Point(425, 212);
         this.updateInventoryButton.Name = "updateInventoryButton";
         this.updateInventoryButton.Size = new System.Drawing.Size(75, 23);
         this.updateInventoryButton.TabIndex = 1;
         this.updateInventoryButton.Text = "Update";
         this.updateInventoryButton.UseVisualStyleBackColor = true;
         this.updateInventoryButton.Click += new System.EventHandler(this.updateInventoryButton_Click);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(556, 262);
         this.Controls.Add(this.updateInventoryButton);
         this.Controls.Add(this.inventoryDataGridView);
         this.Name = "MainForm";
         this.Text = "Main form";
         this.Load += new System.EventHandler(this.MainForm_Load);
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.inventoryDataSet)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView inventoryDataGridView;
      private InventoryDataSet inventoryDataSet;
      private System.Windows.Forms.BindingSource inventoryBindingSource;
      private InventoryDataSetTableAdapters.InventoryTableAdapter inventoryTableAdapter;
      private System.Windows.Forms.DataGridViewTextBoxColumn carIdDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn makeDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn colorDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn petNameDataGridViewTextBoxColumn;
      private System.Windows.Forms.Button updateInventoryButton;
   }
}

