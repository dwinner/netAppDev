namespace CarOrderApp
{
   partial class OrderAutoDialog
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
         this.makeLabel = new System.Windows.Forms.Label();
         this.MakeTextBox = new System.Windows.Forms.TextBox();
         this.colorLabel = new System.Windows.Forms.Label();
         this.ColorTextBox = new System.Windows.Forms.TextBox();
         this.priceLabel = new System.Windows.Forms.Label();
         this.PriceTextBox = new System.Windows.Forms.TextBox();
         this.OkBtn = new System.Windows.Forms.Button();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // makeLabel
         // 
         this.makeLabel.AutoSize = true;
         this.makeLabel.Location = new System.Drawing.Point(13, 13);
         this.makeLabel.Name = "makeLabel";
         this.makeLabel.Size = new System.Drawing.Size(34, 13);
         this.makeLabel.TabIndex = 3;
         this.makeLabel.Text = "Make";
         // 
         // MakeTextBox
         // 
         this.MakeTextBox.Location = new System.Drawing.Point(53, 10);
         this.MakeTextBox.Name = "MakeTextBox";
         this.MakeTextBox.Size = new System.Drawing.Size(100, 20);
         this.MakeTextBox.TabIndex = 0;
         // 
         // colorLabel
         // 
         this.colorLabel.AutoSize = true;
         this.colorLabel.Location = new System.Drawing.Point(12, 45);
         this.colorLabel.Name = "colorLabel";
         this.colorLabel.Size = new System.Drawing.Size(31, 13);
         this.colorLabel.TabIndex = 4;
         this.colorLabel.Text = "Color";
         // 
         // ColorTextBox
         // 
         this.ColorTextBox.Location = new System.Drawing.Point(53, 42);
         this.ColorTextBox.Name = "ColorTextBox";
         this.ColorTextBox.Size = new System.Drawing.Size(100, 20);
         this.ColorTextBox.TabIndex = 1;
         // 
         // priceLabel
         // 
         this.priceLabel.AutoSize = true;
         this.priceLabel.Location = new System.Drawing.Point(12, 80);
         this.priceLabel.Name = "priceLabel";
         this.priceLabel.Size = new System.Drawing.Size(31, 13);
         this.priceLabel.TabIndex = 5;
         this.priceLabel.Text = "Price";
         // 
         // PriceTextBox
         // 
         this.PriceTextBox.Location = new System.Drawing.Point(53, 77);
         this.PriceTextBox.Name = "PriceTextBox";
         this.PriceTextBox.Size = new System.Drawing.Size(100, 20);
         this.PriceTextBox.TabIndex = 2;
         // 
         // OkBtn
         // 
         this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OkBtn.Location = new System.Drawing.Point(159, 150);
         this.OkBtn.Name = "OkBtn";
         this.OkBtn.Size = new System.Drawing.Size(75, 23);
         this.OkBtn.TabIndex = 6;
         this.OkBtn.Text = "Ok";
         this.OkBtn.UseVisualStyleBackColor = true;
         // 
         // CancelBtn
         // 
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.Location = new System.Drawing.Point(240, 150);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(75, 23);
         this.CancelBtn.TabIndex = 7;
         this.CancelBtn.Text = "Cancel";
         this.CancelBtn.UseVisualStyleBackColor = true;
         // 
         // OrderAutoDialog
         // 
         this.AcceptButton = this.OkBtn;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.CancelBtn;
         this.ClientSize = new System.Drawing.Size(327, 185);
         this.Controls.Add(this.CancelBtn);
         this.Controls.Add(this.OkBtn);
         this.Controls.Add(this.PriceTextBox);
         this.Controls.Add(this.priceLabel);
         this.Controls.Add(this.ColorTextBox);
         this.Controls.Add(this.colorLabel);
         this.Controls.Add(this.MakeTextBox);
         this.Controls.Add(this.makeLabel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "OrderAutoDialog";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Order dialog";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label makeLabel;
      private System.Windows.Forms.Label colorLabel;
      private System.Windows.Forms.Label priceLabel;
      private System.Windows.Forms.Button OkBtn;
      private System.Windows.Forms.Button CancelBtn;
      internal System.Windows.Forms.TextBox MakeTextBox;
      internal System.Windows.Forms.TextBox ColorTextBox;
      internal System.Windows.Forms.TextBox PriceTextBox;
   }
}