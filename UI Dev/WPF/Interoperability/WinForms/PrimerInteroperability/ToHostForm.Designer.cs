namespace PrimerInteroperability
{
	partial class ToHostForm
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
			this.simpleTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.showWpfButton = new System.Windows.Forms.Button();
			this.noteLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// simpleTextBox
			// 
			this.simpleTextBox.Location = new System.Drawing.Point(12, 12);
			this.simpleTextBox.Name = "simpleTextBox";
			this.simpleTextBox.Size = new System.Drawing.Size(100, 20);
			this.simpleTextBox.TabIndex = 0;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(12, 38);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(259, 36);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(12, 80);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(259, 39);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// showWpfButton
			// 
			this.showWpfButton.Location = new System.Drawing.Point(12, 125);
			this.showWpfButton.Name = "showWpfButton";
			this.showWpfButton.Size = new System.Drawing.Size(259, 37);
			this.showWpfButton.TabIndex = 3;
			this.showWpfButton.Text = "button1";
			this.showWpfButton.UseVisualStyleBackColor = true;
			this.showWpfButton.Click += new System.EventHandler(this.OnShowWpfWindow);
			// 
			// noteLabel
			// 
			this.noteLabel.AutoSize = true;
			this.noteLabel.Location = new System.Drawing.Point(9, 208);
			this.noteLabel.Name = "noteLabel";
			this.noteLabel.Size = new System.Drawing.Size(214, 26);
			this.noteLabel.TabIndex = 4;
			this.noteLabel.Text = "If you\'ve enabled support, Tab key will work\r\nwhen this windows is shown modeless" +
    "ly";
			// 
			// ToHostForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.noteLabel);
			this.Controls.Add(this.showWpfButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.simpleTextBox);
			this.Name = "ToHostForm";
			this.Text = "ToHostForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox simpleTextBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button showWpfButton;
		private System.Windows.Forms.Label noteLabel;
	}
}