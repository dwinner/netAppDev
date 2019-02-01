namespace BrowseForDirectories
{
    partial class BrowseForm
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
         this.browseButton = new System.Windows.Forms.Button();
         this.filenameTextBox = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // browseButton
         // 
         this.browseButton.Location = new System.Drawing.Point(410, 12);
         this.browseButton.Name = "browseButton";
         this.browseButton.Size = new System.Drawing.Size(75, 23);
         this.browseButton.TabIndex = 0;
         this.browseButton.Text = "Browse...";
         this.browseButton.UseVisualStyleBackColor = true;
         this.browseButton.Click += new System.EventHandler(this.buttonBrowse_Click);
         // 
         // filenameTextBox
         // 
         this.filenameTextBox.Location = new System.Drawing.Point(12, 15);
         this.filenameTextBox.Name = "filenameTextBox";
         this.filenameTextBox.Size = new System.Drawing.Size(392, 20);
         this.filenameTextBox.TabIndex = 1;
         // 
         // BrowseForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(497, 180);
         this.Controls.Add(this.filenameTextBox);
         this.Controls.Add(this.browseButton);
         this.Name = "BrowseForm";
         this.Text = "Browse for Directories";
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox filenameTextBox;
    }
}

