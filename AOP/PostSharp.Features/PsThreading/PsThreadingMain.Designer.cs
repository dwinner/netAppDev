namespace PsThreading
{
   partial class PsThreadingMain
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
         this.UpdateTextBox = new System.Windows.Forms.TextBox();
         this.UpdateButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // UpdateTextBox
         // 
         this.UpdateTextBox.Location = new System.Drawing.Point(88, 69);
         this.UpdateTextBox.Name = "UpdateTextBox";
         this.UpdateTextBox.Size = new System.Drawing.Size(100, 20);
         this.UpdateTextBox.TabIndex = 0;
         // 
         // UpdateButton
         // 
         this.UpdateButton.Location = new System.Drawing.Point(88, 106);
         this.UpdateButton.Name = "UpdateButton";
         this.UpdateButton.Size = new System.Drawing.Size(75, 23);
         this.UpdateButton.TabIndex = 1;
         this.UpdateButton.Text = "Update";
         this.UpdateButton.UseVisualStyleBackColor = true;
         this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
         // 
         // PsThreadingMain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 261);
         this.Controls.Add(this.UpdateButton);
         this.Controls.Add(this.UpdateTextBox);
         this.Name = "PsThreadingMain";
         this.Text = "PsThreadingDemo";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox UpdateTextBox;
      private System.Windows.Forms.Button UpdateButton;
   }
}

