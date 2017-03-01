namespace SimplePaintProgram
{
   partial class ShapePickerDialog
   {
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
         this.SelectShapeGroupBox = new System.Windows.Forms.GroupBox();
         this.RectangleRadioButton = new System.Windows.Forms.RadioButton();
         this.CircleRadioButton = new System.Windows.Forms.RadioButton();
         this.OkBtn = new System.Windows.Forms.Button();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.SelectShapeGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // SelectShapeGroupBox
         // 
         this.SelectShapeGroupBox.Controls.Add(this.RectangleRadioButton);
         this.SelectShapeGroupBox.Controls.Add(this.CircleRadioButton);
         this.SelectShapeGroupBox.Location = new System.Drawing.Point(13, 13);
         this.SelectShapeGroupBox.Name = "SelectShapeGroupBox";
         this.SelectShapeGroupBox.Size = new System.Drawing.Size(200, 65);
         this.SelectShapeGroupBox.TabIndex = 0;
         this.SelectShapeGroupBox.TabStop = false;
         this.SelectShapeGroupBox.Text = "Select shape";
         // 
         // RectangleRadioButton
         // 
         this.RectangleRadioButton.AutoSize = true;
         this.RectangleRadioButton.Location = new System.Drawing.Point(63, 19);
         this.RectangleRadioButton.Name = "RectangleRadioButton";
         this.RectangleRadioButton.Size = new System.Drawing.Size(74, 17);
         this.RectangleRadioButton.TabIndex = 1;
         this.RectangleRadioButton.TabStop = true;
         this.RectangleRadioButton.Text = "Rectangle";
         this.RectangleRadioButton.UseVisualStyleBackColor = true;
         // 
         // CircleRadioButton
         // 
         this.CircleRadioButton.AutoSize = true;
         this.CircleRadioButton.Location = new System.Drawing.Point(6, 19);
         this.CircleRadioButton.Name = "CircleRadioButton";
         this.CircleRadioButton.Size = new System.Drawing.Size(51, 17);
         this.CircleRadioButton.TabIndex = 0;
         this.CircleRadioButton.TabStop = true;
         this.CircleRadioButton.Text = "Circle";
         this.CircleRadioButton.UseVisualStyleBackColor = true;
         // 
         // OkBtn
         // 
         this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkBtn.Location = new System.Drawing.Point(75, 108);
         this.OkBtn.Name = "OkBtn";
         this.OkBtn.Size = new System.Drawing.Size(75, 23);
         this.OkBtn.TabIndex = 1;
         this.OkBtn.Text = "Ok";
         this.OkBtn.UseVisualStyleBackColor = true;
         this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
         // 
         // CancelBtn
         // 
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.Location = new System.Drawing.Point(156, 108);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(75, 23);
         this.CancelBtn.TabIndex = 2;
         this.CancelBtn.Text = "Cancel";
         this.CancelBtn.UseVisualStyleBackColor = true;
         // 
         // ShapePickerDialog
         // 
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
         this.ClientSize = new System.Drawing.Size(242, 143);
         this.Controls.Add(this.CancelBtn);
         this.Controls.Add(this.OkBtn);
         this.Controls.Add(this.SelectShapeGroupBox);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ShapePickerDialog";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "ShapePickerDialog";
         this.SelectShapeGroupBox.ResumeLayout(false);
         this.SelectShapeGroupBox.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox SelectShapeGroupBox;
      private System.Windows.Forms.RadioButton RectangleRadioButton;
      private System.Windows.Forms.RadioButton CircleRadioButton;
      private System.Windows.Forms.Button OkBtn;
      private System.Windows.Forms.Button CancelBtn;
   }
}