namespace StripHtml
{
    partial class MainForm
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
         this.sourceHtmlLabel = new System.Windows.Forms.Label();
         this.htmlTextBox = new System.Windows.Forms.TextBox();
         this.strippedTextBox = new System.Windows.Forms.TextBox();
         this.strippedHtmlLabel = new System.Windows.Forms.Label();
         this.goButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // sourceHtmlLabel
         // 
         this.sourceHtmlLabel.AutoSize = true;
         this.sourceHtmlLabel.Location = new System.Drawing.Point(13, 13);
         this.sourceHtmlLabel.Name = "sourceHtmlLabel";
         this.sourceHtmlLabel.Size = new System.Drawing.Size(83, 13);
         this.sourceHtmlLabel.TabIndex = 0;
         this.sourceHtmlLabel.Text = "Source (HTML):";
         // 
         // htmlTextBox
         // 
         this.htmlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.htmlTextBox.Location = new System.Drawing.Point(103, 5);
         this.htmlTextBox.Multiline = true;
         this.htmlTextBox.Name = "htmlTextBox";
         this.htmlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.htmlTextBox.Size = new System.Drawing.Size(385, 92);
         this.htmlTextBox.TabIndex = 1;
         // 
         // strippedTextBox
         // 
         this.strippedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.strippedTextBox.Location = new System.Drawing.Point(103, 129);
         this.strippedTextBox.Multiline = true;
         this.strippedTextBox.Name = "strippedTextBox";
         this.strippedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.strippedTextBox.Size = new System.Drawing.Size(385, 92);
         this.strippedTextBox.TabIndex = 2;
         // 
         // strippedHtmlLabel
         // 
         this.strippedHtmlLabel.AutoSize = true;
         this.strippedHtmlLabel.Location = new System.Drawing.Point(13, 132);
         this.strippedHtmlLabel.Name = "strippedHtmlLabel";
         this.strippedHtmlLabel.Size = new System.Drawing.Size(49, 13);
         this.strippedHtmlLabel.TabIndex = 3;
         this.strippedHtmlLabel.Text = "Stripped:";
         // 
         // goButton
         // 
         this.goButton.Location = new System.Drawing.Point(16, 97);
         this.goButton.Name = "goButton";
         this.goButton.Size = new System.Drawing.Size(75, 23);
         this.goButton.TabIndex = 4;
         this.goButton.Text = "&Go";
         this.goButton.UseVisualStyleBackColor = true;
         this.goButton.Click += new System.EventHandler(this.goButton_Click);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(500, 231);
         this.Controls.Add(this.goButton);
         this.Controls.Add(this.strippedHtmlLabel);
         this.Controls.Add(this.strippedTextBox);
         this.Controls.Add(this.htmlTextBox);
         this.Controls.Add(this.sourceHtmlLabel);
         this.Name = "MainForm";
         this.Text = "StripHtml";
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sourceHtmlLabel;
        private System.Windows.Forms.TextBox htmlTextBox;
        private System.Windows.Forms.TextBox strippedTextBox;
        private System.Windows.Forms.Label strippedHtmlLabel;
        private System.Windows.Forms.Button goButton;
    }
}

