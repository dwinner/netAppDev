namespace LinqToXmlWinApp
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
         this.currentInventoryLabel = new System.Windows.Forms.Label();
         this.inventoryTextBox = new System.Windows.Forms.TextBox();
         this.addItemGroupBox = new System.Windows.Forms.GroupBox();
         this.makeLabel = new System.Windows.Forms.Label();
         this.makeTextBox = new System.Windows.Forms.TextBox();
         this.colorLabel = new System.Windows.Forms.Label();
         this.colorTextBox = new System.Windows.Forms.TextBox();
         this.petNameLabel = new System.Windows.Forms.Label();
         this.petNameTextBox = new System.Windows.Forms.TextBox();
         this.addButton = new System.Windows.Forms.Button();
         this.lookUpGroupBox = new System.Windows.Forms.GroupBox();
         this.makeToLookUpLabel = new System.Windows.Forms.Label();
         this.makeToLookUpTextBox = new System.Windows.Forms.TextBox();
         this.lookUpColorsButton = new System.Windows.Forms.Button();
         this.addItemGroupBox.SuspendLayout();
         this.lookUpGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // currentInventoryLabel
         // 
         this.currentInventoryLabel.AutoSize = true;
         this.currentInventoryLabel.Location = new System.Drawing.Point(13, 13);
         this.currentInventoryLabel.Name = "currentInventoryLabel";
         this.currentInventoryLabel.Size = new System.Drawing.Size(87, 13);
         this.currentInventoryLabel.TabIndex = 0;
         this.currentInventoryLabel.Text = "Current inventory";
         // 
         // inventoryTextBox
         // 
         this.inventoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.inventoryTextBox.Location = new System.Drawing.Point(16, 40);
         this.inventoryTextBox.Multiline = true;
         this.inventoryTextBox.Name = "inventoryTextBox";
         this.inventoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.inventoryTextBox.Size = new System.Drawing.Size(277, 341);
         this.inventoryTextBox.TabIndex = 1;
         // 
         // addItemGroupBox
         // 
         this.addItemGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.addItemGroupBox.Controls.Add(this.addButton);
         this.addItemGroupBox.Controls.Add(this.petNameTextBox);
         this.addItemGroupBox.Controls.Add(this.petNameLabel);
         this.addItemGroupBox.Controls.Add(this.colorTextBox);
         this.addItemGroupBox.Controls.Add(this.colorLabel);
         this.addItemGroupBox.Controls.Add(this.makeTextBox);
         this.addItemGroupBox.Controls.Add(this.makeLabel);
         this.addItemGroupBox.Location = new System.Drawing.Point(328, 40);
         this.addItemGroupBox.Name = "addItemGroupBox";
         this.addItemGroupBox.Size = new System.Drawing.Size(308, 171);
         this.addItemGroupBox.TabIndex = 2;
         this.addItemGroupBox.TabStop = false;
         this.addItemGroupBox.Text = "Add inventory item";
         // 
         // makeLabel
         // 
         this.makeLabel.AutoSize = true;
         this.makeLabel.Location = new System.Drawing.Point(7, 20);
         this.makeLabel.Name = "makeLabel";
         this.makeLabel.Size = new System.Drawing.Size(34, 13);
         this.makeLabel.TabIndex = 0;
         this.makeLabel.Text = "Make";
         // 
         // makeTextBox
         // 
         this.makeTextBox.Location = new System.Drawing.Point(82, 17);
         this.makeTextBox.Name = "makeTextBox";
         this.makeTextBox.Size = new System.Drawing.Size(201, 20);
         this.makeTextBox.TabIndex = 1;
         // 
         // colorLabel
         // 
         this.colorLabel.AutoSize = true;
         this.colorLabel.Location = new System.Drawing.Point(7, 57);
         this.colorLabel.Name = "colorLabel";
         this.colorLabel.Size = new System.Drawing.Size(31, 13);
         this.colorLabel.TabIndex = 2;
         this.colorLabel.Text = "Color";
         // 
         // colorTextBox
         // 
         this.colorTextBox.Location = new System.Drawing.Point(82, 54);
         this.colorTextBox.Name = "colorTextBox";
         this.colorTextBox.Size = new System.Drawing.Size(201, 20);
         this.colorTextBox.TabIndex = 3;
         // 
         // petNameLabel
         // 
         this.petNameLabel.AutoSize = true;
         this.petNameLabel.Location = new System.Drawing.Point(7, 99);
         this.petNameLabel.Name = "petNameLabel";
         this.petNameLabel.Size = new System.Drawing.Size(52, 13);
         this.petNameLabel.TabIndex = 4;
         this.petNameLabel.Text = "Pet name";
         // 
         // petNameTextBox
         // 
         this.petNameTextBox.Location = new System.Drawing.Point(82, 96);
         this.petNameTextBox.Name = "petNameTextBox";
         this.petNameTextBox.Size = new System.Drawing.Size(201, 20);
         this.petNameTextBox.TabIndex = 5;
         // 
         // addButton
         // 
         this.addButton.Location = new System.Drawing.Point(207, 123);
         this.addButton.Name = "addButton";
         this.addButton.Size = new System.Drawing.Size(75, 23);
         this.addButton.TabIndex = 6;
         this.addButton.Text = "Add";
         this.addButton.UseVisualStyleBackColor = true;
         this.addButton.Click += new System.EventHandler(this.addButton_Click);
         // 
         // lookUpGroupBox
         // 
         this.lookUpGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.lookUpGroupBox.Controls.Add(this.lookUpColorsButton);
         this.lookUpGroupBox.Controls.Add(this.makeToLookUpTextBox);
         this.lookUpGroupBox.Controls.Add(this.makeToLookUpLabel);
         this.lookUpGroupBox.Location = new System.Drawing.Point(328, 250);
         this.lookUpGroupBox.Name = "lookUpGroupBox";
         this.lookUpGroupBox.Size = new System.Drawing.Size(308, 130);
         this.lookUpGroupBox.TabIndex = 3;
         this.lookUpGroupBox.TabStop = false;
         this.lookUpGroupBox.Text = "Look up colors for make";
         // 
         // makeToLookUpLabel
         // 
         this.makeToLookUpLabel.AutoSize = true;
         this.makeToLookUpLabel.Location = new System.Drawing.Point(10, 20);
         this.makeToLookUpLabel.Name = "makeToLookUpLabel";
         this.makeToLookUpLabel.Size = new System.Drawing.Size(84, 13);
         this.makeToLookUpLabel.TabIndex = 0;
         this.makeToLookUpLabel.Text = "Make to look up";
         // 
         // makeToLookUpTextBox
         // 
         this.makeToLookUpTextBox.Location = new System.Drawing.Point(121, 17);
         this.makeToLookUpTextBox.Name = "makeToLookUpTextBox";
         this.makeToLookUpTextBox.Size = new System.Drawing.Size(161, 20);
         this.makeToLookUpTextBox.TabIndex = 1;
         // 
         // lookUpColorsButton
         // 
         this.lookUpColorsButton.Location = new System.Drawing.Point(206, 44);
         this.lookUpColorsButton.Name = "lookUpColorsButton";
         this.lookUpColorsButton.Size = new System.Drawing.Size(75, 23);
         this.lookUpColorsButton.TabIndex = 2;
         this.lookUpColorsButton.Text = "Look up colors";
         this.lookUpColorsButton.UseVisualStyleBackColor = true;
         this.lookUpColorsButton.Click += new System.EventHandler(this.lookUpColorsButton_Click);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(648, 426);
         this.Controls.Add(this.lookUpGroupBox);
         this.Controls.Add(this.addItemGroupBox);
         this.Controls.Add(this.inventoryTextBox);
         this.Controls.Add(this.currentInventoryLabel);
         this.Name = "MainForm";
         this.Text = "LINQ to XML";
         this.Load += new System.EventHandler(this.MainForm_Load);
         this.addItemGroupBox.ResumeLayout(false);
         this.addItemGroupBox.PerformLayout();
         this.lookUpGroupBox.ResumeLayout(false);
         this.lookUpGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label currentInventoryLabel;
      private System.Windows.Forms.TextBox inventoryTextBox;
      private System.Windows.Forms.GroupBox addItemGroupBox;
      private System.Windows.Forms.TextBox petNameTextBox;
      private System.Windows.Forms.Label petNameLabel;
      private System.Windows.Forms.TextBox colorTextBox;
      private System.Windows.Forms.Label colorLabel;
      private System.Windows.Forms.TextBox makeTextBox;
      private System.Windows.Forms.Label makeLabel;
      private System.Windows.Forms.Button addButton;
      private System.Windows.Forms.GroupBox lookUpGroupBox;
      private System.Windows.Forms.Label makeToLookUpLabel;
      private System.Windows.Forms.Button lookUpColorsButton;
      private System.Windows.Forms.TextBox makeToLookUpTextBox;

   }
}

